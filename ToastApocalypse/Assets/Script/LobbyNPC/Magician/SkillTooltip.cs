using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillTooltip : MonoBehaviour,IPointerClickHandler
{
    public Text mTitle, mLore;
    public Image mTooltip,mIcon;

    public void SetData(string title, string lore, Sprite icon)
    {
        mTitle.text = title;
        mLore.text = lore;
        mIcon.sprite = icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mTooltip.gameObject.SetActive(false);
    }
}
