using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathUI : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        if (GameSetting.Instance.ChallengeMode)
        {
            GameController.Instance.StageLevel -= 1;
            UIController.Instance.ShowClearTextInMode();
            gameObject.SetActive(false);
        }
        else
        {
            GameController.Instance.MainMenu();
        }
    }
}
