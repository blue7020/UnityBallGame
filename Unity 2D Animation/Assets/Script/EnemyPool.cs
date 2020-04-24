using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    //해당 파트를 빼내서 튜닝
    private void Awake()
    {
        PoolSetup();
    }

    protected override Enemy newObj(int id)
    {
        Enemy newObj = Instantiate(mEnemyArr[id]);
        mEnemyPool[id].Add(newObj);
        //하고싶은거 다해;
        return newObj;
    }
}


