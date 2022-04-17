using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveTable : Collidable
{
    public SaveToDatabase save;
    private string message="Üss rá a táblára hogy elmentsd a játékállásod!";
    public GameObject text;

    private float cooldown = 4.0f;
    private float lastShout;
    private float lastSave;

    protected override void Start()
    {
        base.Start();
        lastShout = -cooldown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "weapon_0" && Time.time-lastShout>cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.cyan, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
        if (coll.name == "weapon_0" && Time.time - lastSave > cooldown)
        {
            lastSave = Time.time;
            save.Save();
            StartCoroutine(ActivationRoutine());

        }
    }
    private IEnumerator ActivationRoutine()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(3);
        text.SetActive(false);
    }
}
