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
        
        Player.Instance.Level++;
        Debug.Log("방 재시작, 현재 지하 " + Player.Instance.Level + "층");//4층까지 존재 
        Debug.Log(Player.Instance.Level);
        if (Player.Instance.Level == 5)
        {
            SceneManager.LoadScene(7);
            Player.Instance.transform.position = new Vector2(0, -10.5f);
            Player.Instance.Level=6;
        }
        else if (Player.Instance.Level>5)
        {
            StageClear.Instance.EndGame();
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
