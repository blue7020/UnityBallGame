using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text tooltipContents;
    public RectTransform BG;

    public void ShowTooltip(string contents)
    {
        gameObject.SetActive(true);
        tooltipContents.text = contents;
        Vector2 size = new Vector2(BG.sizeDelta.x, tooltipContents.preferredHeight/6);
        if (size.y >= 90)
        {
            size.y = 90;
            BG.sizeDelta = size;
        }
        else
        {
            BG.sizeDelta = size;
        }
        
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    
}
