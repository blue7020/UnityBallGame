using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 left
    //2 top
    //3 right
    //4 bot

    private RoomTemplates Templates;
    private int rand;
    private bool spawned;

    private void Start()
    {
        spawned = false;
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    private void FixedUpdate()
    {
        if (spawned==false)
        {
            if (openingDirection == 1)//left
            {
                rand = Random.Range(0, Templates.leftRooms.Length);
                Instantiate(Templates.leftRooms[rand], transform.position, Templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)//top
            {
                rand = Random.Range(0, Templates.topRooms.Length);
                Instantiate(Templates.topRooms[rand], transform.position, Templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)//right
            {
                rand = Random.Range(0, Templates.rightRooms.Length);
                Instantiate(Templates.rightRooms[rand], transform.position, Templates.rightRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)//bot
            {
                rand = Random.Range(0, Templates.bottomRooms.Length);
                Instantiate(Templates.bottomRooms[rand], transform.position, Templates.bottomRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned==false&&spawned==false)
            {
                Instantiate(Templates.CloseRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
            spawned = true;
        }
    }
}
