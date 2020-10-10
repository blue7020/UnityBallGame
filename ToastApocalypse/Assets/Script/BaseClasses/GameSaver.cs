using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : SaveDataController
{
    public static GameSaver Instance;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameSave()
    {
        Save();
    }

    public void GameLoad()
    {
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        //게임이 종료될 때 적용
        Save();
    }
}
