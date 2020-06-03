using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingRange : MonoBehaviour
{
    [SerializeField]
    private Enemy mEnemy;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mEnemy.State = eMonsterState.Traking;
            mEnemy.mDelayCount = 0;

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (mEnemy.mCoroutine == null)
        {
            mEnemy.mCoroutine = StartCoroutine(mEnemy.SkillCast());
        }
        if (mEnemy.mCurrentHP>0)
        {
            StartCoroutine(mEnemy.MoveToPlayer());
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mEnemy.State = eMonsterState.Idle;
            StopCoroutine(mEnemy.MoveToPlayer());
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Move, false);
        }


    }
}
