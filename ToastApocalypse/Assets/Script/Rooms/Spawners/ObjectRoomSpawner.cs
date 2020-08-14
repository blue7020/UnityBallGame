using System;
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
        int randomIteration = UnityEngine.Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = UnityEngine.Random.Range(1, grid.availabePoints.Count);
            Convert.ToInt32(randomPos);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availabePoints[randomPos], Quaternion.identity, transform) as GameObject;
            go.transform.position += new Vector3(0.5f, 0, 0);
            grid.availabePoints.RemoveAt(randomPos);
        }
        room.mEnemyFinder.SpawnAll = true;
    }
}
