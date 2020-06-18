using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private eChest Type;
    [SerializeField]
    private ChestPool mChestPool;
    [SerializeField]
    private GameObject[] mMimic;
    private bool IsMimic;
    [SerializeField]
    public GameObject mItem;

    private void Start()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                Type = eChest.Wood;
                //mchest와 미믹의 이미지
                break;
            case 1:
                Type = eChest.Silver;
                break;
            case 2:
                Type = eChest.Gold;
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
        if (rand > 0.49f)//상자
        {

        }
        else//미믹
        {
            mChestPool.GetFromPool(3);
        }
    }

    private void Silver()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.29f)//상자
        {

        }
        else//미믹
        {
            mChestPool.GetFromPool(4);
        }
    }

    private void Gold()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.09f)//상자
        {

        }
        else//미믹
        {
            mChestPool.GetFromPool(5);
        }
    }
}
