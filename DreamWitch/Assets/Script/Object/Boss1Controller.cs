using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    public Enemy mEnemy;

    public int mBoltCount, mDamageCooltimeCount;
    public bool isDamage,isAttacked;

    public Transform[] BlockSpawnPosArr;
    public GameObject[] BlockArr;
    public List<GameObject> BlockList;
    public List<int> RandomNumList;

    public Vector2 PlayerStartPos;

    public GameObject[] mDoorArr;//보스 방과 연결된 문

    private void Awake()
    {
        mEnemy.mFuntion = (() => { BossReset(); });
        BlockList = new List<GameObject>();
        RandomNumList = new List<int>();
    }

    public void BossReset()
    {
        mDoorArr[0].SetActive(false);
        mDoorArr[1].SetActive(true);
        RemoveObject();
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        gameObject.layer = 0;
        mEnemy.isDeath = false;
        mEnemy.isNoDamage = true;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Death, false);
        //컷씬에서 먹구름이 보스를 소환하는 것부터 시작하기 때문에 보스 입장 컷씬을 초기화하기
        gameObject.SetActive(false);
    }

    private void CreateUnDuplicateRandom(int min, int max)
    {
        int currentNumber = Random.Range(min, max);
        for (int i = 0; i < max;)
        {
            if (RandomNumList.Contains(currentNumber))
            {
                currentNumber = Random.Range(min, max);
            }
            else
            {
                RandomNumList.Add(currentNumber);
                i++;
            }
        }
    }

    public void RemoveObject()
    {
        if (BlockList.Count>0)
        {
            for (int i = 0; i < BlockList.Count; i++)
            {
                Destroy(BlockList[i].gameObject);
            }
            BlockList = new List<GameObject>();
        }
    }

    public void Boss1Damaged()
    {
        StopCoroutine(StartFallingBlock());
        isDamage = true;
        mEnemy.isNoDamage = false;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Damage_Boss, true);
        mBoltCount = 0;
    }

    public void FallingBlock()
    {
        int maxCount = Random.Range(3, 6);
        CreateUnDuplicateRandom(0, BlockSpawnPosArr.Length);
        for (int i=0; i<maxCount;i++)
        {
            int ObjRand = Random.Range(0, BlockSpawnPosArr.Length);
            if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
            {
                BlockList.Add(Instantiate(BlockArr[ObjRand], BlockSpawnPosArr[RandomNumList[i]]));
            }
            else
            {
                BlockList.Add(Instantiate(BlockArr[0], BlockSpawnPosArr[RandomNumList[i]]));
            }
        }
        RandomNumList = new List<int>();
    }

    public IEnumerator StartFallingBlock()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        SoundController.Instance.SESound(6);
        SoundController.Instance.SESound(21);
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        FallingBlock();
        yield return delay;
        SoundController.Instance.SESound(21);
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        FallingBlock();
        delay = new WaitForSeconds(7f);
        yield return delay;
    }

    public void DamageCooltime()
    {  
        if (mDamageCooltimeCount>=6)
        {
            mDamageCooltimeCount = 0;
            Player.Instance.CutSceneKnockBack(PlayerStartPos, 0.5f);
            StartCoroutine(mEnemy.Boss1AttackCooltime_1());
        }
        else
        {
            mDamageCooltimeCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBolt"))
        {
            if (isDamage)
            {
                Destroy(other.gameObject);
            }
            else
            {
                if (mBoltCount == 2)
                {
                    //보스 피격음 2 사운드
                    Destroy(other.gameObject);
                    Boss1Damaged();
                    mEnemy.StopCoroutine(mEnemy.Boss1AttackCooltime_1());
                }
                else
                {
                    //보스 피격음 2 사운드
                    mBoltCount++;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
