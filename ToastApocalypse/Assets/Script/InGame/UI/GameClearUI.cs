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
        MainLobbyUIController.Instance.ShowParts();//이건 씬 이동시의 문제니까 알아서 해보기
    }
}
