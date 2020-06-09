using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Image mHPBar;
    private float mNowHP;
    private float mNowMaxHP;

    private void Awake()
    {
        mHPBar = GetComponent<Image>();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        mNowHP = Player.Instance.mCurrentHP;
        mNowMaxHP = Player.Instance.mMaxHP;
        mHPBar.fillAmount = mNowHP / mNowMaxHP;
    }

    private void FixedUpdate()
    {
        mHPBar.fillAmount = mNowHP / mNowMaxHP;
        if (mNowHP!= Player.Instance.mCurrentHP)
        {
            mNowHP = Player.Instance.mCurrentHP;
        }
        else if (mNowMaxHP != Player.Instance.mCurrentHP)
        {
            mNowMaxHP = Player.Instance.mMaxHP;
        }
    }

}
