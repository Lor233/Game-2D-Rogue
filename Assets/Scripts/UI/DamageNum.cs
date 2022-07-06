using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    public Text damageText;
    public float lifeTimer;
    public float upSpeed;
    public float upAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (upSpeed > 0)
        {
            upSpeed += upAcceleration;
        }
        transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
    }

    public void ShowUIdamage(int amount)
    {
        damageText.text = amount.ToString();
    }
}
