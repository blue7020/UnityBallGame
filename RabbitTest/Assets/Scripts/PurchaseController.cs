﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseController : MonoBehaviour
{
    public static PurchaseController Instance;

    public Text mPurchaseText, mPurchaseTooltipText, mPurchaseBuyText;
    public Button mPurchaseBuyButton;
    public Image mIcon;
    public Sprite[] mSprite;
    public int mPageCount;

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

    public void PurchaseWindowRefresh()
    {
        MainController.Instance.RefreshGold();
        if (SaveDataController.Instance.mLanguage == 1)//if now Korean
        {
            mPurchaseBuyText.text = "구매";
            switch (mPageCount)
            {
                case 0: //광고 제거
                    if (SaveDataController.Instance.mUser.NoAds)
                    {
                        mPurchaseBuyButton.interactable = true;

                    }
                    else
                    {
                        mPurchaseBuyButton.interactable = false;
                    }
                        mPurchaseText.text = "광고 제거";
                        mPurchaseTooltipText.text = "광고를 제거합니다.\n<b>가격: 3300원</b>\n(주의: 게임을 삭제하면\n구매 복구를 할 수 없습니다.)";
                    break;
                case 1: //부활권
                    mPurchaseBuyButton.interactable = true;
                    mPurchaseText.text = "부활 스크롤";
                    mPurchaseTooltipText.text = "1회 소모하여 부활합니다.\n<b>가격: 25G</b>\n(주의: 게임을 삭제하면\n구매 복구를 할 수 없습니다.)";
                    break;
                case 2: //캐릭터:실비아
                    if (SaveDataController.Instance.mUser.CharacterOpen[1] == false)
                    {
                        mPurchaseBuyButton.interactable = true;
                    }
                    else
                    {
                        mPurchaseBuyButton.interactable = false;
                    }
                    mPurchaseText.text = "캐릭터: 실비아";
                    mPurchaseTooltipText.text = "실비아를 해금합니다.\n<b>가격: 50G</b>\n(주의: 게임을 삭제하면\n구매 복구를 할 수 없습니다.)";
                    break;
                case 3: //캐릭터:머플러왕자
                    if (SaveDataController.Instance.mUser.CharacterOpen[2] == false)
                    {
                        mPurchaseBuyButton.interactable = true;
                    }
                    else
                    {
                        mPurchaseBuyButton.interactable = false;
                    }
                    mPurchaseText.text = "캐릭터: 머플러 왕자";
                    mPurchaseTooltipText.text = "머플러 왕자를 해금합니다.\n<b>가격: 50G</b>\n(주의: 게임을 삭제하면\n구매 복구를 할 수 없습니다.)";
                    break;
                default:
                    break;
            }
        }
        else //english
        {
            mPurchaseBuyText.text = "Buy";
            switch (mPageCount)
            {
                case 0: //광고 제거
                    if (SaveDataController.Instance.mUser.NoAds)
                    {
                        mPurchaseBuyButton.interactable = true;

                    }
                    else
                    {
                        mPurchaseBuyButton.interactable = false;
                    }
                    mPurchaseText.text = "REMOVE ADS";
                    mPurchaseTooltipText.text = "Remove all Ads.\n<b>Price: USD 2.49</b>\n(CAUTION: If you delete a game, you cannot restore your purchase.)";
                    break;
                case 1: //부활권
                    mPurchaseBuyButton.interactable = true;
                    mPurchaseText.text = "Revival Scroll";
                    mPurchaseTooltipText.text = "If consumed, it will be revived once unconditionally.\n<b>Price: 25G</b>\n(CAUTION: If you delete a game, you cannot restore your purchase.)";
                    break;
                case 2: //캐릭터:실비아
                    if (SaveDataController.Instance.mUser.CharacterOpen[1] == false)
                    {
                        mPurchaseBuyButton.interactable = true;
                    }
                    else
                    {
                        mPurchaseBuyButton.interactable = false;
                    }
                    mPurchaseText.text = "Character: Silvia";
                    mPurchaseTooltipText.text = "Unlock character Sylvia.\n<b>Price: 50G</b>\n(CAUTION: If you delete a game, you cannot restore your purchase.)";
                    break;
                case 3: //캐릭터:머플러 왕자
                    if (SaveDataController.Instance.mUser.CharacterOpen[2] == false)
                    {
                        mPurchaseBuyButton.interactable = true;
                    }
                    else
                    {
                        mPurchaseBuyButton.interactable = false;
                    }
                    mPurchaseText.text = "Character: Prince Muffler";
                    mPurchaseTooltipText.text = "Unlock character Prince Muffler.\n<b>Price: 50G</b>\n(CAUTION: If you delete a game, you cannot restore your purchase.)";
                    break;
                default:
                    break;
            }
        }
    }

    public void BuyFunction()
    {
        switch (mPageCount)
        {
            case 0:
                IAPController.Instance.BuyNOAds();
                break;
            case 1:
                if (SaveDataController.Instance.mUser.Gold >= 25)
                {
                    if (SaveDataController.Instance.mUser.RevivalCount+1<=99)
                    {
                        SaveDataController.Instance.mUser.Gold -= 25;
                        MainController.Instance.RefreshGold();
                        SaveDataController.Instance.mUser.RevivalCount += 1;
                        SoundController.Instance.SESound(9);
                    }
                }
                break;
            case 2:
                BuyCharacter(1);
                break;
            case 3:
                BuyCharacter(2);
                break;
            default:
                break;
        }
        SaveDataController.Instance.Save();
    }


    public void PlusPage()
    {
        if (mPageCount + 1 <= mSprite.Length - 1)
        {
            mPageCount++;
            mIcon.sprite = mSprite[mPageCount];
        }
        PurchaseWindowRefresh();
    }

    public void MinusPage()
    {
        if (mPageCount - 1 >= 0)
        {
            mPageCount--;
            mIcon.sprite = mSprite[mPageCount];
        }
        PurchaseWindowRefresh();
    }

    public void BuyCharacter(int id)
    {
        if (SaveDataController.Instance.mUser.Gold >= SaveDataController.Instance.mCharacterInfoArr[id].Price)
        {
            SaveDataController.Instance.mUser.Gold -= SaveDataController.Instance.mCharacterInfoArr[id].Price;
            MainController.Instance.RefreshGold();
            SaveDataController.Instance.mUser.CharacterOpen[id] = true;
            if (SaveDataController.Instance.mUser.CharacterOpen[id] == false)
            {
                mPurchaseBuyButton.interactable = true;
            }
            else
            {
                mPurchaseBuyButton.interactable = false;
            }
            SoundController.Instance.SESound(9);
        }
    }
}
