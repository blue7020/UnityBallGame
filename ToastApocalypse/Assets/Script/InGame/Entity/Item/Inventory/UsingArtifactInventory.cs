using System.Collections;
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
                lore = "[" + art.TextStats.Title + "]\n" + art.TextStats.ContensFormat + "\n\n\"" + art.TextStats.PlayableText + "\"";
                UIController.Instance.tooltip.ShowTooltip(lore);
            }
            else if (GameSetting.Instance.Language == 1)
            {
                lore = "[" + art.TextStats.EngTitle + "]\n" + art.TextStats.EngContensFormat + "\n\n\"" + art.TextStats.EngPlayableText+"\"";
                UIController.Instance.tooltip.ShowTooltip(lore);
            }
        }

    }
}
