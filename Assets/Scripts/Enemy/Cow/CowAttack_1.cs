using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAttack_1 : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage, transform.parent.position.x);
        }
    }
}
