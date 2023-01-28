using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNum : MonoBehaviour
{
    [Header("Coin")]
    public Text cNumText;
    public int cInitNum;
    public static int cCurrentNum;

    // Start is called before the first frame update
    void Start()
    {
        updateCoinNum(cInitNum);
    }

    public void updateCoinNum(int num)
    {
        cCurrentNum += num;
        cNumText.text = cCurrentNum.ToString();
    }
}
