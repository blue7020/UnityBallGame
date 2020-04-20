using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //이게 있어야 Scene에 관련된 기능을 사용할 수 있다.

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int mScore; //인스펙터로 점수 확인용
    private bool mbGameOver; //게임 오버가 True면 리셋 버튼 활성화

    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private UIController mUIController;
    [SerializeField]
    private ItemPool mItemPool;//아이템 목록
    [SerializeField]
    private int mItemSpawnWaveCount;

    [Header("EnermySpawn")]
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private float mSpawnXMin, mSpawnXMax, mSpawnZ;
    [SerializeField]
    private float mSpawnRate;
    private float mCurrentSpawnRate;
    private Coroutine mHazardRoutine;

    // Start is called before the first frame update
    void Start()
    {
        mUIController.ShowScore(mScore);
        mUIController.ShowMessageText("");
        mUIController.ShowRestart(false);
        mHazardRoutine = StartCoroutine(SpawnHazard());
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

    public void AddScore(int amount)
    {
        mScore += amount;
        mUIController.ShowScore(mScore);
    }

    public void Restart()//리셋 기능(완전히 게임 오버일 때)
    {
        SceneManager.LoadScene(0); //단점: 파일이 커지면 부하가 커서 느려진다.

        //이 방법은 Continue(이어하기)가 가능하다.
        //mbGameOver = false;
        //mScore = 0;
        //mPlayer.transform.position = Vector3.zero;
        //mPlayer.gameObject.SetActive(true);

        //if(mHazardRoutine == null)
        //{
        //    mHazardRoutine = StartCoroutine(SpawnHazard());
        //    //중첩으로 동작해야하는 케이스일 때 처음 실행이냐 실행 중이냐를 구분지어야 할 때, 이 코드를 응용해 사용할 수 있다.
        //    //ex) 지속시간이 3초인 스킬을 1초일 때 사용하면 아직 지속시간이니 현재 지속시간 = 최대 지속시간  
        //}
        //mUIController.ShowScore(mScore);
        //mUIController.ShowMessageText("");
        //mUIController.ShowRestart(false);

    }

    public void PlayerDie()
    {
        //스폰 멈추기 (완전히 게임 오버일 때)
        StopCoroutine(mHazardRoutine);
        //먼저 StopCoroutine을 해줘야 한다. 먼저 null로 바꾸면 멈춰지지 않음.
        mHazardRoutine = null;//정지 시의 데이터는 남아있기 때문에 확실히 정지시키기 위해 null로 바꿔준다.

        mbGameOver = true;
        //TODO UI 최종 스코어 표시(게임 오버 표시 밑에)

        mUIController.ShowMessageText("Game Over");
        mUIController.ShowRestart(true);
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
        int currentItemSpawnWaveCount = 0;

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
            if (currentItemSpawnWaveCount >= mItemSpawnWaveCount - 1)
            {
                Item item = mItemPool.GetFromPool(Random.Range(0, 3));
                item.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                         0,
                                                         mSpawnZ);
                currentItemSpawnWaveCount = 0;
            }
            else
            {
                currentItemSpawnWaveCount++;
            }
            
            yield return spawnRate;//==3 (Spawn rate 설정한 값이 3이니까)
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (mbGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }


}
