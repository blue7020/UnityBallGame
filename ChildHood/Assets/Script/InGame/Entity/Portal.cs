using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public static Portal Instance;
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
    }

    public void ShowPortal()
    {
        //TODO 포탈 생성 이펙트
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RoomControllers.Instance.RestartRoom();
        }
    }

    public void nextroom()
    {
        
        Player.Instance.Level++;
        Debug.Log(Player.Instance.Level);
        if (Player.Instance.Level == 5)
        {

            Debug.Log("potal보스방 진입");
            SceneManager.LoadScene(7);
            Player.Instance.transform.position = new Vector2(0, -10.5f);
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
