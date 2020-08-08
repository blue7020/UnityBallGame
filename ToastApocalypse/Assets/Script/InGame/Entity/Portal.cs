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
        //TODO 포탈 생성 이펙트 후 setactive true
        gameObject.SetActive(true);
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
        for (int i = 0; i < Player.Instance.buffIncrease.Length; i++)
        {
            Player.Instance.buffIncrease[i] = 0;
        }
        Player.Instance.Nodamage = false;
        if (GameController.Instance.Level == 5)
        {
            SceneManager.LoadScene(3);
            Player.Instance.transform.position = new Vector2(0, -10.5f);
            GameController.Instance.Level=6;
        }
        else if (GameController.Instance.Level>5)
        {
            UIController.Instance.ShowClearText();
            if (GameSetting.Instance.StageOpen[5]==false)
            {
                GameSetting.Instance.StageOpen[GameSetting.Instance.NowStage] = true;
            }
        }
        else
        {
            SceneManager.LoadScene(2);
            Player.Instance.transform.position = new Vector2(0, 0);
            UIController.Instance.StartCoroutine(UIController.Instance.ShowLevel());
        }
        UIController.Instance.ShowHP();
        UIController.Instance.ShowGold();


    }
}
