using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform playerPos;
    public bool playerDead;

    [Header("Health")]
    public float health;
    public bool dead;

    [Header("Be Attack")]
    // public int damage;
    public float flashTime;
    public GameObject bloodEffect;
    public GameObject damageCanvas;
    
    Animator anim;
    SpriteRenderer sr;
    Color originalColor;

    // Start is called before the first frame update
    public void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        GetComponentInChildren<HealthBar>().maxHp = health;
        GetComponentInChildren<HealthBar>().hp = health;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health > 0)
        {
            playerPos = GameObject.Find("Player").transform;
            playerDead = GameObject.Find("Player").GetComponent<PlayerController>().dead;
        }

        if (dead)
        {
            // Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            GetComponentInChildren<HealthBar>().hp = health;
            FlashColor(flashTime);

            float localx = Mathf.Sign(transform.localScale.x);
            bloodEffect.transform.localScale = new Vector3(localx, 1, 1);
            Instantiate(bloodEffect, transform.position + new Vector3(-0.6f * localx, 0.8f, 0), Quaternion.identity);
            
            DamageNum damageNum = Instantiate(damageCanvas, transform.position + new Vector3(-1.2f * localx, 1.0f, 0), Quaternion.identity).GetComponent<DamageNum>();
            damageNum.ShowUIdamage(damage);
        }
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
            sr.color = new Color32(145, 145, 145, 255);
            transform.parent.parent.gameObject.GetComponent<EnemyManager>().enemys -= 1;
        }

        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
}
