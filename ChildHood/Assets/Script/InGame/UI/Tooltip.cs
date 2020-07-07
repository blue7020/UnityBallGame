using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private Text tooltipContents;
    [SerializeField]
    private RectTransform BG;

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
