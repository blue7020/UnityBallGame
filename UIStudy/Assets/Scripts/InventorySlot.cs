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
    private int mID;//Slot ID
    private Image mDragTarget;
    private bool mbDragging;

    public void Init(int id,Sprite image,Image dragTarget)
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
        mbDragging = InventrotyController.Instance.StartDragging(mID);
        if (mbDragging)
        {
            Vector3 Pos = Camera.main.ScreenToWorldPoint(eventData.position);
            Pos.z = 0;

            mDragTarget.sprite = mItemImage.sprite;
            mItemImage.color = Color.clear;
            mDragTarget.transform.position = eventData.position;
            mDragTarget.gameObject.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (mbDragging)
        {
            mDragTarget.transform.position = eventData.position;
        }

        //screen space Camera 옵션일경우 사용
        //Vector3 Pos = mUICamera.ScreenToWorldPoint(eventData.position);
        //Pos.z = 0;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (mbDragging)
        {
            Debug.Log("endDrag");
            mItemImage.color = Color.white;
            mDragTarget.gameObject.SetActive(false);
            mbDragging = false;
            InventrotyController.Instance.EndDragging();
        }
    }
}
