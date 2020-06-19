using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private eChest Type;
    [SerializeField]
    private ChestPool mChestPool;
    [SerializeField]
    private EnemyPool mEnemytPool;
    [SerializeField]
    public GameObject mItem;

    private void Awake()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                Type = eChest.Wood;
                Wood();
                break;
            case 1:
                Type = eChest.Silver;
                Silver();
                break;
            case 2:
                Type = eChest.Gold;
                Gold();
                break;
            default:
                Debug.LogError("Wrong ChestType");
                break;
        }
    }

    

    private void Wood()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.5f)//상자
        {
            Chest mChest = mChestPool.GetFromPool(0);
            mChest.transform.position = transform.position;
        }
        else//미믹
        {
            Enemy mEnemy = mEnemytPool.GetFromPool(0);
            mEnemy.transform.position = transform.position;
        }
    }

    private void Silver()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.3f)//상자
        {
            Chest mChest = mChestPool.GetFromPool(1);
            mChest.transform.position = transform.position;
        }
        else//미믹
        {
            Enemy mEnemy = mEnemytPool.GetFromPool(1);
            mEnemy.transform.position = transform.position;
        }
    }

    private void Gold()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.1f)//상자
        {
            Chest mChest = mChestPool.GetFromPool(2);
            mChest.transform.position = transform.position;
        }
        else//미믹
        {
            Enemy mEnemy = mEnemytPool.GetFromPool(3);
            mEnemy.transform.position = transform.position;
        }
    }
}
