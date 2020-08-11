using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UsingArtifactInventory : MonoBehaviour, IPointerDownHandler
{
    public Artifacts art;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.Instance.NowActiveArtifact != null)
        {
            art = Player.Instance.NowActiveArtifact;
            if (GameSetting.Instance.Language == 0)
            {
                UIController.Instance.tooltip.ShowTooltip(art.TextStats.ContensFormat);
            }
            else if (GameSetting.Instance.Language == 1)
            {
                UIController.Instance.tooltip.ShowTooltip(art.TextStats.EngContensFormat);
            }
        }

    }
}
