﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IPointerDownHandler
{
    [SerializeField]
    public Image mItemImage;
    private int mID;//Slot ID
    public Artifacts art;


    public void Init(int id, Sprite image)
    {
        mID = id;
        mItemImage.sprite = image;
    }

    //public void SetSprite(Sprite image,Artifacts arti)
    //{
    //    mItemImage.sprite = image;
    //    art = arti;
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.Instance.Inventory[mID]!=null)
        {
            art = Player.Instance.Inventory[mID];
            if (GameSetting.Instance.Language == 0)
            {
                UIController.Instance.tooltip.ShowTooltip(art.mStatInfoArr[art.mID].ContensFormat);
            }
            else if (GameSetting.Instance.Language == 1)
            {
                UIController.Instance.tooltip.ShowTooltip(art.mStatInfoArr[art.mID].EngContensFormat);
            }
        }
        
    }
}
