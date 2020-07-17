﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    [SerializeField]
    private int mStageNum;

    [SerializeField]
    private GameObject mPortal;

    private void Awake()
    {
        if (GameSetting.Instance.StageOpen[mStageNum-1]==false)
        {
            mPortal.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);//TODO 스테이지마다 다르게
        }
    }
}
