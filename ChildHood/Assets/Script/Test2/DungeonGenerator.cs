using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomControllers.Instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {

            int rand = UnityEngine.Random.Range(0, 2);
            if (rand == 0)
            {
                RoomControllers.Instance.LoadRoom("Empty1", roomLocation.x, roomLocation.y);
            }
            else if (rand == 1)
            {
                RoomControllers.Instance.LoadRoom("Empty2", roomLocation.x, roomLocation.y);
            }
            else
            {
                RoomControllers.Instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
            }


        }
    }
}
