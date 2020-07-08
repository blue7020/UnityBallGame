using System.Collections;
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


    public void Init(int id, Sprite image,Artifact art = null)
    {
        mID = id;
        mItemImage.sprite = image;
    }

    public void SetItem(Artifacts arti)
    {
        art = arti;
        mItemImage.sprite = art.mRenderer.sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (art!=null)
        {
            if (GameSetting.Instance.Language == 0)
            {
                UIController.Instance.tooltip.ShowTooltip(art.TextStats.ContensFormat);
            }
            else if (GameSetting.Instance.Language == 1)
            {
                UIController.Instance.tooltip.ShowTooltip(art.TextStats.EngContensFormat);
            }
        }
        
    }
}
