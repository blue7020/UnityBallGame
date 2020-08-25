using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : InformationLoader
{
    public static EnemyController Instance;

    [SerializeField]
    public MonsterStat[] mInfoArr;

    public MonsterStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.MONSTER_STAT);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
