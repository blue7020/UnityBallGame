using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomController : MonoBehaviour
{

    public static BossRoomController Instance;

    public Room[] bossRoom;
    private Room CurrentRoom;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            CurrentRoom=Instantiate(bossRoom[GameSetting.Instance.NowStage-1]);
            Player.Instance.CurrentRoom = CurrentRoom;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
