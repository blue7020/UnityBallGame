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
    [SerializeField]
    private Room room;
    [SerializeField]
    private GameObject mDoor;
    [SerializeField]
    private eRoomType Type;

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
            if (room.EnemyCount > 0)
            {
                mDoor.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Player.Instance.NowEnemyCount == 0)
            {
                mDoor.gameObject.SetActive(false);
            }
        }
    }
}
