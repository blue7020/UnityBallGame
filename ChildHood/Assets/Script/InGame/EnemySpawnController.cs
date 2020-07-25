using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public static EnemySpawnController Instance;

    public EnemyPool mEnemyPool;
    public int length;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i=0;i<length;i++)
        {
            int rand = Random.Range(0, 3);//현재는 몬스터가 3마리 뿐이니 이렇게 함.
            Enemy mEnemy = mEnemyPool.GetFromPool(rand);
            int randX = Random.Range(-9,10);
            int randY = Random.Range(-5, 6);
            mEnemy.transform.position += new Vector3(randX,randY, 0);
        }
    }
}
