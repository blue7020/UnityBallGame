using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public static PlayerSelect Instance;

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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSelectController.Instance.NowPlayerID = GameSetting.Instance.PlayerID;
            MainLobbyCamera.Instance.PlayerSpawn = false;
            PlayerSelectController.Instance.mWindow.gameObject.SetActive(true);
        }
    }
}
