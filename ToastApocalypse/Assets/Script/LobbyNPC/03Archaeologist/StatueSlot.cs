using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatueSlot : MonoBehaviour, IPointerClickHandler
{
    public int mID;
    public StatueStat mStatue;
    public StatueText mStatueText;
    public Image Icon;
    public Text Title, Price;

    public void SetData(int id)
    {
        mID = id;
        mStatue = LobbyStatueController.Instance.mStatInfoArr[mID];
        mStatueText = LobbyStatueController.Instance.mTextInfoArr[mID];
        Icon.sprite = LobbyStatueController.Instance.mSprites[mID];
        if (GameSetting.Instance.Language == 0)//한국어
        {
            Price.text = "가격: " + mStatueText.Price.ToString();
            Title.text = mStatueText.Name;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            Price.text = "Price: " + mStatueText.Price.ToString();
            Title.text = mStatueText.EngName;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StatueShop.Instance.ShowStatueInfo(mStatue, mStatueText);
    }
}
