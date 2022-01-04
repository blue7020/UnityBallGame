using System.Collections;
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
            LoadJson(out mInfoArr, Paths.CHARACTER_TEXT);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PageReset()
    {
        mPageCount = 0;
    }

    public void RefreshPage()
    {
        LanguageSetting();
        if (mPageCount==SaveDataController.Instance.mCharacterID)
        {
            mButton.interactable = false;
        }
        else
        {
            mButton.interactable = true;
        }
        mIconImage.sprite = mSprite[mPageCount];
    }

    public void LanguageSetting()
    {
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mCharacterNameText.text = mInfoArr[mPageCount].name_kor;
            mToolTipText.text = mInfoArr[mPageCount].text_kor;
            mTitleText.text = "캐릭터 선택";
            mButtonText.text = "선택";
        }
        else
        {
            mCharacterNameText.text = mInfoArr[mPageCount].name_eng;
            mToolTipText.text = mInfoArr[mPageCount].text_eng;
            mTitleText.text = "Character";
            mButtonText.text = "Select";
        }
    }

    public void PlusPage()
    {
        if (mPageCount + 1<= mSprite.Length)
        {
            mPageCount++;
            RefreshPage();
        }
    }

    public void MinusPage()
    {
        if (mPageCount - 1 >= 0)
        {
            mPageCount--;
            RefreshPage();
        }
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
