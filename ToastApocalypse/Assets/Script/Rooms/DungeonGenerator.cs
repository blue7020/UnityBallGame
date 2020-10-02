using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private List<Vector2Int> dungeonRooms;
    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.Instance.GenerateDungeon();
        SpawnRooms(dungeonRooms);
    }

    public void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomControllers.Instance.LoadRoom(0, 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomControllers.Instance.LoadRoom(RoomControllers.Instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
        }
    }
}
