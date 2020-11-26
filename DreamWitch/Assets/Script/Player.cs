using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public LayerMask mGoundLayer;

    const float mGoundCheckRadius = 0.2f;
    public Rigidbody2D mRB2D;
    public Animator mAnim;
    public SpriteRenderer mRenderer;
    public Transform mGroundChecker;

    public float mSpeed;
    public float mJumpForce;
    public float mHangeCounter;
    public float mHangTime =0.2f;

    public float mJumpBufferLength=1f;
    private float mJumpBufferCount;

    public bool isJump=false;
    public bool isGround=false;
    private float Hori;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Moving();
    }

    private void Update()
    {

        //HangTime
        if (isGround)
        {
            mHangeCounter = mHangTime;
        }
        else
        {
            mHangeCounter -= Time.deltaTime;
        }

        //JumpBuffer
        if (Input.GetButtonDown("Jump"))
        {
            mJumpBufferCount = mJumpBufferLength;
        }
        else
        {
            mJumpBufferCount -= Time.deltaTime;
        }

        //Jump
        if (mJumpBufferCount>=0&& mHangeCounter>0f)
        {
            if (isGround)
            {
                Jump();
            }
        }
        else if (Input.GetButtonUp("Jump")&& mRB2D.velocity.y<0)
        {
            isJump = false;
        }
        mAnim.SetFloat("yVelocity", mRB2D.velocity.y);
    }

    private void Jump()
    {
        Debug.Log(mRB2D.velocity.y +" / "+Vector2.up);
        if (mRB2D.velocity.y<=0)
        {
            mJumpBufferCount = 0;
            isJump = true;
            mAnim.SetBool(AnimHash.Jump, true);
            mRB2D.AddForce(Vector2.up*mJumpForce);
        }
    }

    private void Moving()
    {
        #region Move
        Hori = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(Hori, 0);
        float spd;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isJump == false)
            {
                spd = mSpeed * 1.3f;
            }
            else
            {
                spd = mSpeed;
            }
        }
        else
        {
            spd = mSpeed;
        }
        mRB2D.velocity = dir.normalized * spd;
        mAnim.SetFloat("xVelocity", mRB2D.velocity.x);
        if (Hori > 0)//좌
        {
            mRenderer.flipX = false;
        }
        if (Hori < 0)//우
        {
            mRenderer.flipX = true;
        }
        #endregion
    }

    public void GroundCheck()
    {
        isGround = false;
        Collider2D[] Coll2D = Physics2D.OverlapCircleAll(mGroundChecker.position, mGoundCheckRadius,mGoundLayer);
        if (Coll2D.Length>0)
        {
            isGround = true;
        }
        mAnim.SetBool(AnimHash.Jump, !isGround);
    }
}
