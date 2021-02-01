using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : Enemy
{

    public int mBoltCount, mDamageCooltimeCount;
    public bool isDamage,isAttacked;

    public Transform[] BlockSpawnPosArr;
    public GameObject[] BlockArr;
    public List<GameObject> BlockList;
    public List<int> RandomNumList;

    public Vector2 PlayerStartPos;

    public GameObject[] mObj;//보스 방과 연결된 문과 출구 사다리

    private void Awake()
    {
        mFuntion = (() => { BossReset(); });
        BlockList = new List<GameObject>();
        RandomNumList = new List<int>();
    }

    public void BossReset()
    {
        mObj[0].SetActive(false);
        mObj[1].SetActive(true);
        mObj[2].SetActive(false);
        mObj[3].SetActive(true);
        RemoveObject();
        mAnim.SetBool(AnimHash.Enemy_Attack, false);
        gameObject.layer = 0;
        isDeath = false;
        isNoDamage = true;
        mAnim.SetBool(AnimHash.Enemy_Death, false);
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

    public void FallingBlock()
    {
        int maxCount = Random.Range(3, 6);
        CreateUnDuplicateRandom(0, BlockSpawnPosArr.Length);
        for (int i=0; i<maxCount;i++)
        {
            int ObjRand = Random.Range(0, BlockSpawnPosArr.Length);
            if (mCurrentHP <= mMaxHP / 2)
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

    public void BossSpawn()
    {
        mAnim.SetBool(AnimHash.Enemy_Spawn, true);
        mCurrentHP = mMaxHP;
    }

    public void Boss1AttackEnd()
    {
        mAnim.SetBool(AnimHash.Enemy_Attack, false);
        isNoDamage = true;
        isDamage = false;
        StartCoroutine(Boss1AttackCooltime_1());
    }

    public void Boss1Attack()
    {
        mBoltCount = 0;
        isNoDamage = true;
        mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(StartFallingBlock());
    }

    public void Boss1Cooltime()
    {
        if (mDamageCooltimeCount >= 6)
        {
            mDamageCooltimeCount = 0;
            Player.Instance.CutSceneKnockBack(PlayerStartPos, 0.5f);
            mAnim.SetBool(AnimHash.Enemy_Damage_Boss, false);
            mBoltCount = 0;
        }
        else
        {
            mDamageCooltimeCount++;
        }
    }

    public IEnumerator Boss1AttackCooltime_1()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);
        if (isAttacked == false)
        {
            isAttacked = true;
            mAnim.SetBool(AnimHash.Enemy_Damage_Boss, false);
            RemoveObject();
            yield return delay;
            Boss1Attack();
        }
    }

    public void BossDeath()
    {
        mObj[1].SetActive(false);
        mObj[2].SetActive(true);
        mObj[3].SetActive(false);
    }

    public void BossHit()
    {
        mAnim.SetBool(AnimHash.Enemy_Damage_Boss, false);
        SoundController.Instance.SESound(22);
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        RemoveObject();
        Player.Instance.CutSceneKnockBack(PlayerStartPos, 0.5f);
        GetComponent<Boss1Controller>().isAttacked = false;
        mAnim.SetBool(AnimHash.Enemy_Attack, false);
        isDamage = false;
    }

    public void Boss1Damaged()
    {
        StopCoroutine(StartFallingBlock());
        SoundController.Instance.SESound(22);
        isDamage = true;
        isNoDamage = false;
        mAnim.SetBool(AnimHash.Enemy_Damage_Boss, true);
        mBoltCount = 0;
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
                    SoundController.Instance.SESound(23);
                    Destroy(other.gameObject);
                    Boss1Damaged();
                }
                else
                {
                    SoundController.Instance.SESound(23);
                    mBoltCount++;
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollide = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollide = false;
        }
    }

}
