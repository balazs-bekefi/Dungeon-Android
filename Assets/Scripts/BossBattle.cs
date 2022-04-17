using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private BossBattleTrigger bossBattleTrigger;
    [SerializeField] private Enemy _EnemyPrefab;
    [SerializeField] private Enemy _EnemyPrefab1;
    public DoorController doorController;
    public AudioSource normalAudio;
    public AudioSource bossAudio;
    public GridManager gridManager;
    public GameObject canvas;
    public FinalBoss finalBoss;
    public GameObject doorBlock;
    private int count=0;
    private int count1=0;

    private void Start()
    {
        bossBattleTrigger.OnplayerEnterTrigger += BossBattleTrigger_OnPlayerEntertrigger;        
    }

    private void BossBattleTrigger_OnPlayerEntertrigger(object sender, EventArgs e)
    {
        StartBattle();
        bossBattleTrigger.OnplayerEnterTrigger -= BossBattleTrigger_OnPlayerEntertrigger;
    }

    private void StartBattle()
    {
        canvas.SetActive(true);
        normalAudio.Pause();
        bossAudio.Play();
    }
    private void Update()
    {
        if (finalBoss.hitpoint < 70 && count==0)
        {
            SecondPhase();
        }
        if (finalBoss.hitpoint < 40 && count1 == 0)
        {
            ThirdPahase();
        }
        if (finalBoss.hitpoint <= 0)
        {
            doorController.Reset();
            bossAudio.Stop();
            normalAudio.Play();
        }
        
    }
    private void SecondPhase()
    {
        count++;
        var spawnedEnemy = Instantiate(_EnemyPrefab, new Vector3(1.396f, 2.9f, 0f), Quaternion.identity);
        spawnedEnemy.name = $"Enemy0";
        var spawnedEnemy1 = Instantiate(_EnemyPrefab, new Vector3(1.368f, 4.517f, 0f), Quaternion.identity);
        spawnedEnemy1.name = $"Enemy1";
        var spawnedEnemy2 = Instantiate(_EnemyPrefab, new Vector3(4.502f, 4.484f, 0f), Quaternion.identity);
        spawnedEnemy2.name = $"Enemy2";
        var spawnedEnemy3 = Instantiate(_EnemyPrefab, new Vector3(4.483f, 2.798f, 0f), Quaternion.identity);
        spawnedEnemy3.name = $"Enemy3";
    }

    private void ThirdPahase()
    {
        count1++;
        var spawnedEnemy = Instantiate(_EnemyPrefab1, new Vector3(1.841f, 3.212f, 0f), Quaternion.identity);
        spawnedEnemy.name = $"Wizard0";
        var spawnedEnemy1 = Instantiate(_EnemyPrefab1, new Vector3(1.792f, 4.416f, 0f), Quaternion.identity);
        spawnedEnemy1.name = $"Wizard1";
        var spawnedEnemy2 = Instantiate(_EnemyPrefab1, new Vector3(3.824f, 4.313f, 0f), Quaternion.identity);
        spawnedEnemy2.name = $"Wizard2";
        var spawnedEnemy3 = Instantiate(_EnemyPrefab1, new Vector3(3.764f, 2.971f, 0f), Quaternion.identity);
        spawnedEnemy3.name = $"Wizard3";

    }

}
