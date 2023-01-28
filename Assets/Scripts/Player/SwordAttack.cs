using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Animator animWeapon;
    
    private float flipY;

    void Start()
    {
        flipY = transform.localScale.y;
    }

    void Update()
    {
        // Rotation sword
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        // Flip sword
        if (difference.x > 0)
            transform.localScale = new Vector3(flipY, flipY, 1);
        else
            transform.localScale = new Vector3(flipY, -flipY, 1);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        SoundManager.instance.PlaySwingSword();
        transform.GetChild(0).gameObject.SetActive(true);
        animWeapon.SetTrigger("attack");
    }
}
