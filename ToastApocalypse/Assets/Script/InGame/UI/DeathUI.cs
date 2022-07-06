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
            if (GameController.Instance.ReviveCode==1)
            {
                GameController.Instance.MainMenu();
                RewardAdsManager.Instance.ShowRewardAd(2);
            }
            else
            {
                GameController.Instance.MainMenu();
            }
        }
    }
}
