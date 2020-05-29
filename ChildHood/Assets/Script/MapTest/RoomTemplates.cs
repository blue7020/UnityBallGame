using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] leftRooms;
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] bottomRooms;

    public GameObject CloseRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool SpawnBoss;
    public GameObject boss;

    private void Update()
    {
        if (waitTime<=0 && SpawnBoss ==false)
        {
            for (int i=0; i<rooms.Count; i++)
            {
                if (i==rooms.Count-1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    SpawnBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
