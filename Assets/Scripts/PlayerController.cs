using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;

    float attackCdCurrent;

    public float speed;
    public float attackCd;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0)
        {
            transform.localScale = new Vector3((float) (movement.x * 1.5), (float) 1.5, 1);
        }

        SwitchAnim();
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
}
