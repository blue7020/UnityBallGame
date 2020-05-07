using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    private Animator mAnim;
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private float mHealAmount;

    private bool mCooltime = false;

    private void Start()
    {
        mAnim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (mCooltime!=true)
            {
                if ((mPlayer.mCurrentHP + mHealAmount) >= mPlayer.mMaxHP)
                {
                    mPlayer.mCurrentHP = mPlayer.mMaxHP;
                }
                else
                {
                    mPlayer.mCurrentHP += mHealAmount;
                }
                mAnim.SetBool(AnimHash.Empty, true);
                StartCoroutine("Cooltime");
                mCooltime = true;
            }
            else
            {
                Debug.Log("Cooltime");
            }
            

        }
    }

    IEnumerator Cooltime()
    {
        yield return new WaitForSeconds(5f);
        mAnim.SetBool(AnimHash.Empty, false);
        mCooltime = false;
    }
}
