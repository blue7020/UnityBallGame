﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectController : InformationLoader
{
    public static CharacterSelectController Instance;

    public Text mTitleText, mToolTipText, mCharacterNameText, mButtonText;
    public int mPageCount;
    public Image mIconImage;
    public Sprite[] mSprite;
    public Button mButton;

    private CharacterText[] mInfoArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mInfoArr = SaveDataController.Instance.mCharacterInfoArr;
    }

    public void PageReset()
    {
        mPageCount = 0;
        mIconImage.sprite = mSprite[mPageCount];
        LanguageSetting();
    }


    public void LanguageSetting()
    {
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mCharacterNameText.text = mInfoArr[mPageCount].name_kor;
            mToolTipText.text = mInfoArr[mPageCount].text_kor;
            mTitleText.text = "캐릭터 선택";
            if (SaveDataController.Instance.mUser.CharacterOpen[mPageCount] ==true)
            {
                mButtonText.text = "선택";
                mButton.interactable = true;
            }
            else
            {
                mButtonText.text = "잠김";
                mButton.interactable = false;
            }
            if (SaveDataController.Instance.mUser.CharacterID == mPageCount)
            {
                mButtonText.text = "선택";
                mButton.interactable = false;
            }
        }
        else
        {
            mCharacterNameText.text = mInfoArr[mPageCount].name_eng;
            mToolTipText.text = mInfoArr[mPageCount].text_eng;
            mTitleText.text = "Character";
            if (SaveDataController.Instance.mUser.CharacterOpen[mPageCount] == true)
            {
                mButtonText.text = "Select";
                mButton.interactable = true;
            }
            else
            {
                mButtonText.text = "Locked";
                mButton.interactable = false;
            }
            if (SaveDataController.Instance.mUser.CharacterID == mPageCount)
            {
                mButtonText.text = "Locked";
                mButton.interactable = false;
            }
        }
    }

    public void PlusPage()
    {
        if (mPageCount + 1<= mSprite.Length-1)
        {
            mPageCount++;
            mIconImage.sprite = mSprite[mPageCount];
        }
        LanguageSetting();
    }

    public void MinusPage()
    {
        if (mPageCount - 1 >= 0)
        {
            mPageCount--;
            mIconImage.sprite = mSprite[mPageCount];
        }
        LanguageSetting();
    }

    public void SelectCharacter()
    {
        SaveDataController.Instance.mCharacterID = mInfoArr[mPageCount].ID;
        if (mPageCount == SaveDataController.Instance.mCharacterID)
        {
            mButton.interactable = false;
        }
        else
        {
            mButton.interactable = true;
        }
    }
}