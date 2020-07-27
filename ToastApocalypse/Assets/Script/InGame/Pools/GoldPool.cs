using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPool : ObjectPool<DropGold>
{
    public static GoldPool Instance;
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
}
