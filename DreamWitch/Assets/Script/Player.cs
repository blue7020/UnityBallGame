using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public LayerMask mGoundLayer;
    const float GROUND_CHECK_RADIUS = 0.01f;

    public Rigidbody2D mRB2D;
    public Animator mAnim;
    public SpriteRenderer mRenderer;
    public Transform mGroundChecker, mHoldZone, mBulletStart,Map;
    public Vector2 CheckPointPos;
    public HoldingItem mHold, mNowHold;
    public PlayerBolt mBolt;

    public int mMaxHP, mCurrentHP;
    public float mSpeed;
    public float mJumpForce;

    public bool isJump, isGround, isNoDamage,isCooltime,isHold;
    private float Hori;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mCurrentHP = mMaxHP;
            CheckPointPos = GameController.Instance.mStartPoint.transform.position + new Vector3(0, 2f, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Hori = Input.GetAxis("Horizontal");
        GroundCheck();
        Moving(Hori, isJump);
    }

    private void Update()
    {

        mAnim.SetFloat("yVelocity", mRB2D.velocity.y);
        if (Input.GetButton("Jump"))
        {
            isJump = true;
            mAnim.SetBool(AnimHash.Jump, true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
        }

        if (Input.GetKey(KeyCode.S)&&!isCooltime)//공격
        {
            StartCoroutine(AttackCooltime());
        }
    }

    public IEnumerator AttackCooltime()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        PlayerBolt bolt = Instantiate(mBolt);
        bolt.transform.position = transform.position;
        bolt.transform.rotation = mRenderer.gameObject.transform.rotation;
        bolt.mRB2D.AddForce(mBulletStart.right * mSpeed, ForceMode2D.Impulse);
        isCooltime = true;
        yield return delay;
        isCooltime = false;
    }

    private void Moving(float dir, bool jumpFlag)
    {
        if (isGround && jumpFlag)
        {
            jumpFlag = false;
            mRB2D.AddForce(new Vector2(0f, mJumpForce));
        }

        #region Move
        Vector3 move = new Vector3(dir, 0f, 0f);
        transform.position += move * Time.deltaTime * mSpeed;
        mAnim.SetFloat("xVelocity", dir);
        if (dir > 0)//좌
        {
            mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }
        if (dir < 0)//우
        {
            mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        }
        #endregion
    }

    public void GroundCheck()
    {
        isGround = false;
        Collider2D[] Coll2D = Physics2D.OverlapCircleAll(mGroundChecker.position, GROUND_CHECK_RADIUS, mGoundLayer);
        if (Coll2D.Length > 0)
        {
            isGround = true;
            mRB2D.velocity = Vector2.zero;
        }
        mAnim.SetBool(AnimHash.Jump, !isGround);
    }

    public void GetItem(HoldingItem obj)//아이템 획득
    {
        //인벤토리 구현
        //mNowHold = obj;
        //mNowHold.transform.SetParent(mHoldZone);
        //mNowHold.transform.position = Vector3.zero;
        //isHold = true;
    }

    //TODO 아이템 홀드

    public void Damage(int damage)
    {
        if (!isNoDamage)
        {
            mCurrentHP -= damage;
            GameController.Instance.Damege();
            StartCoroutine(DamageAnimation());
        }
    }
    public IEnumerator DamageAnimation()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        isNoDamage = true;
        mRenderer.color = new Vector4(1,1,1,0.2f);
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
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        isNoDamage = false;
    }

    public void Death()
    {
        mCurrentHP -= 1;
        GameController.Instance.Damege();
        StartCoroutine(DamageAnimation());
        if (mCurrentHP > 0)
        {
            transform.position = CheckPointPos;
        }
        else
        {
            GameController.Instance.GameOver();
        }
    }

}
