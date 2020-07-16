using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Camera mUICamera;
    [SerializeField]
    private Image mItemImage;
    private int mID;
    private Image mDragTarget;
    private bool mbDragging;
    

    public void Init(int id, Sprite image, Image dragTarget)
    {
        mID = id;
        mItemImage.sprite = image;
        mDragTarget = dragTarget;
    }

    public void SetSprite(Sprite image)
    {
        mItemImage.sprite = image;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mbDragging = InventoryController.Instance.StartDragging(mID);
        if (mbDragging)
        {
            mDragTarget.sprite = mItemImage.sprite;
            mItemImage.color = Color.clear;
            mDragTarget.transform.position = eventData.position;
            mDragTarget.gameObject.SetActive(true);
        }

        //drag target 세팅
    }

    public void OnDrag(PointerEventData eventData)
    {

        if(mbDragging)
        {
            mDragTarget.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(mbDragging)
        {
            Debug.Log("End Drag");
            mItemImage.color = Color.white;
            mDragTarget.gameObject.SetActive(false);
            mbDragging = false;
            InventoryController.Instance.EndDragging();
        }
    }
}
