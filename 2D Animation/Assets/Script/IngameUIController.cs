using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : MonoBehaviour
{
    [SerializeField]
    private Text mCoinText;
    /*
    public float Coin
    {
    set
    {
    mCoinText.text = value.Tostring();
    }
    }
    */
    //두 개의 기능은 같지만 이것이 낫다
    public void ShowCoin(float value)
    {
        mCoinText.text = value.ToString();
    }
}
