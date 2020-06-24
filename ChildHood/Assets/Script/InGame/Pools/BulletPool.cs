using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet>
{
    public static BulletPool Instance;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        PoolSetup();
    }
    protected override Bullet CreateNewObj(int id, Vector3 Pos = new Vector3())
    {
        Bullet newObj = Instantiate(mOriginArr[id], Pos,Quaternion.identity);
        mPools[id].Add(newObj);
        return newObj;
    }
}
