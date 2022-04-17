using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : Enemy
{
    public float[] fireballSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public Transform[] fireballs;
    public Slider slider;
    public Animator animator;

    private void Update()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
        slider.value = hitpoint;            
    }
    protected override void FixedUpdate()
    {
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
            {
                if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                    chasing = true;

                if (chasing)
                {
                    if (!collidingWithPlayer)
                    {
                        UpdateMotor((playerTransform.position - transform.position).normalized);
                        animator.Play("finalBoss_idle");
                    }
                }
                else
                {
                    UpdateMotor(startingPosition - transform.position);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
                chasing = false;
                animator.Play("finalBoss_idle");
            }

            collidingWithPlayer = false;
            boxCollider.OverlapCollider(filter, hits);
            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i] == null)
                    continue;

                if (hits[i].tag == "Fighter" && hits[i].name == "Player")
                {
                    collidingWithPlayer = true;
                    animator.Play("finalBoss_Attack");
                }

                hits[i] = null;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, triggerLenght);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseLength);
    }
}
