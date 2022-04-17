using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : Collidable
{
    public event EventHandler DoorEventTrigger;

    public SpriteRenderer spriteRenderer;

    public bool door=false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && door == false)
        {
            door = true;
        }
    }
}