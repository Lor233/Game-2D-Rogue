using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
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
}
