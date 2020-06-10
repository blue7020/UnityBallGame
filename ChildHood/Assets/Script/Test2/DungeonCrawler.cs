using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }

    public DungeonCrawler(Vector2Int StartPos)
    {
        Position = StartPos;
    }

    public Vector2Int Move(Dictionary<Direction,Vector2Int> directionMovementMap)
    {
        if (Player.Instance.Level<5)
        {
            Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
            Position += directionMovementMap[toMove];
            
        }
        return Position;
    }
}
