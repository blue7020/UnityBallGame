using System.Collections;
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
    [SerializeField]
    public ePortalType Type;
    [SerializeField]
    private StageClear mClear;
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
        //버프초기화
        for (int i = 0; i < Player.Instance.NowBuff.Count; i++)
        {
            if (Player.Instance.NowBuffActive[i] == true)
            {
                StopCoroutine(Player.Instance.NowBuff[i]);
                if (Player.Instance.NowBuffType[i] == eBuffType.Atk)
                {
                    Player.Instance.mInfoArr[Player.Instance.mID].Atk -= Player.Instance.NowBuffValue[i];
                    Debug.Log("off");
                }
                if (Player.Instance.NowBuffType[i] == eBuffType.AtkSpd)
                {
                    Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd += Player.Instance.NowBuffValue[i];
                    Debug.Log("off");
                }
                if (Player.Instance.NowBuffType[i] == eBuffType.Spd)
                {
                    Player.Instance.mInfoArr[Player.Instance.mID].Spd -= Player.Instance.NowBuffValue[i];
                    Debug.Log("off");
                }
                if (Player.Instance.NowBuffType[i] == eBuffType.Def)
                {
                    Player.Instance.mInfoArr[Player.Instance.mID].Def -= Player.Instance.NowBuffValue[i];
                    Debug.Log("off");
                }
            }
            Player.Instance.NowBuff.RemoveAt(i);
            Player.Instance.NowBuffActive.RemoveAt(i);
            Player.Instance.NowBuffValue.RemoveAt(i);
            Player.Instance.NowBuffType.RemoveAt(i);
        }
        Player.Instance.Level++;
        Debug.Log("방 재시작, 현재 지하 " + Player.Instance.Level + "층");//4층까지 존재 
        Debug.Log(Player.Instance.Level);
        if (Player.Instance.Level == 5)
        {
            SceneManager.LoadScene(6);
            Player.Instance.transform.position = new Vector2(0, -10.5f);
            Player.Instance.Level=6;
        }
        else if (Player.Instance.Level>5)
        {
           mClear.gameObject.SetActive(true);
           mClear.EndGame();
        }
        else
        {
            SceneManager.LoadScene(0);
            Player.Instance.transform.position = new Vector2(0, 0);
        }
        UIController.Instance.ShowHP();
        UIController.Instance.ShowGold();


    }
}
