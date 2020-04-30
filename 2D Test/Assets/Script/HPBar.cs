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

    void Start()
    {
        mHPBar = GetComponent<Image>();
        mNowHP = mPlayer.mCurrentHP;
        mNowMaxHP = mPlayer.mMaxHP;
        mHPBar.fillAmount = mNowMaxHP / 10f;
    }

    private void FixedUpdate()
    {
        if (mNowHP!=mPlayer.mCurrentHP)
        {
            mHPBar.fillAmount = mNowHP / 10f;
            mNowHP = mPlayer.mCurrentHP;
        }
        else if (mNowMaxHP != mPlayer.mCurrentHP)
        {
            mNowMaxHP = mPlayer.mMaxHP;
        }
    }
}
