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

    private GameObject mTarget;
    private Vector2 MovePoint;

    private bool PlayerTracking;
    private bool HPBarOn;
    private bool MoveOn;
    public bool MoveEnd;

    [Header("Status")]
    [SerializeField]
    public float mAtk;
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
        MoveOn = true;
    }

    private void Start()
    {
        StartCoroutine("ChangeMove");
    }

    private void FixedUpdate()
    {
        Move();
        if (HPBarOn == true)
        {
            mHPBar.transform.position = mHPBarPos.position;
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
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

    //
    //while (true)
    //{
    //    mAnim.SetBool(AnimHash.Move, true);
    //    Vector2 TargetPos = mPlayer.transform.position;
    //    Vector2 EnemyPos = transform.position;
    //    Vector2 Destination = TargetPos - EnemyPos;
    //    if (MoveEnd == true)
    //    {
    //        mAnim.SetBool(AnimHash.Move, false);
    //        mRB2D.velocity = Destination;
    //    }
    //    yield return new WaitForSeconds(1f);
    //    MoveOn = true;

    //}
    //플레이어 주변을 따라다니는 펫의 이동 코드로 응용 가능!

    IEnumerator ChangeMove()
    {
        MoveOn = false;
        yield return new WaitForSeconds(2f);
        MoveOn = true;
    }

    private void Move()
    {
        if (MoveOn == true)
        {
            Vector2 Destination;
            Vector2 Velocity = Vector2.zero;
            Vector2 EnemyPos = transform.position;
            if (PlayerTracking == true)
            {
                Vector2 TargetPos = mTarget.transform.position;
                Destination = TargetPos - EnemyPos;
            }
            else
            {
                int RandX = Random.Range(-2, 2);
                int RandY = Random.Range(-2, 2);
                MovePoint = new Vector2(RandX, RandY);
                Destination = MovePoint + EnemyPos;
            }
            mAnim.SetBool(AnimHash.Move, true);
            mRB2D.velocity = Destination;
            mAnim.SetBool(AnimHash.Move, false);
            StartCoroutine("ChangeMove");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            mTarget = other.gameObject;
            PlayerTracking = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            mTarget = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            mTarget = other.gameObject;
            PlayerTracking = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            mPlayer.Hit(mAtk);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        transform.position = transform.position;
    }
}
