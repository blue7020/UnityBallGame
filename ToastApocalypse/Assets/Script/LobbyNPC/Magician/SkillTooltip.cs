using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillTooltip : MonoBehaviour,IPointerClickHandler
{
    public static SkillTooltip Instance;

    public Text mTitle, mLore;
    public Image mTooltip,mIcon;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowTooltip(string title, string lore, Sprite icon)
    {
        mIcon.sprite = icon;
        mTitle.text = title;
        mLore.text = lore;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mTooltip.gameObject.SetActive(false);
    }
}
