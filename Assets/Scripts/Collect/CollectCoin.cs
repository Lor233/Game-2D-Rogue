using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : CollectItem
{
    public int num;

    public override void Collected(ItemNum ItemNum)
    {
        SoundManager.instance.PlayPickCoin();
        ItemNum.updateCoinNum(num);
        Destroy(gameObject);
    }
}
