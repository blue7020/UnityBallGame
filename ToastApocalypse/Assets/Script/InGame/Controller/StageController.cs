﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    public int mStageNum;
    public GameObject mPortal;

    public Room[] mStage;

    private void Start()
    {
        MapController.Instance.SetMapList(mStageNum, mStage);
        if (SaveDataController.Instance.mUser.StageOpen[mStageNum - 1] == false)
        {
            mPortal.gameObject.SetActive(false);
        }
        else
        {
            mPortal.gameObject.SetActive(true);
        }
        switch (mStageNum)
        {
            case 7:
                if (SaveDataController.Instance.mUser.EventPortalOpenCheckArr[0] == false)
                {
                    PortalScholar.Instance.mPortalArr[0].gameObject.SetActive(false);
                }
                else
                {
                    PortalScholar.Instance.mPortalArr[0].gameObject.SetActive(true);
                }
                break;
            case 8:
                if (SaveDataController.Instance.mUser.EventPortalOpenCheckArr[1] == false)
                {
                    PortalScholar.Instance.mPortalArr[1].gameObject.SetActive(false);
                }
                else
                {
                    PortalScholar.Instance.mPortalArr[1].gameObject.SetActive(true);
                }
                break;
            default:
                break;
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
