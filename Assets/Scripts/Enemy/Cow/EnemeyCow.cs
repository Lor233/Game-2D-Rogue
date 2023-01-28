using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyCow : Enemy
{
    [Header("Move")]
    public float speed;
    public float startWaitTime;
    public float waitTime;
    public bool canAct;

    [Header("Attack")]
    public float attackDistance;
    public float attackCd;
    public float attackCdCurrent;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        waitTime = startWaitTime;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (health > 0 && !playerDead && canAct)
        {
            if (Vector2.Distance(transParent.position, playerPos.position) <= attackDistance && attackCdCurrent <= 0)
            {
                Attack();
            }
            else
            {
                Move();
                anim.SetBool("run", canAct);
                if (attackCdCurrent > 0)
                    attackCdCurrent -= Time.deltaTime;
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("attack");
        attackCdCurrent = attackCd;
    }

    void Move()
    {
        transParent.position = Vector2.MoveTowards(transParent.position, playerPos.position, speed * Time.deltaTime);
    }

}
