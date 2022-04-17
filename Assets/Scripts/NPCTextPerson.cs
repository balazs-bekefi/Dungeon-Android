using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextPerson : Collidable
{
    public string message;
    public string message1;

    private float cooldown = 1.5f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = -cooldown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && Time.time - lastShout > cooldown && gameObject.name == "IntroNPC" || coll.name == "weapon_0" && Time.time - lastShout > cooldown && gameObject.name == "IntroNPC")
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message + PlayerPrefs.GetString("playerName") + message1, 30, Color.cyan, transform.position + new Vector3(0, 0.30f, 0), Vector3.zero, cooldown);
        }
        else if (coll.name == "Player" && Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 30, Color.cyan, transform.position + new Vector3(0, 0.30f, 0), Vector3.zero, cooldown);
        }
    }
}
