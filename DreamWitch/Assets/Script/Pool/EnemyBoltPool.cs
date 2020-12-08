using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltPool : ObjectPool<EnemyBolt>
{
    public static EnemyBoltPool Instance;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            PoolSetup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override EnemyBolt CreateNewObj(int id)
    {
        EnemyBolt newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        return newObj;
    }
}
