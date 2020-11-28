using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public LayerMask mGoundLayer;

    const float mGoundCheckRadius = 0.05f;
    public Rigidbody2D mRB2D;
    public Animator mAnim;
    public SpriteRenderer mRenderer;
    public Transform mGroundChecker, mHoldZone, Map;
    public HoldingItem mHold,mNowHold;

    public float mSpeed;
    public float mJumpForce;

    public bool isJump,isGround,isHold;
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
        Hori = Input.GetAxis("Horizontal");
        GroundCheck();
        Moving(Hori,isJump);
    }

    private void Update()
    {

        mAnim.SetFloat("yVelocity", mRB2D.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
            mAnim.SetBool(AnimHash.Jump, true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (mHold != null)
            {
                GetItem(mHold);
            }
        }
    }

    private void Moving(float dir,bool jumpFlag)
    {
        if (isGround&& jumpFlag)
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
            //mRenderer.flipX = false;
            mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }
        if (dir < 0)//우
        {
            mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
            //mRenderer.flipX = true;
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

}
