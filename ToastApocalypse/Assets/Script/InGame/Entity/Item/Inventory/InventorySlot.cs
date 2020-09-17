using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image mItemImage;
    public int mID;//Slot ID
    public Artifacts artifact;
    public string lore;


    public void Init(int id, Sprite spt)
    {
        mID = id;
        mItemImage.sprite = spt;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (artifact != null)
        {

            if (GameSetting.Instance.Language == 0)//한국어
            {
                if (artifact.mStats != null)
                {
                    if (artifact.mStats.Skill_Cooltime > 0)
                    {
                        lore = "[" + artifact.TextStats.Title + "] (재사용 대기시간: " + artifact.mStats.Skill_Cooltime.ToString() + "초)\n"; ;

                    }
                    else
                    {
                        lore = "[" + artifact.TextStats.Title + "]\n";
                    }
                }
                lore += artifact.TextStats.ContensFormat + "\n";
                if (artifact.mStats.Heal > 0)
                {
                    lore += "회복량: " + artifact.mStats.Heal + "\n";
                }
                if (artifact.mStats.Atk > 0)
                {
                    lore += "공격력: +" + artifact.mStats.Atk * 100 + "%\n";
                }
                if (artifact.mStats.Def > 0)
                {
                    lore += "방어력: +" + artifact.mStats.Def * 100 + "%\n";
                }
                if (artifact.mStats.AtkSpd > 0)
                {
                    lore += "공격 속도: +" + artifact.mStats.AtkSpd * 100 + "%\n";
                }
                if (artifact.mStats.Crit > 0)
                {
                    lore += "치명타 확률: +" + artifact.mStats.Crit * 100 + "%\n";
                }
                if (artifact.mStats.Spd > 0)
                {
                    lore += "이동 속도: +" + artifact.mStats.Spd * 100 + "%\n";
                }
                if (artifact.mStats.CooltimeReduce > 0)
                {
                    lore += "재사용 대기시간 감소: +" + artifact.mStats.CooltimeReduce * 100 + "%\n";
                }
                if (artifact.mStats.CCReduce > 0)
                {
                    lore += "상태이상 저항: +" + artifact.mStats.CCReduce * 100 + "%\n";
                }
                if (artifact.mStats.Skill_Duration > 0)
                {
                    lore += "지속 시간: " + artifact.mStats.Skill_Duration + "초\n";
                }
                lore += "\n\n\"" + artifact.TextStats.PlayableText + "\"";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                if (artifact.mStats.Skill_Cooltime > 0)
                {
                    lore = "[" + artifact.TextStats.EngTitle + "] (CoolTime: " + artifact.mStats.Skill_Cooltime.ToString() + "Sec)\n"; ;

                }
                else
                {
                    lore = "[" + artifact.TextStats.EngTitle + "]\n";
                }
                lore += "\n" + artifact.TextStats.EngContensFormat + "\n\n";
                if (artifact.mStats.Heal > 0)
                {
                    lore += "Heal amount: " + artifact.mStats.Heal + "\n";
                }
                if (artifact.mStats.Atk > 0)
                {
                    lore += "Atk: +" + artifact.mStats.Atk * 100 + "%\n";
                }
                if (artifact.mStats.Def > 0)
                {
                    lore += "Def: +" + artifact.mStats.Def * 100 + "%\n";
                }
                if (artifact.mStats.AtkSpd > 0)
                {
                    lore += "Atk Speed: +" + artifact.mStats.AtkSpd * 100 + "%\n";
                }
                if (artifact.mStats.Crit > 0)
                {
                    lore += "Critical chance: +" + artifact.mStats.Crit * 100 + "%\n";
                }
                if (artifact.mStats.Spd > 0)
                {
                    lore += "Speed: +" + artifact.mStats.Spd * 100 + "%\n";
                }
                if (artifact.mStats.CooltimeReduce > 0)
                {
                    lore += "Cooltime Reduce: +" + artifact.mStats.CooltimeReduce * 100 + "%\n";
                }
                if (artifact.mStats.CCReduce > 0)
                {
                    lore += "CC Resistance: +" + artifact.mStats.CCReduce * 100 + "%\n";
                }
                if (artifact.mStats.Skill_Duration > 0)
                {
                    lore += "Druation: " + artifact.mStats.Skill_Duration + "Sec\n";
                }
                lore += "\n\n\"" + artifact.TextStats.EngPlayableText + "\"";

            }
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.tooltip.ShowTooltip(lore);
            }
            else
            {
                TutorialUIController.Instance.tooltip.ShowTooltip(lore);
            }
        }
    }
}
