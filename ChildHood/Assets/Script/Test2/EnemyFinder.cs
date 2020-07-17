using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eRoomType
{
    End,
    Normal
}

public class EnemyFinder : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Room room;
    [SerializeField]
    private GameObject mDoor;
    [SerializeField]
    private eRoomType Type;
#pragma warning restore 0649

    public int mMonsterCount;

    private void Start()
    {
        mMonsterCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Type == eRoomType.End)
            {
                room.EnemyCount = 1;
                Player.Instance.NowEnemyCount = room.EnemyCount;

            }
            if (Player.Instance.NowEnemyCount > 0)
            {
                mDoor.gameObject.SetActive(true);
                //Player.Instance.TargetReset();
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Player.Instance.NowEnemyCount == 0)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
