using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GetDataFromDatabase dataFromDatabase;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(menu);
            Destroy(hud);
            Destroy(floatingTextManager);
            return;

        }
        joystick.joystickOriginalPos = joystick.joystick.transform.position;
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("gameQuality"));

    }

    private void Start()
    {
        DeactivateGameObjects();
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;
    public CharacterMenu characterMenu;
    public JoystickMovement joystick;
    public GameObject attackZone;
    public int recentlykilledEnemyes;
    public int killedEnemyes;
    public int recentlyplayerDeaths;
    public int playerDeaths;
    public int skinId;
    public int pesos;
    public int experience;
    public float playedTime;
    public float recentlyPlayedTime;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void ActivateGameObjects()
    {
        hud.SetActive(true);
        floatingTextManager.gameObject.SetActive(true);
        joystick.gameObject.SetActive(true);
        attackZone.SetActive(true);
    }

    public void DeactivateGameObjects()
    {
        GameObject.Find("HUD").SetActive(false);
        floatingTextManager.gameObject.SetActive(false);
        joystick.gameObject.SetActive(false);
        attackZone.SetActive(false);
    }


    public bool TryUpgradeWeapon(int money,List<int> weaponPrices,Weapon weapon)
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (money >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    public int GetCurrentLevel(int experience,List<int> xpTable)
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }
        return r;
    }

    public int GetXpToLevel(int level,List<int> xpTable)
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel(GameManager.instance.experience, GameManager.instance.xpTable);
        experience += xp;
        if (currLevel < GetCurrentLevel(GameManager.instance.experience, GameManager.instance.xpTable))
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        ActivateGameObjects();
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        joystick.PointerUp();
    }

    public void Respawn()
    {
        deathMenuAnim.SetTrigger("hide");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.Respawn();
    }

    public string activeScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void LoadState()
    {
        pesos = dataFromDatabase.pesos;
        experience = dataFromDatabase.experience;
        player.SetLevel(GetCurrentLevel(GameManager.instance.experience, GameManager.instance.xpTable));
        weapon.SetWeaponLevel(dataFromDatabase.weaponLevel);
        player.hitpoint = dataFromDatabase.health;
        playedTime = dataFromDatabase.playedTime;
        skinId = dataFromDatabase.skin;
        player.SwapSprite(skinId);
        characterMenu.characterSelectionSprite.sprite = playerSprites[skinId];
        killedEnemyes = dataFromDatabase.killedEnemys;
        playerDeaths = dataFromDatabase.playerDeaths;
        AudioListener.volume = dataFromDatabase.musicVolume;
        QualitySettings.SetQualityLevel(dataFromDatabase.gameQuality);
    }

}
