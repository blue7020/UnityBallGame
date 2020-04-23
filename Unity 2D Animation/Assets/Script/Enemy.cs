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
    private int mDelayCount;

    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        //mAnim.SetBool(AnimHash.Attack, true);
    }

    private void OnEnable()
    {
        mAnim.SetBool(AnimHash.Dead, false);
        mCurrentHp = mMaxHP;
        mState = eEnemyState.Idle;
        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(0.1f);
        while (true)
        {
            switch (mState)
            {
                case eEnemyState.Idle:
                    if(mDelayCount>=20)
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
                    if (mDelayCount>=10)
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
