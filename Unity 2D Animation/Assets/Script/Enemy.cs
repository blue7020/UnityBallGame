using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyState
{
    Idle,
    Walk,
    Attack,
    Die
}
public class Enemy : MonoBehaviour
{
    private Rigidbody2D mRB2D;
    private Animator mAnim;
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private int mAtk;
    [SerializeField]
    private int mMaxHP;
    private int mCurrentHp;
    private eEnemyState mState;
    private int mDelayCount;//대기 시간(프레임)
    private int mDelayTime = 10;//대기 시간을 변수화 시켜서 기획자나 그래픽이 수정하기 쉽게 하는 것이 좋다

    private Player mTarget;

    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        //mAnim.SetBool(AnimHash.Attack, true);
    }

    private void OnEnable()
    {
        //mAnim.SetBool(AnimHash.Dead, false);
        mCurrentHp = mMaxHP;
        mState = eEnemyState.Idle;
        
    }

    public void STartMoving()
    {
        StartCoroutine(StateMachine());
    }

    public void ZombieHit(int damage)
    {
        Debug.Log("ZHit: " + damage);
    }

    public void Attack()
    {
        mTarget.PlayerHit(mAtk);
    }
    public void AttackFinish()
    {
        mAnim.SetBool(AnimHash.Attack, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (mTarget==null)
            {
                mTarget = collision.gameObject.GetComponent<Player>();
            }


            mAnim.SetBool(AnimHash.Walk, false);
            mState = eEnemyState.Attack;
            mDelayCount = 0;
            

            //collision.gameObject.SendMessage("Hit",1,SendMessageOptions.DontRequireReceiver);
            //메서드는 문자열로, 파라미터는 1개까지(SendMessageOptions는 디폴트 파라미터)
            //Hit이 있는 곳에서 다 실행함(=편리하지만 성능이 좋지 않아서 신중히 써야함)
            //Player 오브젝트의 Hit이라는 문자열과 일치하는 메서드를 찾는 것이기 때문에 빨간 줄이 뜨지 않고 인게임에서 오류가 난다.

           //한꺼번에 여러종류가 타깃이면 SendMessage가 효율적이다.(모든 곳에서 불러오니까.)

           //있으면 돌리고 없으면 돌리지 마라- 돈트리콰이어리시버, 아니면 리콰이어리시버
        }
    }

    private IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(0.1f);
        while (true)
        {
            switch (mState)
            {
                case eEnemyState.Idle:
                    if(mDelayCount>= mDelayTime*2)
                    {
                        mState = eEnemyState.Walk;
                        mAnim.SetBool(AnimHash.Walk, true);
                        //속도 주기
                        mRB2D.velocity = transform.right * mSpeed;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Walk:
                    if (mDelayCount>= mDelayTime)
                    {
                        mState = eEnemyState.Idle;
                        mAnim.SetBool(AnimHash.Walk, false);
                        mRB2D.velocity = Vector2.zero;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Attack:
                    if (mDelayCount>= mDelayTime*3)
                    {                        
                        mAnim.SetBool(AnimHash.Attack, true);
                        
                        mDelayCount = 0;
                        
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Die:
                    break;
                default:
                    Debug.LogError("Wrong State: " + mState);
                    break;
            }
            yield return pointOne;
        }
    }
}
