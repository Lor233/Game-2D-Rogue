using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyCow : Enemy
{
    [Header("Move")]
    public float speed;
    public float startWaitTime;
    public float waitTime;
    public bool canRun;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform RightUpPos;

    [Header("Attack")]
    public float attackDistance;
    public float attackCd;
    public float attackCdCurrent;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (health > 0)
        {
            if (Vector2.Distance(transform.position, playerPos.position) <= attackDistance & !playerDead)
            {
                attack();
            }
            else
            {
                if (canRun)
                {
                    transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
                }
            }
        }

        if (Vector2.Distance(transform.position, movePos.position) < 0.001f)
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
            canRun = false;
        }
        else
        {
            transform.localScale = new Vector3((float) (Mathf.Sign(movePos.transform.position.x - transform.position.x) * 1.5), (float) 1.5, 1);
            canRun = true;
        }

        anim.SetBool("run", canRun);
    }

    Vector2 GetRandomPos()
    {
        return new Vector2(Random.Range(leftDownPos.position.x, RightUpPos.position.x),
            Random.Range(leftDownPos.position.y, RightUpPos.position.y));
    }

    void attack()
    {
        if (attackCdCurrent <= 0)
        {
            transform.localScale = new Vector3((float) (Mathf.Sign(playerPos.transform.position.x - transform.position.x) * 1.5), (float) 1.5, 1);
            anim.SetTrigger("attack");
            attackCdCurrent = attackCd;
        }
        else if (attackCdCurrent > 0)
        {
            attackCdCurrent -= Time.deltaTime;
        }
    }

}
