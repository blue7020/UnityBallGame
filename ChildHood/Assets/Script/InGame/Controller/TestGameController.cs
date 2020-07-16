using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameController : MonoBehaviour
{
    public static TestGameController Instance;

    public bool pause;
    public bool GotoMain;

    public int Level;//현재 층
    public int MapLevel;//현재 스테이지
    public int StageHP;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StageHP = 4;
        GotoMain = false;
        pause = false;
    }

    private void Start()
    {
        if (GotoMain==false)
        {
            DontDestroyOnLoad(gameObject);
        }
        MapLevel = 1;//나중에 맵 선택 시 해당 레벨을 부여하는 것으로 수정

        Level = 1;
    }
}