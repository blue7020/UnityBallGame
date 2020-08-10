﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillShop : MonoBehaviour
{
    public static SkillShop Instance;

    public Image mScreen;
    public Button mBuyButton;
    public Text mTitle, ChangeButtonText, ShopButtonText, mShopTitleText, mBuyText, mLoreText, mSkillTitle, mSkillPrice, mChangeTitleText, mSelectText, mChangeTooltip;

    public SkillStat mSkill;
    public SkillText mSkillText;

    public int SKILL_SLOT;

    public SkillSlot ShopSlot;
    public Transform mShopParents;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mTitle.text = "마법사: 무엇을 하고 싶은가?";
            ChangeButtonText.text = "스킬 교체";
            ShopButtonText.text = "스킬 구매";
            mShopTitleText.text = "스킬 구매";
            mChangeTitleText.text = "스킬 교체";
            mSkillTitle.text = "스킬 선택";
            mLoreText.text = "스테이지 내에서 사용할 수 있는 스킬을 구입합니다";
            mChangeTooltip.text = "등록할 스킬을 드래그하여 현재 선택 칸에 넣어주세요";
            mSelectText.text = "현재 선택";
            mBuyText.text = "구매";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mTitle.text = "Magician: What do you want?";
            ChangeButtonText.text = "Skill Change";
            ShopButtonText.text = "Skill Shop";
            mShopTitleText.text = "Skill Shop";
            mChangeTitleText.text = "Skill Change";
            mSkillTitle.text = "Skill Select";
            mLoreText.text = "Touch the skill to purchase";
            mChangeTooltip.text = "Drag the skill to selection slot.";
            mSelectText.text = "Selection";
            mBuyText.text = "Purchase";
        }
    }

    private void Start()
    {
        SKILL_SLOT = SkillController.Instance.mStatInfoArr.Length;
        for (int i = 0; i < SKILL_SLOT; i++)
        {
            SkillSlot mSlot= Instantiate(ShopSlot, mShopParents);
            mSlot.SetData(i);
            
        }    
    }

    public void BuySkill()
    {
        if (GameSetting.Instance.Syrup >=mSkillText.Price)
        {
            GameSetting.Instance.Syrup -= mSkillText.Price;
            GameSetting.Instance.PlayerHasSkill[mSkill.ID] = true;
            MainLobbyUIController.Instance.ShowSyrupText();
            mBuyButton.interactable = false;
        }
    }

    public void ShowSkillInfo(SkillStat stat, SkillText lore)
    {
        mSkill = stat;
        mSkillText = lore;
        mBuyButton.interactable = true;
        if (GameSetting.Instance.PlayerHasSkill[mSkill.ID] == true)
        {
            if (GameSetting.Instance.Language == 0)//한국어
            {
                mBuyText.text = "보유중";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                mBuyText.text = "Hold";
            }
            mBuyButton.interactable = false;
        }
        else
        {
            mBuyText.text = mSkillText.Price.ToString();
            mBuyButton.interactable = true;
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mSkillTitle.text = mSkillText.Title;
            mLoreText.text = mSkillText.ContensFormat;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mSkillTitle.text = mSkillText.EngTitle;
            mLoreText.text = mSkillText.EngContensFormat;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MainLobbyUIController.Instance.GamePause();
            mScreen.gameObject.SetActive(true);
        }
    }
}