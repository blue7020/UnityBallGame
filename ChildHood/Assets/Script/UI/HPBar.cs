using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    private Image mHPBar;
    //변경점을 확인하기 위해
    private float mNowHP;
    private float mNowMaxHP;
    private string value;

    private void Awake()
    {
        mHPBar = GetComponent<Image>();
        mNowHP = mPlayer.mCurrentHP;
        mNowMaxHP = mPlayer.mMaxHP;
        mHPBar.fillAmount = mNowHP/ mNowMaxHP;

    }

    private void Update()
    {
        mHPBar.fillAmount = mNowHP / mNowMaxHP;
        if (mNowHP!=mPlayer.mCurrentHP)
        {
            mNowHP = mPlayer.mCurrentHP;
        }
        else if (mNowMaxHP != mPlayer.mCurrentHP)
        {
            mNowMaxHP = mPlayer.mMaxHP;
        }
    }

}
