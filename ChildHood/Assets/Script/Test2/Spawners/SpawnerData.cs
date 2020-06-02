using UnityEngine;

[CreateAssetMenu(fileName ="Spawner.asset", menuName = "Spawners/Spawn")]

public class SpawnerData : ScriptableObject
{
    public GameObject itemToSpawn;
    public int minSpawn;
    public int maxSpawn;
}
