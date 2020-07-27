using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public Room room;
    public GameObject mDoor;
    public bool SpawnAll;
    public bool WallOn;

    private void Start()
    {
        mDoor.gameObject.SetActive(false);
        SpawnAll = false;
        WallOn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&WallOn==false)
        {
            WallOn = true;
            mDoor.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&SpawnAll ==true)
        {
            if (Player.Instance.CurrentRoom.EnemyCount == 0)
            {
                gameObject.SetActive(false);
            }
        }
        
        
    }
}
