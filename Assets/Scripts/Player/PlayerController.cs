using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Vector2 movement;
    Color originalColor;
    HealthBarPlayer hpBar;

    [Header("Health")]
    public int health;
    public bool dead;

    [Header("Be Attack")]
    public int colliderDamage;
    public float flashTime;
    public GameObject bloodEffect;
    public GameObject damageCanvas;

    [Header("Move")]
    public float speed;
    public bool canRun;
    
    [Header("Attack")]
    public float attackCd;
    public float attackCdCurrent;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        // hpBar = GetComponentInParent<HealthBarPlayer>();
        hpBar = GameObject.Find("Canvas/HealthBarPlayer").GetComponent<HealthBarPlayer>();
        hpBar.maxHp = health;
        hpBar.hp = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            movement.x = 0;
            movement.y = 0;
            // Destroy(gameObject);
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement.x != 0)
            {
                transform.localScale = new Vector3((float) (movement.x * 1.5), (float) 1.5, 1);
            }

            SwitchAnim();
        }
    }

    private void FixedUpdate()
    {
        if (attackCdCurrent <= 0)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    void SwitchAnim()
    {
        if (Input.GetKey("space") & attackCdCurrent <= 0)
        {
            anim.SetTrigger("attack");
            attackCdCurrent = attackCd;
        }
        else if (attackCdCurrent > 0)
        {
            attackCdCurrent -= Time.deltaTime;
        }
        else if (attackCdCurrent <= 0 && canRun)
        {
            anim.SetFloat("speed", movement.magnitude);
        }
    }

    // public void TakeDamage(int damage)
    // {
    //     health -= damage;
    //     hpBar.hp = health;
    //     FlashColor(flashTime);
        
    //     float localx = Mathf.Sign(transform.localScale.x);
    //     bloodEffect.transform.localScale = new Vector3(localx, 1, 1);
    //     Instantiate(bloodEffect, transform.position + new Vector3(-0.6f * localx, 0.8f, 0), Quaternion.identity);

    //     DamageNum damageNum = Instantiate(damageCanvas, transform.position + new Vector3(-0.6f * localx, 1.0f, 0), Quaternion.identity).GetComponent<DamageNum>();
    //     damageNum.ShowUIdamage(damage);
    // }

    public void TakeDamage(int damage)
    {
        health -= damage;
        hpBar.hp = health;
        FlashColor(flashTime);
        
        float localx = Mathf.Sign(transform.localScale.x);
        bloodEffect.transform.localScale = new Vector3(localx, 1, 1);
        Instantiate(bloodEffect, transform.position + new Vector3(-0.6f * localx, 0.8f, 0), Quaternion.identity);

        DamageNum damageNum = Instantiate(damageCanvas, transform.position + new Vector3(-0.6f * localx, 1.0f, 0), Quaternion.identity).GetComponent<DamageNum>();
        damageNum.ShowUIdamage(damage);
    }

    void FlashColor(float time)
    {
        // sr.color = Color.white;
        sr.color = Color.red;

        if (health > 0)
        {
            anim.SetTrigger("be_attack");
        }
        else
        {
            anim.SetTrigger("dead");
        }

        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
}
