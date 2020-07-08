using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : ObjectPool<PlayerBullet>
{
    public static PlayerBulletPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        PoolSetup();
    }
    protected override PlayerBullet CreateNewObj(int id)
    {
        PlayerBullet newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        return newObj;
    }
}
