using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    public static HPBar Instance;

    private Image mHPBar;
    private float mNowHP;
    private float mNowMaxHP;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mHPBar = GetComponent<Image>();

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        mNowHP = Player.Instance.mCurrentHP;
        mNowMaxHP = Player.Instance.mMaxHP;
        mHPBar.fillAmount = mNowHP / mNowMaxHP;
        ShowHPBar();
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
        mHPBar.fillAmount = mNowHP / mNowMaxHP;
    }

}
