﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UsingArtifactInventory : MonoBehaviour, IPointerDownHandler
{
    public Artifacts art;
    public string lore;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.Instance.NowActiveArtifact != null)
        {
            art = Player.Instance.NowActiveArtifact;
            if (GameSetting.Instance.Language == 0)
            {
                lore = "["+art.TextStats.Title+"] (재사용 대기시간: " + art.mStats.Skill_Cooltime + "초)\n" + art.TextStats.ContensFormat + "\n\n\"" + art.TextStats.PlayableText + "\"";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                lore = "["+art.TextStats.EngTitle + "] (CoolTime: " + art.mStats.Skill_Cooltime + "Sec)\n" + art.TextStats.EngContensFormat + "\n\n\"" + art.TextStats.EngPlayableText+"\"";
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
