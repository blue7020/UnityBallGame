using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : ObjectPool<UsingItem>
{
    public int Items;

    private void Awake()
    {
        PoolSetup();
        Items = mOriginArr.Length;
    }

    protected override UsingItem CreateNewObj(int id)
    {
        UsingItem newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        return newObj;
    }
}
