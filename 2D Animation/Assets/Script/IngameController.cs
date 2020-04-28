using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameController : MonoBehaviour
{
    [SerializeField]
    private IngameUIController mUIController;
    private float mCoin;
    [SerializeField]
    private TextEffectPool mTextEffectPool;

    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private Transform mLeftPos, mRightPos;


    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public TextEffect GetTextEffect()
    {
        return mTextEffectPool.GetFromPool();
    }

    public void AddCoin(float value)
    {
        mCoin += value;
        mUIController.ShowCoin(mCoin);
    }


    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds three = new WaitForSeconds(3);
        while (true)
        {
            yield return three;
            bool IsLeft = Random.value < 0.5f;//0, 1보다 0.5f를 쓰는 것이 낫다. 50%에 더 근접한 결과임
            bool IsMale = Random.value < 0.5f;
            Transform spawnPos;
            if (IsLeft)
            {
                spawnPos = mLeftPos;
            }
            else
            {
                spawnPos = mRightPos;
            }
            Enemy enemy;
            if (IsMale)
            {
                enemy = mEnemyPool.GetFromPool(0);
            }
            else
            {
                enemy = mEnemyPool.GetFromPool(1);
            }
            enemy.transform.position = spawnPos.position;
            enemy.transform.rotation = spawnPos.rotation;
            enemy.STartMoving();
        }
    }
}
