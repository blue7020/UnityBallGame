using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingRange : MonoBehaviour
{
    public Enemy mEnemy;
    public bool Setting;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Setting == false)
            {
                mEnemy.mTarget = other.GetComponent<Player>();
                mEnemy.mState = eMonsterState.Traking;
                mEnemy.mDelayCount = 0;
                Setting = true;
            }

            if (mEnemy.mCoroutine == null)
            {
                mEnemy.mCoroutine = StartCoroutine(mEnemy.SkillCast());
            }
            if (mEnemy.mCurrentHP > 0)
            {
                StartCoroutine(mEnemy.MoveToPlayer());
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mEnemy.mState = eMonsterState.Idle;
            mEnemy.mRB2D.velocity = Vector2.zero;
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
            StopCoroutine(mEnemy.MoveToPlayer());
            Setting = false;


        }


    }
}
