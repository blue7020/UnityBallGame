using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalDoor : MonoBehaviour
{
    public static PortalDoor Instance;

    public Image mWindow;
    public Text mTooltip, mTitle, mPortalButtonText,mMapText;
    public Button mPortalButton;
    public Animator mAnim;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (SaveDataController.Instance.mUser.GameClear)
            {
                mAnim.SetBool(AnimHash.ModePortal, true);
                mPortalButton.gameObject.SetActive(true);
                if (GameSetting.Instance.Language == 0)
                {
                    mMapText.text = "도전 모드";
                    mTitle.text = "도전 모드";
                    mTooltip.text = "1~6스테이지를 연속으로 플레이합니다.\n더 많은 시럽을 획득할 수 있으며,\n도중에 죽어도 보상을 받을 수 있습니다.";
                    mPortalButtonText.text = "도전한다";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    mMapText.text = "Challenge Mode";
                    mTitle.text = "Challenge Mode";
                    mTooltip.text = "Playing 1 to 6 stages in a row.\nYou can earn more syrup,\nand you can get compensation if you die on the way.";
                    mPortalButtonText.text = "Challenge";
                }
            }
            else
            {
                mAnim.SetBool(AnimHash.ModePortal, false);
                mPortalButton.gameObject.SetActive(false);
                if (GameSetting.Instance.Language == 0)
                {
                    mMapText.text= "도전 모드";
                    mTitle.text = "도전 모드";
                    mTooltip.text = "이 기능은 아직 개방되지 않았습니다.\n\n개방 조건: 1~6 스테이지 클리어";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    mMapText.text = "Challenge Mode";
                    mTitle.text = "Challenge Mode";
                    mTooltip.text = "This function is not open yet.\nRequirements: 1~6 stage clear";
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Portal()
    {
        GameSetting.Instance.NowStage = 1;
        GameSetting.Instance.NowStageRoom = MapController.Instance.Stage1;
        GameSetting.Instance.Ingame = true;
        GameSetting.Instance.ChallengeMode = true;
        SceneManager.LoadScene(2);
        SoundController.Instance.BGMChange(1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }
}
