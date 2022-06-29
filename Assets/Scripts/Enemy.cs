using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform playerPos;
    public bool playerDead;

    [Header("Health")]
    public int health;
    public bool dead;

    [Header("Be Attack")]
    public int damage;
    public float flashTime;
    public GameObject bloodEffect;
    
    Animator anim;
    SpriteRenderer sr;
    Color originalColor;

    // Start is called before the first frame update
    public void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        playerPos = GameObject.Find("Player").transform;
        playerDead = GameObject.Find("Player").GetComponent<PlayerController>().dead;

        if (dead)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);

        bloodEffect.transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), 1, 1);
        Instantiate(bloodEffect, transform.position + new Vector3(-0.6f * Mathf.Sign(transform.localScale.x), 0.8f, 0), Quaternion.identity);
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
