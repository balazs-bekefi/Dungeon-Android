using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    public float atttackRange = 2;
    private bool canAttack;
    private float lastAttack;
    public float attackDelay = 1.0f;
    public Animator animator;
    public GameObject projectile;
    public Transform shotPoint;
    public Transform gun;

    protected override void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                chasing = true;

            if (chasing && Vector3.Distance(playerTransform.position, transform.position) > atttackRange)
            {
                UpdateMotor((playerTransform.position - transform.position).normalized);
                canAttack = false;
            }
            else if (chasing && Vector3.Distance(playerTransform.position, transform.position) <= atttackRange)
            {
                float facing = playerTransform.position.x - transform.position.x;
                if (facing < 0)
                {
                    canAttack = true;
                    animator.SetFloat("Facing", facing);
                }
                else
                {
                    canAttack = true;
                    animator.SetFloat("Facing", facing);
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
            canAttack = false;
            animator.SetFloat("Facing", 0);
        }
        if (canAttack && Time.time > lastAttack + attackDelay)
        {
            Vector3 difference = playerTransform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            shotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            Instantiate(projectile, shotPoint.position, shotPoint.transform.rotation);
            lastAttack = Time.time;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, triggerLenght);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseLength);
    }
}
