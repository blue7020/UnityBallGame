using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomController : MonoBehaviour
{

    public static BossRoomController Instance;

    public Room[] bossRoom;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            Instantiate(bossRoom[Player.Instance.mNowStage]);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
