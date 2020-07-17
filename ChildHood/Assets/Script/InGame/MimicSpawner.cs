using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicSpawner : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Transform MimicPos;
    [SerializeField]
    private Enemy[] mEnemyArr;
#pragma warning restore 0649
    private Enemy mSpawnEnemy;
    public eChestType Type;
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
