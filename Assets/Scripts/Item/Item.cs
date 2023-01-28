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
    public bool isAttack;

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
        if (health > 0 && !isAttack)
        {
            health -= damage;
            // Hit audio
            SoundManager.instance.PlayHitWood();

            if (health > 0)
            {
                
                anim.SetTrigger("be_attack");
            }
            else
            {
                SoundManager.instance.PlayBrokenWood();
                anim.SetTrigger("dead");
            }
            FlashColor(flashTime);
            StartCoroutine(InvincibleCo());
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
        }
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
