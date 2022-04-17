using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private DoorTrigger doorTrigger;
    [SerializeField] private BossBattleTrigger bossBattleTrigger;
    public List<Sprite> doorSkins;
    public List<Sprite> pressureplateSkin;
    public SpriteRenderer spriteRenderer;
    public List <GameObject> doorBlock;

    private void Start()
    {
        bossBattleTrigger.OnplayerEnterTrigger += BossBattleTrigger_OnPlayerEntertrigger;
    }

    private void BossBattleTrigger_OnPlayerEntertrigger(object sender, EventArgs e)
    {
        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        spriteRenderer.sprite = doorSkins[0];
        doorTrigger.spriteRenderer.sprite = pressureplateSkin[0];
        doorTrigger.door = false;
        bossBattleTrigger.OnplayerEnterTrigger -= BossBattleTrigger_OnPlayerEntertrigger;
    }

    private void Update()
    {
        if (doorTrigger.door == true)
        {
            doorTrigger.spriteRenderer.sprite = pressureplateSkin[1];
            spriteRenderer.sprite = doorSkins[1];
            doorBlock[0].SetActive(false);
            doorBlock[1].SetActive(false);
        }
    }
    public void Reset()
    {
        doorTrigger.spriteRenderer.sprite = pressureplateSkin[1];
        spriteRenderer.sprite = doorSkins[1];
        doorBlock[0].SetActive(false);
        doorBlock[1].SetActive(false);
    }
}
