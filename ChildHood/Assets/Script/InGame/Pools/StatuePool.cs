using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePool : ObjectPool<Statue>
{
    private void Awake()
    {
        PoolSetup();
    }
}
