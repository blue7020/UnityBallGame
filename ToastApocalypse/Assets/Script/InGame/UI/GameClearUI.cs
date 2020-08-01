using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameClearUI : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        GameSetting.Instance.StageOpen[Player.Instance.mNowStage] = true;
        GameSetting.Instance.GetParts(GameSetting.Instance.NowStage);
        GameController.Instance.MainMenu();
    }
}
