using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeBarPool : ObjectPool<GaugeBar>
{
    public static GaugeBarPool Instance;

    public Transform mGaugeBarArea;

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

    protected override GaugeBar CreateNewObj(int id, Vector3 Pos = new Vector3())
    {
        GaugeBar newObj = Instantiate(mOriginArr[id], mGaugeBarArea);
        mPools[id].Add(newObj);
        return newObj;
    }
}
