using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicSpawner : MonoBehaviour
{
    public static MimicSpawner Instance;

    public Transform MimicPos;
    public Room room;
    public Enemy[] mEnemyArr;

    private Enemy mSpawnEnemy;
    public eChestType Type;

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

    private void Start()
    {
        if (Type == eChestType.Wood)
        {
            mSpawnEnemy = Instantiate(mEnemyArr[0], MimicPos);
            mSpawnEnemy.transform.SetParent(MimicPos);
        }
        else if (Type == eChestType.Silver)
        {
            mSpawnEnemy = Instantiate(mEnemyArr[1], MimicPos);
            mSpawnEnemy.transform.SetParent(MimicPos);
        }
        else if (Type == eChestType.Gold)
        {
            mSpawnEnemy = Instantiate(mEnemyArr[2], MimicPos);
            mSpawnEnemy.transform.SetParent(MimicPos);
        }
    }

}
