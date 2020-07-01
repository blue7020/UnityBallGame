using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    public int BGMSetting;
    public int SESetting;
    public int Language; //0 = 한국어 / 1 = 영어

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
        BGMSetting = 3;
        SESetting = 3;
    }
}
