﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ePortalType
{
    Stage,
    level
}

public class Portal : MonoBehaviour
{

    public static Portal Instance;
    public ePortalType Type;
    public StageClear mClear;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowPortal()
    {
        gameObject.SetActive(true);
        //TODO 포탈 생성 이펙트
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nextroom();
        }
    }

    public void nextroom()
    {
        GameController.Instance.Level++;
        Debug.Log("현재 지하 " + GameController.Instance.Level + "층");//4층까지 존재 
        if (GameController.Instance.Level == 5)
        {
            SceneManager.LoadScene(3);
            Player.Instance.transform.position = new Vector2(0, -10.5f);
            GameController.Instance.Level=6;
        }
        else if (GameController.Instance.Level>5)
        {
            GameSetting.Instance.StageOpen[Player.Instance.mNowStage] = true;
            GameController.Instance.DestroyController();//TODO 나중에 점수 계산 ui 나올때 버튼  기능으로 넣기
            UIController.Instance.StageClear();
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
            Player.Instance.transform.position = new Vector2(0, 0);
        }
        UIController.Instance.ShowHP();
        UIController.Instance.ShowGold();


    }
}
