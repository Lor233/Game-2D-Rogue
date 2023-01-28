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

    public bool canAct;

    [Header("Health")]
    public int health;
    public bool dead;
    public HealthBarPlayer hpBar;
    public GameOverMenu GameOverMenu;
    

    [Header("Be Attack")]
    public bool isAttack;
    public int colliderDamage;
    public float flashTime;
    public Transform effectPoint;
    public GameObject bloodEffect;
    public GameObject damageCanvas;

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

        // hpBar = GetComponentInParent<HealthBarPlayer>();
        // hpBar = GameObject.Find("Canvas/HealthBarPlayer").GetComponent<HealthBarPlayer>();
        hpBar.maxHp = health;
        hpBar.hp = health;

        GameOverMenu.GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !GameOverMenu.GameIsPaused)
        {
            // movement.x = 0;
            // movement.y = 0;
            // GameOverMenu.Pause();
            // Destroy(gameObject);
        }
        if (health > 0)
        {
            MoveGet();
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (attackCdCurrent <= 0 && canAct)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    void MoveGet()
    {
        if (canAct)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
                sr.flipX = false;
            if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
                sr.flipX = true;
        }
    }

    void Attack()
    {
        if (Input.GetKey("space") && attackCdCurrent <= 0)
        {
            anim.SetTrigger("attack");
            attackCdCurrent = attackCd;
            movement.x = 0;
            movement.y = 0;
        }
        else if (attackCdCurrent > 0)
        {
            attackCdCurrent -= Time.deltaTime;
        }
        else if (attackCdCurrent <= 0 && canAct)
        {
            anim.SetFloat("speed", movement.magnitude);
        }
    }

    public void Knockback(Vector3 other, float KnockbackInt = 0)
    {
        Vector2 difference = transform.position - other;
        difference.Normalize();
        rb.velocity = difference * (0.25f + KnockbackInt);
        // transform.position = new Vector2(transform.position.x + difference.x * (0.25f + KnockbackInt),
        //                                         transform.position.y + difference.y * (0.25f + KnockbackInt));
    }

    public void TakeDamage(int damage, float otherX)
    {
        if (health > 0 && !isAttack)
        {
            health -= damage;
            hpBar.hp = health;
            // Direction
            if (transform.position.x < otherX)
                transform.eulerAngles = new Vector3(0, 0, 0);
            if (transform.position.x > otherX)
                transform.eulerAngles = new Vector3(0, 180, 0);
            // Hit audio
            SoundManager.instance.PlayBeAttack();
            // White flash
            FlashColor(flashTime);
            // Camera shake
            StartCoroutine(FindObjectOfType<CameraFollow>().CameraShakeCo(0.1f, 0.1f));
            // Blood effect
            float directionX = Mathf.Sign(otherX - transform.position.x);
            bloodEffect.transform.localScale = new Vector3(directionX, 1, 1);
            Instantiate(bloodEffect, effectPoint.position, Quaternion.identity);
            // Damage number
            DamageNum damageNum = Instantiate(damageCanvas, transform.position + new Vector3(-0.5f * directionX, 0.7f, 0), Quaternion.identity).GetComponent<DamageNum>();
            damageNum.ShowUIdamage(damage);
            // Invincible time
            StartCoroutine(InvincibleCo(0.5f));
        }
    }

    void FlashColor(float time)
    {
        // sr.color = Color.red;
        sr.material.SetFloat("_FlashAmount", 0.7f);

        if (health > 0)
        {
            anim.SetTrigger("be_attack");
        }
        else
        {
            sr.material.SetFloat("_FlashAmount", 0);
            anim.SetTrigger("dead");
        }

        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        // sr.color = originalColor;
        sr.material.SetFloat("_FlashAmount", 0);
    }

    IEnumerator InvincibleCo(float time)
    {
        isAttack = true;
        yield return new WaitForSeconds(time);
        isAttack = false;
    }

    void GameOver()
    {
        GameOverMenu.Pause();
    }
}
