using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{

    private void Awake()
    {
        PoolSetup();
    }

    protected override Enemy CreateNewObj(int id, Vector3 Pos = new Vector3())
    {
        Enemy newObj = Instantiate(mOriginArr[id], Pos,Quaternion.identity);
        mPools[id].Add(newObj);
        return newObj;
    }
}
