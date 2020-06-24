using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : ObjectPool<UsingItem>
{

    private void Awake()
    {
        PoolSetup();
    }

    protected override UsingItem CreateNewObj(int id, Vector3 Pos = new Vector3())
    {
        UsingItem newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        return newObj;
    }
}
