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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Type == eRoomType.End)
            {
                room.EnemyCount = 1;

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
            if (Player.Instance.CurrentRoom.EnemyCount == 0)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
