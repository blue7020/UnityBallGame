using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePool : ObjectPool<Statue>
{
    public static StatuePool Instance;
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

    protected override Statue CreateNewObj(int id)
    {
        Statue newObj = Instantiate(mOriginArr[id]);
        newObj.transform.rotation = Quaternion.identity;
        mPools[id].Add(newObj);
        return newObj;
    }
}
