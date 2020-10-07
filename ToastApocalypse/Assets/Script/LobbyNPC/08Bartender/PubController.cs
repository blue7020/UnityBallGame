using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PubController : MonoBehaviour
{
    public static PubController Instance;

    public PubSlot ShopSlot;
    public Transform mShopParents;

    public ItemStat mItem;

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
        for (int i = 0; i < GameSetting.Instance.mItemInfoArr.Length; i++)
        {
            if (GameSetting.Instance.mItemInfoArr[i].Open==true)
            {
                PubSlot mSlot = Instantiate(ShopSlot, mShopParents);
                mSlot.SetData(i);
            }
        }
    }

    public void BuyItem()
    {
        if (GameSetting.Instance.Syrup >= mItem.OpenPrice)
        {
            SoundController.Instance.SESoundUI(3);
            GameSetting.Instance.Syrup -= mItem.OpenPrice;
            GameSetting.Instance.mItemInfoArr[mItem.ID].PlayerHas = true;
            MainLobbyUIController.Instance.ShowSyrupText();
            ShowItemInfo(mItem);
        }
    }

    public void ShowItemInfo(ItemStat stat)
    {
        mItem = stat;
        Bartender.Instance.mButton.interactable = true;
        if (GameSetting.Instance.mItemInfoArr[mItem.ID].PlayerHas == true)
        {
            if (GameSetting.Instance.Language == 0)//한국어
            {
                Bartender.Instance.mButtonText.text = "보유중";
                Bartender.Instance.mItemNameText.text = mItem.Name;
                Bartender.Instance.mItemTooltipText.text = mItem.ContensFormat + "\n가격:" + mItem.OpenPrice.ToString() + " 시럽";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                Bartender.Instance.mButtonText.text = "Get";
                Bartender.Instance.mItemNameText.text = mItem.EngName;
                Bartender.Instance.mItemTooltipText.text = mItem.EngContensFormat + "\nPrice: " + mItem.OpenPrice.ToString() + " Syrup";
            }
            Bartender.Instance.mButton.interactable = false;
        }
        else
        {
            Bartender.Instance.mButton.interactable = true;
            if (GameSetting.Instance.Language == 0)//한국어
            {
                Bartender.Instance.mButtonText.text = "구매";
                Bartender.Instance.mItemNameText.text = mItem.Name;
                Bartender.Instance.mItemTooltipText.text = mItem.ContensFormat + "\n가격:" + mItem.OpenPrice.ToString() + " 시럽";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                Bartender.Instance.mButtonText.text = "Purchase";
                Bartender.Instance.mItemNameText.text = mItem.EngName;
                Bartender.Instance.mItemTooltipText.text = mItem.EngContensFormat + "\nPrice: " + mItem.OpenPrice.ToString() + " Syrup";
            }
        }
    }

}
