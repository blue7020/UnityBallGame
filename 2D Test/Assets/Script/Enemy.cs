using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D mRB2D;
    private Animator mAnim;
    [SerializeField]
    private Player mPlayer;

    [SerializeField]
    private Transform mHPBarPos;
    private GaugeBar mHPBar;

    private bool HPBarOn;
    [SerializeField]
    private bool AttackOn;

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

    

    private void Awake()
    {
        mCurrentHP = mMaxHP;
        mAnim = GetComponent<Animator>();
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (HPBarOn == true)
        {
            mHPBar.transform.position = mHPBarPos.position;
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
        }
        if (AttackOn == true)
        {
            StartCoroutine("Jump");
        }
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

    IEnumerator Jump()
    {
        AttackOn = false;
        WaitForSeconds AttackCooltime = new WaitForSeconds(5f);
        mAnim.SetBool(AnimHash.Jumping, true);
        while (true)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            if (target!=null)
            {
                Vector2 pos = mPlayer.transform.position;
                Vector2 dir = transform.position;
                Vector2 destination = pos - dir;
                mRB2D.velocity = destination * mSpeed;
            }
            //점프 애니메이션 부분은 예전에 했던 점프 애니메이션 2개로 나눈거 참고
            yield return AttackCooltime;
            mAnim.SetBool(AnimHash.Jumping, false);
            AttackOn = true;
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
