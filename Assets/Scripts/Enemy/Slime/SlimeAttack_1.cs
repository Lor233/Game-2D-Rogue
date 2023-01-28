using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack_1 : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage, transform.parent.position.x);
            // Knockback effect
            other.GetComponent<PlayerController>().Knockback(transform.parent.position);
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            other.GetComponent<Item>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
