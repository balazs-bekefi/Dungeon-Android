using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using UnityEngine.Networking;
using System;
using System.IO;

public class SaveToDatabase : MonoBehaviour
{
    public void Save()
    {
        string id = PlayerPrefs.GetString("playerID");
        int pesos = GameManager.instance.pesos;
        int experience = GameManager.instance.experience;
        int weaponlevel = GameManager.instance.weapon.weaponLevel;
        int health = GameManager.instance.player.hitpoint;
        float fullplayedTime = GameManager.instance.playedTime + GameManager.instance.recentlyPlayedTime;
        string playedTime = fullplayedTime.ToString("0.0000");
        string musicVolume = PlayerPrefs.GetFloat("musicVolume").ToString("0.0000");
        int gamequality = PlayerPrefs.GetInt("gameQuality");
        string lastscene = SceneManager.GetActiveScene().name;
        int skin = GameManager.instance.skinId;
        int killedEnemys = GameManager.instance.killedEnemyes + GameManager.instance.recentlykilledEnemyes;
        int playerDeaths = GameManager.instance.playerDeaths + GameManager.instance.recentlyplayerDeaths;
        StartCoroutine(AddData(id, pesos, experience, weaponlevel, health, playedTime, musicVolume, gamequality, lastscene, skin, killedEnemys, playerDeaths));
    }

    IEnumerator AddData(string id, int pesos, int experience, int weaponlevel, int health, string playedTime, string musicVolume, int gamequality, string lastscene, int skin, int killedEnemyes, int playerDeaths)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("health", health);
        form.AddField("pesos", pesos);
        form.AddField("experience", experience);
        form.AddField("weaponlevel", weaponlevel);
        form.AddField("playedtime", playedTime);
        form.AddField("lastscene", lastscene);
        form.AddField("gamequality", gamequality);
        form.AddField("musicvolume", musicVolume);
        form.AddField("skinid", skin);
        form.AddField("killedenemyes", killedEnemyes);
        form.AddField("playerdeaths", playerDeaths);

        UnityWebRequest www = UnityWebRequest.Post("https://adungeongame.000webhostapp.com/SavePlayerData.php", form);
        yield return www.SendWebRequest();
        try
        {
            if (www.downloadHandler.text[0] == '0')
            {
                string path = Application.persistentDataPath + "/player.fun";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            else
            {
                SaveSystem.SavePlayer(GameManager.instance);
            }
        }
        catch (IndexOutOfRangeException)
        {
            SaveSystem.SavePlayer(GameManager.instance);
        }
    }
}
