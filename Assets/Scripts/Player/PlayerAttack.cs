using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;

    Animator anim;
    PolygonCollider2D coli2D;

    // Start is called before the first frame update
    void Start()
    {
        // anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        // coli2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage, transform.parent.position.x);
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            other.GetComponent<Item>().TakeDamage(1);
        }
    }
}
