using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPool : ObjectPool<ChestController>
{
    [SerializeField]
    private List<GameObject>[] mPool;

    private void Awake()
    {
        PoolSetup();
    }
}
