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

    [Header("Health")]
    public int health;
    public bool dead;

    [Header("Be Attack")]
    public int colliderDamage;
    public float flashTime;
    public GameObject bloodEffect;

    [Header("Move")]
    public float speed;
    
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
        else if (attackCdCurrent <= 0)
        {
            anim.SetFloat("speed", movement.magnitude);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(colliderDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
        
        // bloodEffect.transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), 1, 1);
        // Instantiate(bloodEffect, transform.position + new Vector3(-0.6f * Mathf.Sign(transform.localScale.x), 0.8f, 0), Quaternion.identity);
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
