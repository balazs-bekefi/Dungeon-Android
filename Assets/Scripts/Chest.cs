using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    public AudioSource chestOpen;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + "  gold!", 30, Color.yellow, transform.position + new Vector3(0, 0.20f, 0), Vector3.up * 25, 1.7f);
            chestOpen.Play();
        }
    }
}
