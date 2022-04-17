using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    public List<RuntimeAnimatorController> controller;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Death()
    {
        GameManager.instance.recentlyplayerDeaths++;
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("show");
    }

    private void FixedUpdate()
    {
        float x = GameManager.instance.joystick.joystickVec.x;
        float y = GameManager.instance.joystick.joystickVec.y;
        if (isAlive)
        {
            animator.SetFloat("Horizontal", x);
            UpdateMotor(new Vector3(x, y, 0));
        }
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
        animator.runtimeAnimatorController = controller[skinId] as RuntimeAnimatorController;
    }

    public void OnLevelUp()
    {
        maxHitpoint=maxHitpoint+2;
        hitpoint = maxHitpoint;
        GameManager.instance.ShowText("Elérted a " + GameManager.instance.GetCurrentLevel(GameManager.instance.experience, GameManager.instance.xpTable) + ". szintet!", 28, Color.cyan, transform.position, Vector3.up * 12, 1.5f);
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
            return;

        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        if (hitpoint < maxHitpoint)
        {
            hitpoint = hitpoint + healingAmount;
            GameManager.instance.OnHitpointChange();
            GameManager.instance.ShowText("+" + healingAmount.ToString() + "  életerõ", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
            if (hitpoint > maxHitpoint)
                hitpoint = maxHitpoint;
        }
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
        GameManager.instance.OnHitpointChange();
    }
}
