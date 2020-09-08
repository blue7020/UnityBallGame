using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : ObjectPool<PlayerBullet>
{
    public static PlayerBulletPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        PoolSetup();
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected override PlayerBullet CreateNewObj(int id)
    {
        PlayerBullet newObj = Instantiate(mOriginArr[id]);
        newObj.transform.rotation = Quaternion.identity;
        mPools[id].Add(newObj);
        return newObj;
    }
}
