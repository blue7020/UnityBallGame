using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : ObjectPool<TextEffect>
{
    [SerializeField]
    private Transform mCanvas;
    protected override TextEffect newObj(int id)
    {
        TextEffect newEffect = Instantiate(mEnemyArr[id], mCanvas);
        mEnemyPool[id].Add(newEffect);
        return newEffect;
    }

}
