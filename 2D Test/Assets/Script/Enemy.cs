using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private Animator mAnim;
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private Transform mHPBarPos;
    private GaugeBar mHPBar;

    private bool HPBarOn;

    [Header("Status")]
    [SerializeField]
    public float mAtk;
    [SerializeField]
    public float mSpeed;
    [SerializeField]
    public float mMaxHP;
    public float mCurrentHP;
    [SerializeField]
    private float mReward;

    private void FixedUpdate()
    {
        if (HPBarOn == true)
        {
            mHPBar.transform.position = mHPBarPos.position;
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
        }
    }

    private void Awake()
    {
        mCurrentHP = mMaxHP;
        mAnim = GetComponent<Animator>();
    }

    public void Hit(float value)
    {
        mCurrentHP -= value;
        if (mHPBar == null)
        {
            mHPBar = GaugeBarPool.Instance.GetFromPool();
        }
        if (mCurrentHP <= 0)
        {

            mHPBar.gameObject.SetActive(false);
            gameObject.SetActive(false);
            mHPBar = null;
            HPBarOn = false;

        }
        else
        {
            mHPBar.gameObject.SetActive(true);
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
            mHPBar.transform.position = mHPBarPos.position;
            HPBarOn = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            mPlayer.Hit(mAtk);
        }
    }
}
