using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Instance;
    public Room[] Stage1;
    public Room[] Stage2;
    public Room[] Stage3;
    public Room[] Stage4;
    public Room[] Stage5;
    public Room[] Stage6;
    public Room[] Stage7;
    public Room[] Stage8;

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

    public void SetMapList(int id,Room[] map)
    {
        switch (id)
        {
            case 1:
                Stage1 = map;
                break;
            case 2:
                Stage2 = map;
                break;
            case 3:
                Stage3 = map;
                break;
            case 4:
                Stage4 = map;
                break;
            case 5:
                Stage5 = map;
                break;
            case 6:
                Stage6 = map;
                break;
            case 7:
                Stage7 = map;
                break;
            case 8:
                Stage8 = map;
                break;
            default:
                break;
        }
    }

    public void SetMapIngame()
    {
        switch (GameSetting.Instance.NowStage)
        {
            case 1:
                RoomControllers.Instance.rooms = Stage1;
                break;
            case 2:
                RoomControllers.Instance.rooms = Stage2;
                break;
            case 3:
                RoomControllers.Instance.rooms = Stage3;
                break;
            case 4:
                RoomControllers.Instance.rooms = Stage4;
                break;
            case 5:
                RoomControllers.Instance.rooms = Stage5;
                break;
            case 6:
                RoomControllers.Instance.rooms = Stage6;
                break;
            case 7:
                RoomControllers.Instance.rooms = Stage7;
                break;
            case 8:
                RoomControllers.Instance.rooms = Stage8;
                break;
            default:
                break;
        }
    }
}
