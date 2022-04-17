using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : EnemyHitbox
{
    public bool active;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && active)
        {
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
