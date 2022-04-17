using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetDataFromDatabase : MonoBehaviour
{
    public int health;
    public int pesos;
    public int experience;
    public int weaponLevel;
    public int gameQuality;
    public float musicVolume;
    public string currentScene;
    public float playedTime;
    public int skin;
    public int killedEnemys;
    public int playerDeaths;

    public void Awake()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            PlayerData data = SaveSystem.LoadPlayer();
            health = data.health;
            pesos = data.pesos;
            experience = data.experience;
            weaponLevel = data.weaponLevel;
            gameQuality = data.gameQuality;
            musicVolume = data.musicVolume;
            currentScene = data.lastscene;
            playedTime = data.playedTime;
            skin = data.skin;
            killedEnemys = data.killedEnemys;
            playerDeaths = data.playerDeaths;
            File.Delete(path);
        }
        else
        {
            StartCoroutine(getData());
        }
    }

    IEnumerator getData()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetString("playerID"));
        UnityWebRequest www = UnityWebRequest.Post("https://adungeongame.000webhostapp.com/GetDataFromDatabase.php", form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text[0] == 'E')
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            string s = www.downloadHandler.text;
            health = int.Parse(s.Split('-')[0]);
            pesos = int.Parse(s.Split('-')[1]);
            experience = int.Parse(s.Split('-')[2]);
            weaponLevel = int.Parse(s.Split('-')[3]);
            gameQuality = int.Parse(s.Split('-')[6]);
            musicVolume = float.Parse(s.Split('-')[7]);
            currentScene = s.Split('-')[5];
            playedTime = float.Parse(s.Split('-')[4]);
            skin = int.Parse(s.Split('-')[8]);
            killedEnemys = int.Parse(s.Split('-')[9]);
            playerDeaths = int.Parse(s.Split('-')[10]);
        }
    }
}