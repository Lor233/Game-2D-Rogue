using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool playerPos;
    public int enemys;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = false;
        enemys = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemys == 0)
        {
            transform.parent.gameObject.GetComponent<Room>().complete = true;
        }
    }
}
