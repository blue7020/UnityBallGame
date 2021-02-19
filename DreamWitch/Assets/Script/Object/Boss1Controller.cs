using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    public int mBoltCount, mDamageCooltimeCount;
    public bool isDamage,isAttack;

    public eEnemyState mState;
    public int mDelayCount;

    public Transform[] BlockSpawnPosArr;
    public GameObject[] BlockArr;
    public List<GameObject> BlockList;
    public List<int> RandomNumList;

    public Vector2 PlayerStartPos;

    public Enemy mEnemy;

    public bool isHint;
    public string text;

    public GameObject[] mObj;//보스 방과 연결된 문과 출구쪽 사다리

    private void Awake()
    {
        mEnemy.mFuntion = (() => { BossReset(); });
        BlockList = new List<GameObject>();
        RandomNumList = new List<int>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        if (TitleController.Instance.mLanguage == 0)
        {
            text = DialogueSystem.Instance.mDialogueTextArr[55].text_kor;
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            text = DialogueSystem.Instance.mDialogueTextArr[55].text_eng;
        }
    }

    public void BossSpawn()
    {
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Spawn, true);
        mEnemy.mCurrentHP = mEnemy.mMaxHP;
        mState = eEnemyState.Spawn;
        StartCoroutine(StateMachine());
    }

    public IEnumerator BossReset()
    {
        WaitForSeconds delay = new WaitForSeconds(2.5f);
        UIController.Instance.mTextBoxImage.gameObject.SetActive(false);
        StopCoroutine(StateMachine());
        StopCoroutine(StartFallingBlock());
        mState = eEnemyState.None;
        mDelayCount = 0;
        mObj[0].SetActive(true);
        mObj[1].SetActive(true);
        mObj[2].SetActive(false);
        mObj[3].SetActive(true);
        gameObject.layer = 0;
        mEnemy.isDeath = false;
        mEnemy.isNoDamage = true;
        RemoveObject();
        yield return delay;
        RemoveObject();
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Death, false);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Damage_Boss, false);
        mState = eEnemyState.Idle;
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
            int ObjRand = Random.Range(0, BlockArr.Length);
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

    public void Boss1Attack()
    {
        if (!isAttack)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            isAttack = true;
            mBoltCount = 0;
            mEnemy.isNoDamage = true;
            StartCoroutine(StartFallingBlock());
        }
    }

    public void Boss1Damaged()
    {
        if (!isDamage)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
            StopCoroutine(StartFallingBlock());
            SoundController.Instance.SESound(22);
            isAttack = false;
            isDamage = true;
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Damage_Boss, true);
            mBoltCount = 0;
            mState = eEnemyState.Damaged;
        }
    }

    public void NodamageFalse()
    {
        mEnemy.isNoDamage = false;
    }

    public IEnumerator BossDeath()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        StopCoroutine(StateMachine());
        mEnemy.isNoDamage = true;
        UIController.Instance.mTextBoxImage.gameObject.SetActive(false);
        RemoveObject();
        SoundController.Instance.SESound(22);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Death, true);
        SoundController.Instance.BGMChange(0);
        GameController.Instance.isBoss = false;
        Darkness.Instance.Show();
        Darkness.Instance.transform.position = new Vector3(575.5f, -144, 0);
        yield return delay;
        SoundController.Instance.SESound(5);
        mObj[1].SetActive(false);
        mObj[2].SetActive(true);
        mObj[3].SetActive(false);
        StartCoroutine(mEnemy.Death(2.5f));
    }

    public void GotoIdle()
    {
        mDamageCooltimeCount = 0;
        mEnemy.isNoDamage = true;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Damage_Boss, false);
        SoundController.Instance.SESound(22);
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        RemoveObject();
        Player.Instance.CutSceneKnockBack(PlayerStartPos, 0.5f);
        isDamage = false;
        mBoltCount = 0;
        mState = eEnemyState.Idle;
    }

    public IEnumerator StateMachine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            if (GameController.Instance.Pause==false)
            {
                switch (mState)
                {
                    case eEnemyState.Spawn:
                        mState = eEnemyState.Idle;
                        break;
                    case eEnemyState.Idle:
                        isAttack = false;
                        isDamage = false;
                        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                        mEnemy.mAnim.SetBool(AnimHash.Enemy_Damage_Boss, false);
                        if (mDelayCount >= 20 && mDelayCount < 60)
                        {
                            mEnemy.isNoDamage = true;
                        }
                        else if (mDelayCount >=60)
                        {
                            mDelayCount = 0;
                            mState = eEnemyState.Attack;
                        }
                        mDelayCount++;
                        break;
                    case eEnemyState.Damaged:
                        if (mDelayCount >= 20)
                        {
                            mDelayCount = 0;
                            if (mDamageCooltimeCount >= 6)
                            {
                                ShowHint();
                                GotoIdle();
                            }
                            else
                            {
                                mDamageCooltimeCount++;
                            }
                        }
                        else
                        {
                            mDelayCount++;
                        }
                        break;
                    case eEnemyState.Attack:
                        if (mDelayCount >= 5&&mDelayCount<150)
                        {
                            Boss1Attack();
                        }
                        else if (mDelayCount >= 150)
                        {
                            isDamage = false;
                            isAttack = false;
                            RemoveObject();
                            mDelayCount = 0;
                            ShowHint();
                            mState = eEnemyState.Idle;
                        }
                        mDelayCount++;
                        break;
                    default:
                        break;
                }
            }
            yield return delay;
        }
    }

    public void ShowHint()
    {
        if (mEnemy.mCurrentHP==mEnemy.mMaxHP)
        {
            if (TitleController.Instance.mLanguage == 0)
            {
                text = DialogueSystem.Instance.mDialogueTextArr[55].text_kor;
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                text = DialogueSystem.Instance.mDialogueTextArr[55].text_eng;
            }
            if (!isHint)
            {
                isHint = true;
                UIController.Instance.ShowDialogue(text, false);
            }
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

}
