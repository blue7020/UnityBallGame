using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public Room room;
    public GameObject mDoor;
    public eRoomType Type;
    public bool SpawnAll;

    private void Start()
    {
        mDoor.gameObject.SetActive(false);
        SpawnAll = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mDoor.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (SpawnAll==true)
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
}
