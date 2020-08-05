using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSelectSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public int SkillID;
    public int mDraggingID;

    public Image mIcon;
    public SkillStat mSkill;
    public SkillText mSkillText;
    public SkillTooltip mTooltip;
    public string title,lore;

    private void Awake()
    {
        mDraggingID = -1;
    }

    public void SetData(int id)
    {
        mSkill = SkillController.Instance.mStatInfoArr[SkillID];
        mSkillText = SkillController.Instance.mTextInfoArr[SkillID];
        SkillID = id;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mDraggingID = SkillID;
        SkillChangeController.Instance.mSelectSlot = this;
        mSkill = SkillChangeController.Instance.mSkill;
        mSkillText = SkillChangeController.Instance.mSkillText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mDraggingID = -1;
        SkillChangeController.Instance.mSelectSlot = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mSkill!=null&&mSkillText!=null)
        {
            mTooltip = SkillChangeController.Instance.mTooltip;
            if (GameSetting.Instance.Language == 0)//한국어
            {
                if (mSkill.Damage > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 피해량: (" + mSkill.Damage + " * 공격력)+ 현재 스테이지";
                }
                if (mSkill.Heal > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 회복량: " + mSkill.Heal;
                }
                if (mSkill.Atk > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 공격력: +" + mSkill.Atk + "%";
                }
                if (mSkill.Def > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 방어력: +" + mSkill.Def + "%";
                }
                if (mSkill.AtkSpd > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 공격 속도: +" + mSkill.AtkSpd + "%";
                }
                if (mSkill.Crit > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 치명타 확률: +" + mSkill.Crit + "%";
                }
                if (mSkill.Spd > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 이동 속도: +" + mSkill.Spd + "%";
                }
                if (mSkill.Duration > 0)
                {
                    lore = mSkillText.ContensFormat + "/ 지속 시간: +" + mSkill.Duration + "초";
                }
                title = mSkillText.Title + " (재사용 대기시간: " + mSkill.Cooltime + "초)";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                if (mSkill.Damage > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Damage: (" + mSkill.Damage + " * Atk)+ Now Stage";
                }
                if (mSkill.Heal > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Heal amount: " + mSkill.Heal;
                }
                if (mSkill.Atk > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Atk: +" + mSkill.Atk + "%";
                }
                if (mSkill.Def > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Def: +" + mSkill.Def + "%";
                }
                if (mSkill.AtkSpd > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Atk Speed: +" + mSkill.AtkSpd + "%";
                }
                if (mSkill.Crit > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Critical chance: +" + mSkill.Crit + "%";
                }
                if (mSkill.Spd > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Speed: +" + mSkill.Spd + "%";
                }
                if (mSkill.Duration > 0)
                {
                    lore = mSkillText.EngContensFormat + "/ Druation: +" + mSkill.Duration + "Sec";
                }
                title = mSkillText.EngTitle + " (CoolTime: " + mSkill.Cooltime + "Sec)";
            }
            mTooltip.ShowTooltip(title, lore, mIcon.sprite);
            mTooltip.gameObject.SetActive(true);
        }

    }
}
