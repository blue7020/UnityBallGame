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
            if (mEnemy.mCurrentHP > 0)
            {
                if (Setting == false)
                {
                    mEnemy.mTarget = other.GetComponent<Player>();
                    mEnemy.mState = eMonsterState.Traking;
                    Setting = true;
                }
                else
                {
                    if (mEnemy.mCoroutine == null)
                    {
                        mEnemy.mCoroutine = StartCoroutine(mEnemy.SkillCast());
                    }
                    StartCoroutine(mEnemy.MoveToPlayer());
                }
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (mEnemy.mCurrentHP > 0)
            {
                mEnemy.mTarget = null;
                mEnemy.mState = eMonsterState.Idle;
                mEnemy.mRB2D.velocity = Vector3.zero;
                StopCoroutine(mEnemy.MoveToPlayer());
                Setting = false;
            }

        }


    }
}
