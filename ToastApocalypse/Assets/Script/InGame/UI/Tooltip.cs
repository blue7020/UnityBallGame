using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text tooltipContents;
    [SerializeField]
    private RectTransform BG;
#pragma warning restore 0649

    public void ShowTooltip(string contents)
    {
        gameObject.SetActive(true);
        tooltipContents.text = contents;
        Vector2 size = new Vector2(BG.sizeDelta.x, tooltipContents.preferredHeight);
        BG.sizeDelta = size;

    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    
}
