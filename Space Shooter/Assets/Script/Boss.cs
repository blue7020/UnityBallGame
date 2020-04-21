using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int mMaxHP, mCurrentHP;
    private bool mbInvincible;

    [SerializeField]
    private Vector3 mSpawnStartPos, mSpawnEndPos;
    [SerializeField]
    private float mSpawnTime;
    private EffectPool mEffectPool;

    [SerializeField]
    private Transform[] mPosArr;
    [SerializeField]
    private float mPosMoveTime;

    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private GameController mGameController;

    private Coroutine mPhaseRoutine;

    //아래와 같음
    //public bool IsAlive { get { return mCurrentHP > 0; } }
    public bool IsAlive()
    {
        return mCurrentHP > 0;
    }

    private void Awake()
    {
        mEffectPool = GameObject.FindGameObjectWithTag("EffectPool").GetComponent<EffectPool>();
    }

    private void OnEnable()
    {
        transform.position = mSpawnStartPos;
        mbInvincible = true;
        mCurrentHP = mMaxHP;
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        Vector3 StartSize = Vector3.one * 1.5f;
        Vector3 EndSize = Vector3.one * 3f;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float CurrentTime = 0;
        float progress = 0;
        while (CurrentTime <= mSpawnTime)
        {
            yield return frame;
            CurrentTime += Time.fixedDeltaTime;
            progress = CurrentTime / mSpawnTime;
            //선형 보간 (Linear interpolate)
            transform.position = Vector3.Lerp(mSpawnStartPos, mSpawnEndPos, progress);
            transform.localScale = Vector3.Lerp(StartSize, EndSize, progress);



        }
        mbInvincible = false;
        mPhaseRoutine = StartCoroutine(Phase());
    }

    private IEnumerator Phase()
    {
        //이동 후 3발 쏘고 새로운 위치로 이동 (이동 패턴은 0-> 1-> 2 ->1 ->0)
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        WaitForSeconds pointThree = new WaitForSeconds(0.3f);
        float CurrentTime = 0;
        float progress = 0;
        int StartIndex = 1;
        int EndIndex = 0;
        bool bAscend = true;//처음에 인덱스 값이 올라갈지
        while (true)
        {
            Vector3 StartPos = mPosArr[StartIndex].position;
            Vector3 EndPos = mPosArr[EndIndex].position;
            progress = 0;
            CurrentTime = 0;
            //이동
            while (CurrentTime <= mPosMoveTime)
            {
                yield return frame;
                CurrentTime += Time.fixedDeltaTime;
                progress = CurrentTime / mPosMoveTime;
                //선형 보간 (Linear interpolate)
                transform.position = Vector3.Lerp(StartPos, EndPos, progress);
                
            }
            //사격
            for (int i=0; i<3; i++)
            {
                Bolt bolt = mBoltPool.GetFromPool(1);
                bolt.transform.position = mBoltPos.position;
                bolt.transform.LookAt(mPlayer.transform);
                bolt.ResetDir();
                yield return pointThree;
            }

            //로직이 꼬이면 안되는 코드.
            StartIndex = EndIndex;
            if (bAscend)
            {
                EndIndex++;
                bAscend = EndIndex < mPosArr.Length - 1;//bAcend가 true면 EndIndex++
            }
            else
            {
                EndIndex--;
                bAscend = EndIndex == 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolt"))
        {
            if (!mbInvincible)
            {
                mCurrentHP--;
                Debug.LogFormat("{0}/{1}", mCurrentHP, mMaxHP);
                if (mCurrentHP <= 0)
                {
                    mbInvincible = true;
                    StopCoroutine(mPhaseRoutine);
                    StartCoroutine(Die());
                }
            }
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpAst);
            effect.transform.position = other.ClosestPoint(transform.position);//ClosestPoint = 벡터3(other)에서 가장 가까운 지점
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator Die()
    {
        WaitForSeconds pointThree = new WaitForSeconds(0.3f);
        for (int i=0;i<10; i++)
        {

            mGameController.AddScore(20);
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpEnemy);
            effect.transform.position = transform.position + Random.insideUnitSphere * 4 + Vector3.up;
            yield return pointThree;
        }
        gameObject.SetActive(false);
        
    }
}
