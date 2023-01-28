using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public ItemNum itemNum;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CollectItem"))
        {
            other.GetComponent<CollectItem>().Collected(itemNum);
        }
    }
}
