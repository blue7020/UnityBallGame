using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PubSlot : MonoBehaviour, IPointerClickHandler
{
    public int mID;
    public ItemStat mItem;
    public Image Icon;
    public Text Title, Price;

    public void SetData(int id)
    {
        mID = id;
        mItem = SaveDataController.Instance.mItemInfoArr[mID];
        Icon.sprite = GameSetting.Instance.mItemArr[mID].mRenderer.sprite;
        if (GameSetting.Instance.Language == 0)//한국어
        {
            Price.text = "가격: " + mItem.OpenPrice.ToString();
            Title.text = mItem.Name;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            Price.text = "Price: " + mItem.OpenPrice.ToString();
            Title.text = mItem.EngName;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PubController.Instance.ShowItemInfo(mItem);
    }
}
