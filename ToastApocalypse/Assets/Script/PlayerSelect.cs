using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //SceneManager.LoadScene(1);
            PlayerSelectController.Instance.NowPlayerID = GameSetting.Instance.PlayerID;
            MainLobbyCamera.Instance.PlayerSpawn = false;
            MainLobbyCamera.Instance.mPlayerObj=null;
            Destroy(MainLobbyPlayer.Instance.gameObject);
            PlayerSelectController.Instance.mWindow.gameObject.SetActive(true);
        }
    }
}
