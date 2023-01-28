using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyWizard : Enemy
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
    public GameObject bulletPrefab;

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

    void BulletFour()
    {
        GameObject bullet_0 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet_0.GetComponent<WizardBullet>().id = 0;

        GameObject bullet_1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet_1.GetComponent<WizardBullet>().id = 1;

        //GameObject bullet_2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //bullet_2.GetComponent<WizardBullet>().id = 2;

        GameObject bullet_3 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet_3.GetComponent<WizardBullet>().id = 3;

        GameObject bullet_4 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet_4.GetComponent<WizardBullet>().id = 4;
    }

}
