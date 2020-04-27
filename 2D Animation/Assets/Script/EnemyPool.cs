using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    [SerializeField]
    IngameController mIngameController;

    protected override Enemy newObj(int id)
    {
        Enemy newEnemy = Instantiate(mEnemyArr[id]);
        newEnemy.SetIngameController(mIngameController);
        mEnemyPool[id].Add(newEnemy);
        return base.newObj(id);
    }
}


