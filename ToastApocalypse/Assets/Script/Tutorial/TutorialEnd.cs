using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEnd : MonoBehaviour
{
    public Text mTitle, mGuideText,mGiftText;
    public Image mClearUI;

    public int ShyrupAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.Instance.pause = true;
            Player.Instance.mRB2D.velocity = Vector3.zero;
            Player.Instance.Stun = true;
            if (GameSetting.Instance.Language == 0)
            {//한국어
                mTitle.text = "튜토리얼 클리어!";
                mGuideText.text = "터치 시 로비로 이동합니다.";
                if (GameSetting.Instance.TutorialEnd == false)
                {
                    mGiftText.text = "획득한 시럽: +" + ShyrupAmount;
                    GameSetting.Instance.TutorialEnd = true;
                    GameSetting.Instance.NPCOpen[1]=true;
                }
            }
            else if (GameSetting.Instance.Language == 1)
            {//영어
                mTitle.text = "Tutorial Clear!";
                mGuideText.text = "Touch to move to the lobby.";
                if (GameSetting.Instance.TutorialEnd == false)
                {
                    mGiftText.text = "Syrup: +" + ShyrupAmount;
                    GameSetting.Instance.TutorialEnd = true;
                    GameSetting.Instance.NPCOpen[1] = true;
                }
            }
            if (GameSetting.Instance.Syrup + ShyrupAmount >= 99999)
            {
                GameSetting.Instance.Syrup = 99999;
            }
            else
            {
                GameSetting.Instance.Syrup += ShyrupAmount;
            }
            mClearUI.gameObject.SetActive(true);
        }
    }
}
