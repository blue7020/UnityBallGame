using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    public int mStageNum;
    public GameObject mPortal;
    public bool[] PortalType;//0=일반(false가 기본), 1=할로윈 2=크리스마스(이벤트맵은 true일때 활성화)

    public Room[] mStage;

    private void Start()
    {
        if (PortalType[0] == false&&SaveDataController.Instance.mUser.StageOpen[mStageNum - 1] == false)
        {
            mPortal.gameObject.SetActive(false);
        }
        else
        {
            mPortal.gameObject.SetActive(true);
        }
        if (PortalType[1] == true && SaveDataController.Instance.mUser.StageOpen[mStageNum - 1] == false)
        {
            mPortal.gameObject.SetActive(false);
        }
        else
        {
            mPortal.gameObject.SetActive(true);
        }
        if (PortalType[2] == true && SaveDataController.Instance.mUser.StageOpen[mStageNum - 1] == false)
        {
            mPortal.gameObject.SetActive(false);
        }
        else
        {
            mPortal.gameObject.SetActive(true);
        }
    }


    public void Portal()
    {
        GameSetting.Instance.NowStageRoom = mStage;
        GameSetting.Instance.Ingame = true;
        GameSetting.Instance.NowStage = mStageNum;
        SceneManager.LoadScene(2);
        SoundController.Instance.BGMChange(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MainLobbyUIController.Instance.mPortalButton.onClick.RemoveAllListeners();
            MainLobbyUIController.Instance.mPortalButton.onClick.AddListener(() => { Portal(); });
            MainLobbyUIController.Instance.mPortalButton.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MainLobbyUIController.Instance.mPortalButton.gameObject.SetActive(false);
        }
    }
}
