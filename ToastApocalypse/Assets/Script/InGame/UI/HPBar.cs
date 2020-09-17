using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image mBar;
    private float mNowHP;
    private float mNowMaxHP;
    private float mNowAir;

    private void Start()
    {
        mNowHP = Player.Instance.mCurrentHP;
        mNowMaxHP = Player.Instance.mMaxHP;
        mBar.fillAmount = mNowHP / mNowMaxHP;
        ShowHPBar();
        if (GameSetting.Instance.NowStage == 4)
        {
            mNowAir = Player.Instance.mCurrentAir;
            mBar.fillAmount = mNowAir / Player.MAX_AIR;
            ShowAirBar();
        }
    }

    public void ShowHPBar()
    {
        mNowHP = Player.Instance.mCurrentHP;
        mNowMaxHP = Player.Instance.mMaxHP;
        if (mNowHP!= Player.Instance.mCurrentHP)
        {
            mNowHP = Player.Instance.mCurrentHP;
        }
        else if (mNowMaxHP != Player.Instance.mCurrentHP)
        {
            mNowMaxHP = Player.Instance.mMaxHP;
        }
        mBar.fillAmount = mNowHP / mNowMaxHP;
    }

    public void ShowAirBar()
    {
        mNowAir = Player.Instance.mCurrentAir;
        if (mNowAir != Player.Instance.mCurrentAir)
        {
            mNowHP = Player.Instance.mCurrentHP;
        }
        mBar.fillAmount = mNowAir / Player.MAX_AIR;
    }

}
