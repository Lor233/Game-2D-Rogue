using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public GameObject hitEffect;
    public Transform hitTrans;

    Animator anim;
    PolygonCollider2D coli2D;

    public void EndAttack() // This Function is Called inside Animation End Frame
    {
       gameObject.SetActive(false);
    } 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage, transform.parent.position.x);
            // Camera shake
            StartCoroutine(FindObjectOfType<CameraFollow>().CameraShakeCo(0.1f, 0.01f));
            // Hit effect
            Instantiate(hitEffect, hitTrans.position, Quaternion.identity);
            // Knockback effect
            other.GetComponent<Enemy>().Knockback(transform.parent.position);
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            other.GetComponent<Item>().TakeDamage(1);
        }
    }
}
