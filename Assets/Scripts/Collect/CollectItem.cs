using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectItem : MonoBehaviour
{
    public abstract void Collected(ItemNum ItemNum);
}
