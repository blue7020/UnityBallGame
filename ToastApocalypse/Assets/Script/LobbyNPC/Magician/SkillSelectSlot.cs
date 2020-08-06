using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSelectSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public int mSkillID;
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
        mSkill = SkillController.Instance.mStatInfoArr[mSkillID];
        mSkillText = SkillController.Instance.mTextInfoArr[mSkillID];
        mSkillID = id;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mDraggingID = mSkillID;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mDraggingID = -1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mSkill!=null&&mSkillText!=null)
        {
            if (GameSetting.Instance.Language == 0)//한국어
            {
                lore = mSkillText.ContensFormat;
                if (mSkill.Damage > 0)
                {
                    lore += "피해량: (" + mSkill.Damage + " * 공격력)+ 현재 스테이지\n";
                }
                if (mSkill.Heal > 0)
                {
                    lore += "회복량: " + mSkill.Heal + "\n";
                }
                if (mSkill.Atk > 0)
                {
                    lore += "공격력: +" + mSkill.Atk + "%\n";
                }
                if (mSkill.Def > 0)
                {
                    lore += "방어력: +" + mSkill.Def + "%\n";
                }
                if (mSkill.AtkSpd > 0)
                {
                    lore += "공격 속도: +" + mSkill.AtkSpd + "%\n";
                }
                if (mSkill.Crit > 0)
                {
                    lore += " 치명타 확률: +" + mSkill.Crit + "%\n";
                }
                if (mSkill.Spd > 0)
                {
                    lore +=  "이동 속도: +" + mSkill.Spd + "%\n";
                }
                if (mSkill.Duration > 0)
                {
                    lore += "지속 시간: " + mSkill.Duration + "초\n";
                }
                if (mSkill!=null)
                {
                    title = mSkillText.Title + " (재사용 대기시간: " + mSkill.Cooltime.ToString() + "초)\n";
                }
                else
                {
                    title = "스킬을 등록해주십시오";
                }
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                lore = mSkillText.EngContensFormat;
                if (mSkill.Damage > 0)
                {
                    lore += "Damage: (" + mSkill.Damage + " * Atk)+ Now Stage\n";
                }
                if (mSkill.Heal > 0)
                {
                    lore += "Heal amount: " + mSkill.Heal+ "\n";
                }
                if (mSkill.Atk > 0)
                {
                    lore += "Atk: +" + mSkill.Atk + "%\n";
                }
                if (mSkill.Def > 0)
                {
                    lore += "Def: +" + mSkill.Def + "%\n";
                }
                if (mSkill.AtkSpd > 0)
                {
                    lore += "Atk Speed: +" + mSkill.AtkSpd + "%\n";
                }
                if (mSkill.Crit > 0)
                {
                    lore += "Critical chance: +" + mSkill.Crit + "%\n";
                }
                if (mSkill.Spd > 0)
                {
                    lore += "Speed: +" + mSkill.Spd + "%\n";
                }
                if (mSkill.Duration > 0)
                {
                    lore += "Druation: " + mSkill.Duration + "Sec\n";
                }
                if (mSkill != null)
                {
                    title = mSkillText.EngTitle + " (CoolTime: " + mSkill.Cooltime.ToString() + "Sec)\n";
                }
                else
                {
                    title = "Please setting player skill";
                }

            }
            mTooltip.ShowTooltip(title, lore, mIcon.sprite);
            mTooltip.gameObject.SetActive(true);
        }

    }
}
