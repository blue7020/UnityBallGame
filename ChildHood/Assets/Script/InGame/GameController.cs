using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //[SerializeField]
    //public GaugeBarPool mGaugeBarPool;

    public bool pause;

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
        DontDestroyOnLoad(gameObject);
        pause = false;
        UIController.Instance.CharacterImage();
    }
    

    public void GamePause()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1;
            
        }
        else
        {
            pause = true;
            Time.timeScale = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player.Instance.mInfoArr[Player.Instance.mID].Gold += 200;
            Player.Instance.mInfoArr[Player.Instance.mID].Atk += 5;
        }
    }

}
//TODO 씬 파일 만들어서 로비(캐릭터 선택까지)