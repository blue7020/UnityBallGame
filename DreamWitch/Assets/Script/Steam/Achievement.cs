using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System.Threading;

public class Achievement : MonoBehaviour
{
    public static Achievement Instance;

    public CGameID mGameID;
    public AppId_t appID;

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

    // Start is called before the first frame update
    void Start()
    {
        appID = SteamUtils.GetAppID();
        mGameID = new CGameID(SteamUtils.GetAppID());
    }

    public void GetAchivement(int id)
    {
        switch (id)
        {
            case 0://튜토리얼 클리어 시
                SteamUserStats.SetAchievement("Lucid_Dream");
                SteamUserStats.StoreStats();
                break;
            case 1://튜토리얼=1, 스테이지1=6 개의 별사탕을 모두 모았을 때
                SteamUserStats.SetAchievement("Konpeito_Lover");
                SteamUserStats.StoreStats();
                break;
            default:
                break;
        }
    }
}
