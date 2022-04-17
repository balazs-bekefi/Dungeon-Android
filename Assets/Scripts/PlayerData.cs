using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int pesos;
    public int experience;
    public int weaponLevel;
    public int gameQuality;
    public float musicVolume;
    public float[] position;
    public float playedTime;
    public float fullplayedTime;
    public string lastscene;
    public int skin;
    public int killedEnemys;
    public int playerDeaths;

    public PlayerData (GameManager gameManager)
    {
        string id = PlayerPrefs.GetString("playerID");
        pesos = GameManager.instance.pesos;
        experience = GameManager.instance.experience;
        weaponLevel = GameManager.instance.weapon.weaponLevel;
        health = GameManager.instance.player.hitpoint;
        fullplayedTime = GameManager.instance.playedTime + GameManager.instance.recentlyPlayedTime;
        playedTime = fullplayedTime;
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        gameQuality = PlayerPrefs.GetInt("gameQuality");
        lastscene = SceneManager.GetActiveScene().name;
        skin = GameManager.instance.skinId;
        killedEnemys = GameManager.instance.killedEnemyes + GameManager.instance.recentlykilledEnemyes;
        playerDeaths = GameManager.instance.playerDeaths + GameManager.instance.recentlyplayerDeaths;
        position = new float[3];
        position[0] = gameManager.player.transform.position.x;
        position[1] = gameManager.player.transform.position.y;
        position[2] = gameManager.player.transform.position.z;
    }

}
