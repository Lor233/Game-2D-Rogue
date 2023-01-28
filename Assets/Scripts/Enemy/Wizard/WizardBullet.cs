using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : MonoBehaviour
{
    public int id;
    public float bulletSpeed = 3;
    public int damage = 3;

    public GameObject BulletEffect;

    public void Update()
    {
        if (id == 0)
            transform.Translate(-bulletSpeed * Time.deltaTime, bulletSpeed * Time.deltaTime, 0);
        if (id == 1)
            transform.Translate(bulletSpeed * Time.deltaTime, bulletSpeed * Time.deltaTime, 0);
        //if (id == 2)
            //transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
        if (id == 3)
            transform.Translate(-bulletSpeed * Time.deltaTime, -bulletSpeed * Time.deltaTime, 0);
        if (id == 4)
            transform.Translate(bulletSpeed * Time.deltaTime, -bulletSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Door"))
        {
            Instantiate(BulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(BulletEffect, transform.position, Quaternion.identity);
            other.GetComponent<PlayerController>().TakeDamage(damage, transform.position.x);
            // Knockback effect
            other.GetComponent<PlayerController>().Knockback(transform.position);
            Destroy(gameObject);

            // FindObjectOfType<CameraController>().CameraShake(0.5f);
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            other.GetComponent<Item>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
