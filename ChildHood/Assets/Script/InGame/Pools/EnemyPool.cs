using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    [SerializeField]
    public Transform mPos;

    private void Awake()
    {
        PoolSetup();
    }

    protected override Enemy CreateNewObj(int id)
    {
        Enemy newObj = Instantiate(mOriginArr[id], mPos);
        mPools[id].Add(newObj);
        return newObj;
    }
}
