using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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
        SceneManager.LoadScene(0);

    }
}
