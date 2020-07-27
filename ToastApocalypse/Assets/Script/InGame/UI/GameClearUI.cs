using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour,IPointerClickHandler
{
    public Text mClearText, mGuideText;

    //TODO 점수 및 재화 계산 UI 띄우기 

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        GameSetting.Instance.StageOpen[Player.Instance.mNowStage] = true;
        GameController.Instance.DestroyController();
        SceneManager.LoadScene(1);
    }
}
