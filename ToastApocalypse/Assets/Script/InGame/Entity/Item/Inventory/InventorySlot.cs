using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IPointerDownHandler
{
    public Image mItemImage;
    public int mID;//Slot ID
    public Artifacts artifact;


    public void Init(int id, Sprite spt)
    {
        mID = id;
        mItemImage.sprite = spt;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (artifact!=null)
        {
            if (GameSetting.Instance.Language == 0)
            {
                UIController.Instance.tooltip.ShowTooltip(artifact.TextStats.ContensFormat);
            }
            else if (GameSetting.Instance.Language == 1)
            {
                UIController.Instance.tooltip.ShowTooltip(artifact.TextStats.EngContensFormat);
            }
        }
        
    }
}
