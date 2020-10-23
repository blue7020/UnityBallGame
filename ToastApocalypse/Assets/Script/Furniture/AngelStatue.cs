﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngelStatue : MonoBehaviour
{
    public static AngelStatue Instance;
    public Image mWindow,mRewardWindow;
    public Text TitleText, DonateText;
    public Button DonateButton;
    public GameObject Medal;
    public Transform Parents;
    public RewardWindow mSlot;
    public RewardWindow[] SlotArr;
    public Sprite[] mSpt;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            ShowMedal();
            if (GameSetting.Instance.Language==0)
            {
                TitleText.text = "과거에 전쟁의 승리를 기원하던 제단...\n기부하면 좋은 일이 일어날 지도 모른다.";
                DonateText.text = "기부한다";
            }
            else if (GameSetting.Instance.Language==1)
            {
                TitleText.text = "An altar that wished for victory in war in the past...\nGood things may happen if you donate.";
                DonateText.text = "Donate";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }

    public void ShowMedal()
    {
        if (SaveDataController.Instance.mUser.DonateCount>=3)
        {
            Medal.SetActive(true);
        }
    }

    public void Donate()
    {
        IAPController.Instance.BuyDonateStatue();
        ShowRewardWindow();
        ShowMedal();
    }

    public void ShowRewardWindow()
    {
        if (SlotArr!=null)
        {
            for (int i = 0; i < SlotArr.Length; i++)
            {
                SlotArr[i].DestroyThis();
            }
        }
        switch (SaveDataController.Instance.mUser.DonateCount)
        {
            case 1:
                SlotArr = new RewardWindow[2];
                SlotArr[0] = Instantiate(mSlot, Parents);
                SlotArr[0].mIcon.sprite = GameSetting.Instance.mPlayerSpt[7];
                SlotArr[0].mAmountText.text = "1";
                SlotArr[1] = Instantiate(mSlot, Parents);
                SlotArr[1].mIcon.sprite = GameSetting.Instance.mWeaponArr[25].mRenderer.sprite;
                SlotArr[1].mAmountText.text = "1";
                break;
            case 2:
                SlotArr = new RewardWindow[2];
                SlotArr[0] = Instantiate(mSlot, Parents);
                SlotArr[0].mIcon.sprite = GameSetting.Instance.mPlayerSpt[2];
                SlotArr[0].mAmountText.text = "1";
                SlotArr[1] = Instantiate(mSlot, Parents);
                SlotArr[1].mIcon.sprite = SkillController.Instance.SkillIcon[2];
                SlotArr[1].mAmountText.text = "1";
                break;
            default:
                SlotArr = new RewardWindow[1];
                SlotArr[0] = Instantiate(mSlot, Parents);
                SlotArr[0].mIcon.sprite = mSpt[0];
                SlotArr[0].mAmountText.text = "5000";
                break;
        }
        mRewardWindow.gameObject.SetActive(true);
    }

}
