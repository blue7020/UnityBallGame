using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public static TitleController Instance;
    public bool isShowTitle;
    public int mLanguage;

    //저장용
    public int PlayCount;//목숨
    public bool TutorialClear;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (Application.systemLanguage==SystemLanguage.Korean)
            {
                mLanguage = 0;
            }
            else if (Application.systemLanguage == SystemLanguage.English)
            {
                mLanguage = 1;
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
