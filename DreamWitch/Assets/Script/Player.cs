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
        Moving(isJump);
    }

    private void Update()
    {
        mAnim.SetFloat("yVelocity", mRB2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            mAnim.SetBool(AnimHash.Jump, true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJump = false;
        }
    }

    private void Moving(bool jumpFlag)
    {
        if (isGround&& jumpFlag)
        {
            mRB2D.AddForce(new Vector3(0,1,0)*mJumpForce,ForceMode2D.Impulse);
            jumpFlag = false;
        }
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
