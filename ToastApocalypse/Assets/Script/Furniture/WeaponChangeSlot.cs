using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponChangeSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int mID;
    public bool mbDragging;
    public WeaponStat mWeapon;
    public Image mDragTarget;
    public Image mIcon;

    private void Awake()
    {
        mID = -1;
    }

    public void Init(int id)
    {
        mID = id;
        mbDragging = false;
    }

    public void SetData(int id)
    {
        mIcon.sprite = GameSetting.Instance.mWeaponArr[id].mWeaponImage;
        mWeapon = SaveDataController.Instance.mWeaponInfoArr[id];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mbDragging = WeaponSelectController.Instance.StartDragging(mID);
        if (mbDragging == true)
        {
            mDragTarget = WeaponSelectController.Instance.DragTarget;
            mDragTarget.color = Color.white;
            mDragTarget.sprite = mIcon.sprite;
            mIcon.color = Color.clear;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (mbDragging == true)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
            pos.z = 0;

            mDragTarget.transform.position = pos;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mbDragging = false;
        if (mbDragging == false)
        {
            if (WeaponSelectController.Instance.mSelectSlot.mDraggingID > -1)
            {
                WeaponSelectController.Instance.mSelectSlot.SetData(mWeapon.ID, mIcon.sprite, mWeapon);
                GameSetting.Instance.PlayerWeaponID = mWeapon.ID;
            }
            mIcon.transform.SetParent(transform);
            mIcon.transform.position = transform.position;
            mDragTarget.color = Color.clear;
            mIcon.color = Color.white;
        }
    }
}
