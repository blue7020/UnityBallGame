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
    private bool Spawned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Spawned = false;
            if (GameController.Instance.StageLevel >= 5)
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
        if (GameController.Instance.StageLevel < 5 || Spawned == false)
        {
            if (GameController.Instance.StageLevel % 2 == 1)//홀수 층 = 소환형 보스
            {
                NowBoss = BossArr[0];
            }
            else
            {
                NowBoss = BossArr[1];
            }
            Spawned = true;
            Instantiate(NowBoss, transform.position, Quaternion.identity);
        }
    }

    public void BossDeath()
    {
        room.EnemyCount = 0;
        portal.ShowPortal();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (room.eType == eRoomType.Boss&& Spawned==false)
            {
                Instance.BossSpawn();
                room.EnemyCount++;
            }
        }
    }
}
