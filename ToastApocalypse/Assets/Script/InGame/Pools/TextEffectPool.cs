using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : ObjectPool<TextEffect>
{
    public static TextEffectPool Instance;
    public Transform mCanvas;
    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
            PoolSetup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override TextEffect CreateNewObj(int id)
    {
        TextEffect newObj = Instantiate(mOriginArr[id], mCanvas);
        mPools[id].Add(newObj);
        return newObj;
    }


}
