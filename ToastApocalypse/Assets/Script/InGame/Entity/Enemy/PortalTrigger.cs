using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public static PortalTrigger Instance;

    public Room room;
    public Portal portal;
    public Enemy[] BossArr;
    public Enemy[] StageBossArr;
    private Enemy NowBoss;
    private int rand;
    private bool Spawned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Spawned = false;
            if (GameController.Instance.Level >= 5)
            {
                NowBoss = StageBossArr[0];
                Instantiate(NowBoss, transform.position, Quaternion.identity);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BossSpawn()
    {
        if (GameController.Instance.Level < 5 || Spawned == false)
        {
            if (GameController.Instance.Level % 2 == 1)//홀수 층 = 소환형 보스
            {
                NowBoss = BossArr[0];
            }
            else
            {
                NowBoss = BossArr[1];
            }
            Spawned = true;
            Instantiate(NowBoss, transform.position, Quaternion.identity);
            room.mEnemyFinder.SpawnAll = true;
        }
    }

    public void BossDeath()
    {
        portal.ShowPortal();
    }
}
