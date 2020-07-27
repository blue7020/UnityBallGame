
using UnityEngine;

[CreateAssetMenu(fileName ="DeongeonGenerationData.asset",menuName ="DungeonGenarationData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
