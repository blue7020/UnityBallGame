using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public static TitleController Instance;
    public bool isShowTitle;

    public int mLanguageCount = 1; 
    public int mLanguage;

    public int NowStage;

    public int PlayCount;//목숨

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SaveDataController.Instance.LoadGame();
            SaveDataController.Instance.Save();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
