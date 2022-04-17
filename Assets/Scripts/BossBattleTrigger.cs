using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleTrigger : Collidable
{
    public event EventHandler OnplayerEnterTrigger;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && OnplayerEnterTrigger!=null)
        {
            OnplayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}