using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public int mID;
    public Image mIcon;
    public bool mbDragging;
    public Transform mSlot;
    public ItemData mItem;
    public Image mDragTarget;

    public void Init(int id, Sprite voidSpt)
    {
        mID = id;
        mIcon.sprite = voidSpt;
        mbDragging = false;
    }

    public void SetItem(int id)
    {
        mIcon.sprite = ItemDataController.Instance.GetItemSprite(id);
        mItem = ItemDataController.Instance.GetItemData(id);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mbDragging = InventoryController.Instance.StartDragging(mID);
        if (mbDragging==true)
        {
            mDragTarget = InventoryController.Instance.mDragTarget;
            mIcon.transform.SetParent(InventoryController.Instance.Canvas);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        mIcon.transform.position = eventData.position;
        mDragTarget.transform.position = mIcon.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mbDragging = false;
        if (mbDragging == false)
        {
            if (InventoryController.Instance.eType==mItem.ItemType&&InventoryController.Instance.mEquipSlot!=null)
            {
                InventoryController.Instance.mEquipSlot.SetItem(mID);
            }
            mIcon.transform.SetParent(transform);
            mIcon.transform.position = transform.position;
            mDragTarget.gameObject.SetActive(false);
        }
    }
}
