using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet>
{
    [SerializeField]
    private List<Bullet>[] mPool;

    private void Start()
    {
        PoolSetup();
    }
}
