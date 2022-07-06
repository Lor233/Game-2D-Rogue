using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Health")]
    public float health;
    public bool dead;

    [Header("Be Attack")]
    public float flashTime;

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
        if (dead)
        {
            sr.color = new Color32(145, 145, 145, 255);
            // Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;

            if (health > 0)
            {
                anim.SetTrigger("be_attack");
            }
            else
            {
                anim.SetTrigger("dead");
            }
            FlashColor(flashTime);
        }
    }

    void FlashColor(float time)
    {
        // sr.color = Color.white;
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
}
