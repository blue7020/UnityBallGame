using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    public int PlayerID;
    public int PlayerSkillID;
    public bool Ingame;
    //저장해야할 것
    public int BGMSetting;
    public int SESetting;
    public int Language; //0 = 한국어 / 1 = 영어

    public bool[] StageOpen;
    public int NowStage;

    private const int CharacterCount = 2;
    public bool[] CharacterOpen;

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
        StageOpen = new bool[6];
        CharacterOpen = new bool[CharacterCount];
        StageOpen[0] = true;//1스테이지 오픈
        CharacterOpen[0] = true;//기본캐릭터 오픈
        Restart();

        PlayerSkillID = 0;
    }

    public void Restart()
    {
        NowStage = 0;
        CharacterOpen[1] = true;//햄에그캐릭터 오픈
        PlayerID = 0;
        Ingame = false;
    }
}
