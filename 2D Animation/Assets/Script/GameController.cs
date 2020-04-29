using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private float mGold = 100;
    public void UseGold(float amount, Delegates.VoidCallback callback=null)
    {
        if (mGold >= amount)
        {
            mGold -= amount;
            if (callback != null)//델리게이트(callback 메서드)가 연결되어있는지 아닌지
            {
                callback();
            }
            
        }
        else
        {
            Debug.Log("You not enough Gold");
        }
    }
    /* 델리게이트 사용 시
    public Delegates.VoidCallback GoldSpendCallback { get; set; }
    public float Gold
    {
        get
        {
            return mGold;
        }
        set
        {
            if (value>=0)
            {
                mGold = value;
                if (GoldSpendCallback !=null)
                {
                    GoldSpendCallback();
                    GoldSpendCallback = null;
                }
            }
            else
            {
                GoldSpendCallback = null;
            }
        }
    }
    */
}
