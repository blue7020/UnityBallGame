using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public int mTypeCode,mBossCode;
    public eEnemyType mType;
    public eEnemyState mEnemyState;
    public int mStateDelayTime;

    public float mMaxHP, mCurrentHP,mAtk,DelayTime,MoveDelayTime;
    public bool isMoving,isNoDamage,isDeath,isCollide;
    public int mNextMove;

    public Transform mHead,mBoltStarter,mSpawnPos;
    public Animator mAnim;
    public Rigidbody2D mRB2D;
    public SpriteRenderer mRenderer;

    public Delegates.VoidCallback mFuntion;

    private void Awake()
    {
        mCurrentHP = mMaxHP;
        MoveDelayTime = 20;
        if (DelayTime==0)
        {
            DelayTime = 25;
        }
        else
        {
            DelayTime = 25+ (DelayTime*25);
        }
        mSpawnPos = transform;
        if (mTypeCode == 0)
        {
            StartCoroutine(StateMachine());
            mEnemyState = eEnemyState.Idle;
        }
        else if (mTypeCode == 1)
        {
            StartCoroutine(StateMachine());
            mEnemyState = eEnemyState.Attack;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving&& mTypeCode==0)
        {
            //이동
            mRB2D.velocity = new Vector2(mNextMove, mRB2D.velocity.y);


            Vector2 frontVec = new Vector2(mRB2D.position.x + mNextMove * 0.7f, mRB2D.position.y - 1f);

            //낭떠러지 체크
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));//한칸 앞 부분아래 쪽으로 ray를 쏨
            //레이를 쏴서 맞은 레이어를 탐지 
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
            if (raycast.collider == null)
            {
                Turn();
            }
        }
    }
    
    public IEnumerator StateMachine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            switch (mEnemyState)
            {
                case eEnemyState.Idle://사실상 Move
                    if (mStateDelayTime>= MoveDelayTime)
                    {
                        mStateDelayTime = 0;
                        mNextMove = Random.Range(-1, 2);

                        mAnim.SetInteger("WalkSpeed", mNextMove);
                        if (mNextMove != 0)
                        {
                            mRenderer.flipX = mNextMove == 1;//nextmove 가 1이면 방향을 반대로 변경  
                        }
                        float time = Random.Range(20, 50);
                        MoveDelayTime = time;
                    }
                    else
                    {
                        mStateDelayTime++;
                    }
                    break;
                case eEnemyState.Attack:
                    if (mStateDelayTime >= DelayTime)
                    {
                        mStateDelayTime = 0;
                        if (mTypeCode==1)
                        {
                            BoltAttack();
                        }
                    }
                    else
                    {
                        mStateDelayTime++;
                    }
                    break;
                default:
                    break;
            }
            yield return delay;
        }
    }


    public void Turn()
    {
        mNextMove =mNextMove * (-1);//직접 방향을 바꾸어 주었으니 Think는 잠시 멈추어야함
        mRenderer.flipX = mNextMove == 1;
        mStateDelayTime = 0;
    }

    public void BoltAttack()
    {
        mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mAnim.SetBool(AnimHash.Enemy_Attack, true);
    }
    public void AttackEnd()
    {
        EnemyBolt bolt = EnemyBoltPool.Instance.GetFromPool(0);
        bolt.mEnemy = this;
        bolt.transform.position = mBoltStarter.position;
        bolt.mRB2D.AddForce(mBoltStarter.up * bolt.mSpeed, ForceMode2D.Impulse);
        mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }

    public void Damage(float damage)
    {
        if (isNoDamage==false)
        {
            mCurrentHP -= damage;
            if (mCurrentHP <= 0)
            {
                switch (mBossCode)
                {
                    case 0:
                        mAnim.SetBool(AnimHash.Enemy_Death, true);
                        SoundController.Instance.SESound(0);
                        StartCoroutine(Death());
                        break;
                    case 1:
                        StartCoroutine(GetComponent<Boss1Controller>().BossDeath());
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (mBossCode)
                {
                    case 0:
                        SoundController.Instance.SESound(1);
                        StartCoroutine(DamageAnimation());
                        break;
                    case 1:
                        StartCoroutine(DamageAnimation());
                        GetComponent<Boss1Controller>().GotoIdle();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public IEnumerator DamageAnimation()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        isNoDamage = true;
        gameObject.layer = 10;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        gameObject.layer = 0;
        isNoDamage = false;
    }

    public IEnumerator Death(float time =1f)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        mAnim.SetBool(AnimHash.Enemy_Attack, false);
        gameObject.layer = 10;
        mNextMove = 0;
        mRB2D.velocity = Vector2.zero;
        isDeath = true;
        isNoDamage = true;
        yield return delay;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBolt"))
        {
            Damage(other.gameObject.GetComponent<PlayerBolt>().mDamage);
            Destroy(other.gameObject);
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

    public void Revive()
    {
        if (mType == eEnemyType.Normal)
        {
            StopAllCoroutines();
            mAnim.SetBool(AnimHash.Enemy_Attack, false);
            mCurrentHP = mMaxHP;
            gameObject.layer = 0;
            transform.position = mSpawnPos.position;
            isDeath = false;
            isNoDamage = false;
            gameObject.SetActive(true);
            mAnim.SetBool(AnimHash.Enemy_Death, false);
            mCurrentHP = mMaxHP;
            if (mBossCode==0)
            {
                if (mTypeCode == 0)
                {
                    StartCoroutine(StateMachine());
                    mEnemyState = eEnemyState.Idle;
                }
                else if (mTypeCode == 1)
                {
                    StartCoroutine(StateMachine());
                    mEnemyState = eEnemyState.Attack;
                }
            }
        }
        else
        {
            if (mFuntion!=null)
            {
                mFuntion();
            }
        }
    }
}
