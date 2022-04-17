using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : EnemyHitbox
{
    public float bulletSpeed;
    public float lifetime;
    public AudioSource fireballSound;

    protected override void Start()
    {
        base.Start();
        Invoke("DestroyProjectile", lifetime);
        fireballSound.Play();
    }

    protected override void Update()
    {
        
    }
    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
        DestroyProjectile();
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            if (hits[i].name == "Player")
            {
                OnCollide(hits[i]);
            }else if(hits[i].name == "Collision")
            {
                DestroyProjectile();
            }
            else
            {
                continue;
            }

            hits[i] = null;
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
