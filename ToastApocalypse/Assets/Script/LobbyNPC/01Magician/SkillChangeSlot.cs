using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillChangeSlot : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public int mID;
    public bool mbDragging;
    public SkillStat mSkill;
    public SkillText mSkillText;
    public Image mDragTarget;
    public Image mIcon;

    public void Init(int id)
    {
        mID = id;
        mbDragging = false;
    }

    public void SetData(int id)
    {
        mIcon.sprite = SkillController.Instance.SkillIcon[id];
        mSkill = SkillController.Instance.mStatInfoArr[id];
        mSkillText = SkillController.Instance.mTextInfoArr[id];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mbDragging = SkillChangeController.Instance.StartDragging(mID);
        if (mbDragging == true)
        {
            mDragTarget = SkillChangeController.Instance.DragTarget;
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
            if (SkillChangeController.Instance.mSelectSlot.mDraggingID>-1)
            {
                SkillChangeController.Instance.mSelectSlot.mIcon.sprite = mIcon.sprite;
                SkillChangeController.Instance.mSelectSlot.mIcon.color = Color.white;
                SkillChangeController.Instance.mSelectSlot.mSkill = mSkill;
                SkillChangeController.Instance.mSelectSlot.mSkillText = mSkillText;
                SkillChangeController.Instance.mSelectSlot.mSkillID = mSkill.ID;
                GameSetting.Instance.PlayerSkillID = mSkill.ID;
            }
            mIcon.transform.SetParent(transform);
            mIcon.transform.position = transform.position;
            mDragTarget.color = Color.clear;
            mIcon.color = Color.white;
        }
    }
}
