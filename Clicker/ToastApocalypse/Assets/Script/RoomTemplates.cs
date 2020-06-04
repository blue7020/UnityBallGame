using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject Room;
    public List<GameObject> rooms;

    public float waitTime;
    public bool spawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if (spawnedBoss == false)
        {
            if (waitTime <= 0)
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (i == rooms.Count - 1)
                    {
                        Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                    }
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        
    }
}
