﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private float mSpawnXMin, mSpawnXMax, mSpawnZ;
    [SerializeField]
    private float mSpawnRate;
    private float mCurrentSpawnRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazard());
        //Coroutine은 코드 동작을 하다가 멈춘다.
        //동작 하다가 yield return 을 만나면 코드를 일단 멈춘다. => 다음 조건이 되는지 보고 다시 동작한다
        //(이펙트나 애니메이션 처리에서 많이 사용함. / Update보단 Start에서 StartCoroutine를 사용하는 것이 좋다.
        //Coroutine은 메서드 단위로 실행된다.

        //Coroutine을 잘 사용하면 Update 사용을 최소화하고 게임을 효율적으로 만들 수 있다.
        //Coroutine 안에 Coroutine을 넣을 수도 있으며 값이 IEnumerator인 것들만 return한다.


        //Invoke("test",5); //메서드를 딜레이 시켜 돌릴 때(파라미터 활용은 불가능함)
        //Invoke를 사용하는 일은 거의 없다. 예시로는 시간 단위로 자동 세이브를 시켜줄때 사용 가능
        //InvokeRepeating("save", 10, 300); 그리고 Invoke는 실행하고 중간에 못 멈춘다.

    }

    //멀티쓰레드 '같게' 보이게 하는 방법
    private IEnumerator SpawnHazard() //WaitForSeconds의 자료형이 IEnumerator이다.
    {
        WaitForSeconds pointThree = new WaitForSeconds(0.3f);
        WaitForSeconds spawnRate = new WaitForSeconds(mSpawnRate);
        int enemyCount = 3;
        int astCount = 10;
        int currentAstCount;
        int currentEnemyCount;
        float ratio = 1f / 3;//레시오(ratio)==비율
        //게임 전체를 진행하는 중, 조건(리스폰 시간)이 맞다면 전체 대기를 건다.
        while (true)
        {
            currentAstCount = astCount;
            currentEnemyCount = enemyCount;
            while (currentAstCount > 0 && currentEnemyCount > 0)
            {
                //RandomRange 는 구버전 호환용이기때문에 사용하면 안된다.
                float rand = Random.Range(0,1f);
                //float rand = Random.value; //무조건 0~1
                if (rand < ratio)
                {
                    Enermy enemy = mEnemyPool.GetFromPool();
                    enemy.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                         0,
                                                         mSpawnZ);
                    currentEnemyCount--;
                }
                else
                {
                    AsteroidMovement ast = mAstPool.GetFromPool(Random.Range(0, 3));
                    ast.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                         0,
                                                         mSpawnZ);
                    currentAstCount--;
                }
                yield return pointThree;
            }
            for (int i = 0; i < currentAstCount; i++)
            {
                AsteroidMovement ast = mAstPool.GetFromPool(Random.Range(0, 3));
                ast.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                     0,
                                                     mSpawnZ);
                yield return pointThree;
            }
            for (int i = 0; i< currentEnemyCount; i++)
            {
                Enermy enemy = mEnemyPool.GetFromPool();
                enemy.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                     0,
                                                     mSpawnZ);
                yield return pointThree;

            }
            yield return spawnRate;//==3 (Spawn rate 설정한 값이 3이니까)
        }
    }
    // Update is called once per frame
    private void Update()
    {
        //스폰
        new WaitForSeconds(5);
    }


}
