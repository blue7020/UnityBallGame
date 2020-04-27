using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    protected List<T>[] mEnemyPool;
    [SerializeField]
    protected T[] mEnemyArr;


    protected void PoolSetup()
    {
        mEnemyPool = new List<T>[mEnemyArr.Length];
        for (int i = 0; i < mEnemyArr.Length; i++)
        {
            mEnemyPool[i] = new List<T>();
        }
    }

    public T GetFromPool(int id=0)
    {
        for (int i = 0; i < mEnemyPool[id].Count; i++)
        {
            if (!mEnemyPool[id][i].gameObject.activeInHierarchy)
            {
                mEnemyPool[id][i].gameObject.SetActive(true);
                return mEnemyPool[id][i];
            }
        }
        T newObj = Instantiate(mEnemyArr[id]);
        mEnemyPool[id].Add(newObj);
        return newObj;
    }

    virtual protected T newObj(int id)
    {
        T newObj = Instantiate(mEnemyArr[id]);
        mEnemyPool[id].Add(newObj);
        return newObj;
    }
}
