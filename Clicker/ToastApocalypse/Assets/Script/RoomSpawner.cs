using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 bot door
    //2 top door
    //3 left door
    //4 right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        rand = Random.Range(0, 5);
        if (templates.spawnedBoss == false)
        {
            Invoke("Spawn", 0.2f);
        }

    }

    void Spawn()
    {
        if (spawned == false)
        {
            templates.rooms.Add(gameObject);
            Instantiate(templates.Room, transform.position, templates.Room.transform.rotation);
        }

        spawned = true;

    }
}
