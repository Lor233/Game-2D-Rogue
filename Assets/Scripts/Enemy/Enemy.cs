using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform playerPos;
    public Transform transParent;
    public Animator anim;
    public bool playerDead;

    [Header("Health")]
    public float health;
    public bool dead;
    public HealthBar healthBar;

    [Header("Be Attack")]
    public bool isAttack;
    public float flashTime;
    public Transform effectPoint;
    public GameObject bloodEffect;
    public GameObject damageCanvas;

    [Header("Drop")]
    public GameObject coin;

    protected Rigidbody2D rb;
    SpriteRenderer sr;
    Color originalColor;
    GameObject player;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        originalColor = sr.color;
        healthBar.maxHp = health;
        healthBar.hp = health;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        if (health > 0)
        {
            playerPos = player.transform;
            playerDead = player.GetComponent<PlayerController>().dead;

            if (transParent.position.x <= playerPos.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }

        if (dead)
        {
            // Destroy(gameObject);
        }
    }

    public void Knockback(Vector3 other, float KnockbackInt = 0)
    {
        Vector2 difference = transParent.position - other;
        difference.Normalize();
        rb.velocity = difference * (0.25f + KnockbackInt);
        // transParent.position = new Vector2(transParent.position.x + difference.x * (0.17f + KnockbackInt),
        //                                         transParent.position.y + difference.y * (0.17f + KnockbackInt));
    }

    public void TakeDamage(int damage, float otherX)
    {
        if (health > 0 && !isAttack)
        {
            health -= damage;
            healthBar.hp = health;
            FlashColor(flashTime);
            // Hit Audio
            SoundManager.instance.PlayHitSword();
            if (health <= 0)
            {
                SoundManager.instance.PlayKillSword();
            }
            // Blood effect
            float directionX = Mathf.Sign(otherX - transParent.position.x);
            bloodEffect.transform.localScale = new Vector3(directionX, 1, 1);
            Instantiate(bloodEffect, effectPoint.position, Quaternion.identity);
            // Damage number
            DamageNum damageNum = Instantiate(damageCanvas, effectPoint.position, Quaternion.identity).GetComponent<DamageNum>();
            damageNum.ShowUIdamage(damage);
            // Invincible time
            StartCoroutine(InvincibleCo());
        }
        if (health <= 0)
        {
            Instantiate(coin, transParent.position, Quaternion.identity);
        }
    }

    void FlashColor(float time)
    {
        sr.material.SetFloat("_FlashAmount", 0.7f);

        if (health > 0)
        {
            anim.SetTrigger("be_attack");
            Invoke("ResetColor", time);
        }
        else
        {
            anim.SetTrigger("dead");
            Invoke("DeadColor", time);
            transParent.parent.gameObject.GetComponent<EnemyManager>().enemys -= 1;
        }
    }

    void CloseHealthbar()
    {
        healthBar.gameObject.SetActive(false);
    }

    void ResetColor()
    {
        sr.material.SetFloat("_FlashAmount", 0);
    }

    void DeadColor()
    {
        sr.material.SetFloat("_FlashAmount", 0);
        sr.color = new Color32(145, 145, 145, 255);
    }

    IEnumerator InvincibleCo()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.3f);
        isAttack = false;
    }

}
