using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class Test1
{

    [Test]
    public void GetXpToLevelTest()
    {
        GameManager gameManager = new GameManager();

        int level = 3;
        List<int> xptable = new List<int> { 5, 10, 16, 25, 33 };
        int expected = 31;
        int actual = gameManager.GetXpToLevel(level, xptable);
        Assert.AreEqual(expected, actual);

    }

    [Test]
    public void activeSceneTest()
    {
        GameManager gameManager = new GameManager();
        EditorSceneManager.OpenScene("Assets/Scenes/LoginScene.unity");
        string expected = "LoginScene";
        string actual = gameManager.activeScene();
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void GetCurrentLevelTest()
    {
        GameManager gameManager = new GameManager();
        int xp = 40;
        List<int> xptable = new List<int> { 5, 10, 16, 25, 33 };
        int expected = 4;
        int actual = gameManager.GetCurrentLevel(xp, xptable);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TryUpgradeWeaponTest()
    {
        GameManager gameManager = new GameManager();
        Weapon weapon = new Weapon();
        int money = 65;
        List<int> weaponPrices = new List<int> { 10, 30, 60, 90, 150, 220 };
        weapon.weaponLevel = 3;
        bool expected = false;
        bool actual = gameManager.TryUpgradeWeapon(money, weaponPrices, weapon);
        Assert.AreEqual(expected, actual);
    }
}
