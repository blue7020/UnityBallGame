using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{

    public static EnemyPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PoolSetup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override Enemy CreateNewObj(int id)
    {
        Enemy newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        return newObj;
    }
}
