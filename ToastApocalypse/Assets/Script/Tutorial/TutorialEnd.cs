using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEnd : MonoBehaviour
{
    public static TutorialEnd Instance;

    public Text mTitle, mGuideText,mGiftText;
    public Image mClearUI;
    public bool IsClear;

    public int SyrupAmount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            IsClear = false;
            if (GameSetting.Instance.Language == 0)
            {//한국어
                mTitle.text = "튜토리얼 클리어!";
                mGuideText.text = "터치 시 로비로 이동합니다.";
                if (SaveDataController.Instance.mUser.TutorialEnd == false)
                {
                    mGiftText.text = "획득한 시럽: +" + SyrupAmount;
                    SaveDataController.Instance.mUser.TutorialEnd = true;
                    SaveDataController.Instance.mUser.NPCOpen[1] = true;
                }
                else
                {
                    mGiftText.text = "";
                }
            }
            else if (GameSetting.Instance.Language == 1)
            {//영어
                mTitle.text = "Tutorial Clear!";
                mGuideText.text = "Touch to move to the lobby.";
                if (SaveDataController.Instance.mUser.TutorialEnd == false)
                {
                    mGiftText.text = "Syrup: +" + SyrupAmount;
                    SaveDataController.Instance.mUser.TutorialEnd = true;
                    SaveDataController.Instance.mUser.NPCOpen[1] = true;
                }
                else
                {
                    mGiftText.text = "";
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameClear()
    {
        GameController.Instance.pause = true;
        Player.Instance.mRB2D.velocity = Vector3.zero;
        Player.Instance.Stun = true;
        GameSetting.Instance.GetSyrup(SyrupAmount);
        SaveDataController.Instance.mUser.StagePartsget[0] = true;
        mClearUI.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsClear==false)
            {
                TutorialDialog.Instance.ShowDialog();
            }
        }
    }
}
