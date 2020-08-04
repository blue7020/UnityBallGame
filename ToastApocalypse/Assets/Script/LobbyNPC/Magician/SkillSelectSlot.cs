using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSelectSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int SkillID;
    //public Image mToolTip;
    //public Text Title, Lore;
    public int mID;
    public int mDraggingID;

    public Image Icon;
    public SkillStat mSkill;
    public SkillText mSkillText;

    public bool IsSetting;

    public void SetData(int id)
    {
        mSkill = SkillController.Instance.mStatInfoArr[SkillID];
        mSkillText = SkillController.Instance.mTextInfoArr[SkillID];
        SkillID = id;
        Icon.sprite = SkillController.Instance.SkillIcon[SkillID];
        //if (GameSetting.Instance.Language == 0)//한국어
        //{
        //    Title.text = mSkillText.Title;
        //}
        //else if (GameSetting.Instance.Language == 1)//영어
        //{
        //    Title.text = mSkillText.EngTitle;
        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillChangeController.Instance.mSelectSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mDraggingID = -1;
        SkillChangeController.Instance.mSelectSlot = null;
    }



}
