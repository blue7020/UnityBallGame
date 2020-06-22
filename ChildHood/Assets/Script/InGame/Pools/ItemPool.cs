using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : ObjectPool<UsingItem>
{
    [SerializeField]
    public Transform mPos;

    private void Awake()
    {
        PoolSetup();
    }

    protected override UsingItem CreateNewObj(int id)
    {
        UsingItem newObj = Instantiate(mOriginArr[id], mPos);
        mPools[id].Add(newObj);
        return newObj;
    }
}
