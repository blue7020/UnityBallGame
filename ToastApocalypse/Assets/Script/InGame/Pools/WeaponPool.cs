using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : ObjectPool<Weapon>
{
    public static WeaponPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PoolSetup();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override Weapon CreateNewObj(int id)
    {
        Weapon newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        newObj.transform.SetParent(Player.Instance.gameObject.transform);
        return newObj;
    }
}
