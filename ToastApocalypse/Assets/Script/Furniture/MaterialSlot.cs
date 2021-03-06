﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MaterialSlot : MonoBehaviour, IPointerClickHandler
{
    public int mMaterialID;

    public Image mIcon,mAmountImage;
    public Sprite mVoid;
    public MaterialStat mMaterial;
    public string title, lore;
    public Text mCount;
    public int mAmount;//상점용
    public bool mExchange;

    public void SetData(int id,bool exchange=false)
    {
        mIcon.color = Color.white;
        if (mMaterial != null && GameSetting.Instance.Ingame == false)
        {
            mMaterial = MaterialController.Instance.mInfoArr[mMaterialID];
        }
        mMaterialID = id;
        mIcon.sprite = GameSetting.Instance.mMaterialSpt[mMaterialID];
        if (exchange)
        {
            mExchange = exchange;
        }
    }

    public void HideAmount()
    {
        mAmountImage.gameObject.SetActive(false);
    }

    public void RemoveData()
    {
        mMaterial = null;
        mMaterialID = -1;
        mAmount = 0;
        mCount.text = "";
        mIcon.color = Color.clear;
        mIcon.sprite = mVoid;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mMaterial != null && GameSetting.Instance.Ingame == false)
        {
            if (mExchange)
            {
                ExchangeController.Instance.mSellAmount = 0;
                ExchangeController.Instance.mTotalSyrup = 0;
                ExchangeController.Instance.ShowDescription(mMaterialID);
            }
            if (BlackSmith.Instance.IsShop == false && mExchange == false)
            {
                MaterialController.Instance.ShowDescription(mMaterialID);
            }
        }

    }
}
