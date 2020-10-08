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
    public static DungeonCrawlerController Instance;

    public List<Vector2Int> positionsVisited;

    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>()
    {
        { Direction.up,Vector2Int.up},
        { Direction.left,Vector2Int.left},
        { Direction.down,Vector2Int.down},
        { Direction.right,Vector2Int.right}
    };

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Vector2Int> GenerateDungeon()
    {
        List<DungeonCrawler> dungeonCrwalers = new List<DungeonCrawler>();
        positionsVisited = new List<Vector2Int>();
        for (int i=0; i<GameSetting.Instance.CrawlerCount;i++)
        {
            dungeonCrwalers.Add(new DungeonCrawler(Vector2Int.zero));
        }
        int iterations = Random.Range(GameSetting.Instance.MinRoomLength, GameSetting.Instance.MaxRoomLength);
        for (int i=0; i<iterations; i++)
        {
            foreach(DungeonCrawler dungeonCrawler in dungeonCrwalers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }
        Debug.Log(positionsVisited.Count);
        return positionsVisited;
    }
}
