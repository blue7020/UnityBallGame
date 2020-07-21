using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public eItemType eType;
    public int mID;
    public int mDraggingID;
    public Image mIcon;
    public ItemData mItem;

    public bool isEquip;

    private void Awake()
    {
        isEquip = false;
    }
    public void SetItem(int id)
    {
        if (mItem.Level>Player.Instance.Level)
        {
            string text = "레벨이 낮습니다!";
            UIController.Instance.ShowPopup(text);
        }
        else
        {
            mID = id;
            mIcon.sprite = ItemDataController.Instance.GetItemSprite(id);
            mItem = ItemDataController.Instance.GetItemData(id);
            Player.Instance.Equip(mItem);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryController.Instance.eType = eType;
        InventoryController.Instance.mEquipSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mDraggingID = -1;
        InventoryController.Instance.eType = eItemType.Others;
        InventoryController.Instance.mEquipSlot = null;
    }

}
