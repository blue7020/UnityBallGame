using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [SerializeField]
    public Room room;


    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;

    private void Start()
    {
        grid = GetComponentInChildren<GridController>();
    }

    public void InitialiseObjectSpawning()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }

    public void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn+1);

        for (int i=0; i<randomIteration; i++)
        {
            int randomPos = Random.Range(1, grid.availabePoints.Count-1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availabePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availabePoints.RemoveAt(randomPos);
            room.EnemyCount++;
        }
    }
}
