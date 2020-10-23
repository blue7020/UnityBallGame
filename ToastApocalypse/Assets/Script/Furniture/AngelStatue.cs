﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngelStatue : MonoBehaviour
{
    public static AngelStatue Instance;
    public Image mWindow;
    public Text TitleText, DonateText;
    public Button DonateButton;
    public GameObject Medal;
    public Transform Parents;
    public RewardWindow mRewardImageWindow;
    public List<RewardWindow> SlotList;
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
        SlotList = new List<RewardWindow>();
        switch (SaveDataController.Instance.mUser.DonateCount)
        {
            case 1:
                SlotList[0].mIcon.sprite = GameSetting.Instance.mPlayerSpt[7];
                SlotList[0].mAmountText.text = "1";
                SlotList[1].mIcon.sprite = GameSetting.Instance.mWeaponArr[7].mRenderer.sprite;
                SlotList[1].mAmountText.text = "1";
                break;
            case 2:
                SlotList[0].mIcon.sprite = GameSetting.Instance.mPlayerSpt[2];
                SlotList[0].mAmountText.text = "1";
                SlotList[1].mIcon.sprite = SkillController.Instance.SkillIcon[2];
                SlotList[1].mAmountText.text = "1";
                break;
            default:
                break;
        }
        ShowMedal();
    }

}
