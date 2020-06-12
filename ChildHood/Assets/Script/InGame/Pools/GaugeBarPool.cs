using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeBarPool : ObjectPool<GaugeBar>
{
    public static GaugeBarPool Instance;

    [SerializeField]
    private Transform mGaugeBarArea;

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
        DontDestroyOnLoad(gameObject);
        PoolSetup();
    }

    protected override GaugeBar CreateNewObj(int id)
    {
        GaugeBar newObj = Instantiate(mOriginArr[id], mGaugeBarArea);
        mPools[id].Add(newObj);
        return newObj;
    }
}
