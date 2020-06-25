using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{

    private void Awake()
    {
        PoolSetup();
    }

    protected override Enemy CreateNewObj(int id)
    {
        Enemy newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        return newObj;
    }
}
