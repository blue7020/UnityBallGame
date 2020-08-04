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
    public Image mDragTarget;

    public int SkillID;
    public Image Icon;

    public void Init(int id)
    {
        mID = id;
        mbDragging = false;
    }

    public void SetData(int id)
    {
        SkillID = id;
        Icon.sprite = SkillController.Instance.SkillIcon[SkillID];
        mSkill = SkillController.Instance.mStatInfoArr[SkillID];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mbDragging = SkillChangeController.Instance.StartDragging(mID);
        if (mbDragging == true)
        {
            mDragTarget = SkillChangeController.Instance.DragTarget;
            mDragTarget.color = Color.white;
            mDragTarget.sprite = Icon.sprite;
            Icon.color = Color.clear;
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
            if (SkillChangeController.Instance.mSelectSlot!=null)
            {
                SkillChangeController.Instance.mSelectSlot.Icon.sprite = Icon.sprite;
                SkillChangeController.Instance.mSelectSlot.Icon.color = Color.white;
                GameSetting.Instance.PlayerID = SkillID;
            }
            Icon.transform.SetParent(transform);
            Icon.transform.position = transform.position;
            mDragTarget.color = Color.clear;
            Icon.color = Color.white;
        }
    }
}
