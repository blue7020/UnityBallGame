using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3

};

public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>()
    {
        { Direction.up,Vector2Int.up},
        { Direction.left,Vector2Int.left},
        { Direction.down,Vector2Int.down},
        { Direction.right,Vector2Int.right}
    };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrwalers = new List<DungeonCrawler>();

        for(int i=0;i< dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrwalers.Add(new DungeonCrawler(Vector2Int.zero));
            if (i!= dungeonData.numberOfCrawlers-1)
            {
                RoomControllers.Instance.RoomLength = 0;
            }
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        for (int i=0; i<iterations; i++)
        {
            foreach(DungeonCrawler dungeonCrawler in dungeonCrwalers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }
        return positionsVisited;
    }
}
