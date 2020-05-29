using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 bot
    //2 top
    //3 left
    //4 right

    private RoomTemplates Templates;
    private int rand;
    private bool spawned;

    public float waitTime = 4f;

    private void Awake()
    {
        spawned = false;
        Destroy(gameObject,waitTime);
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    private void Update()
    {
        if (!spawned)
        {

            for (int i=0; i<3;i++)
            {
                if (openingDirection == 1)
                {
                    rand = Random.Range(0, Templates.bottomRooms.Length);
                    Instantiate(Templates.bottomRooms[rand], transform.position, Templates.bottomRooms[rand].transform.rotation);
                }
                else if (openingDirection == 2)
                {
                    rand = Random.Range(0, Templates.topRooms.Length);
                    Instantiate(Templates.topRooms[rand], transform.position, Templates.topRooms[rand].transform.rotation);
                }
                else if (openingDirection == 3)
                {
                    rand = Random.Range(0, Templates.leftRooms.Length);
                    Instantiate(Templates.leftRooms[rand], transform.position, Templates.leftRooms[rand].transform.rotation);

                }
                else if (openingDirection == 4)
                {
                    rand = Random.Range(0, Templates.rightRooms.Length);
                    Instantiate(Templates.rightRooms[rand], transform.position, Templates.rightRooms[rand].transform.rotation);

                }
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
