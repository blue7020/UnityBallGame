using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UsingArtifactInventory : MonoBehaviour, IPointerDownHandler
{
    public Artifacts art;
    public string tooltipText;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.Instance.NowActiveArtifact != null)
        {
            art = Player.Instance.NowActiveArtifact;
            if (GameSetting.Instance.Language == 0)
            {
                tooltipText = "["+art.TextStats.Title+ "]\n"+art.TextStats.ContensFormat+"\n-"+art.TextStats.PlayableText + "-";
                UIController.Instance.tooltip.ShowTooltip(tooltipText);
            }
            else if (GameSetting.Instance.Language == 1)
            {
                tooltipText = "[" + art.TextStats.EngTitle + "]\n" + art.TextStats.EngContensFormat + "\n-" + art.TextStats.EngPlayableText+"-";
                UIController.Instance.tooltip.ShowTooltip(art.TextStats.EngContensFormat);
            }
        }

    }
}
