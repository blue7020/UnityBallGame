using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPool : ObjectPool<DropGold>
{
    private void Awake()
    {
        PoolSetup();
    }
}
