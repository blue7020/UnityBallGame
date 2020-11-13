using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Rigidbody2D mRB2D;
    public Animator mAnim;
    public SpriteRenderer mRenderer;

    public float mSpeed;
    public float mJumpForce;
    public bool isJump,isFalling;
    private float Hori;
    public int mGravityScale;
    public int jump;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            isJump = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Moving();
        if (jump < 41)
        {
            if (isJump == false && Input.GetKey(KeyCode.Space) && isFalling == false)
            {
                isJump = true;
                mAnim.SetBool(AnimHash.Jump, true);
                Invoke("JumpCheck", 0.05f);
                mRB2D.AddForce(Vector3.up * mJumpForce);
                mRB2D.gravityScale += mRB2D.velocity.y;
            }
        }
        else
        {
            JumpEnd();
        }
    }

    private void JumpCheck()
    {
        jump++;
    }

    private void Moving()
    {
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
        if (Hori > 0)//좌
        {
            mAnim.SetBool(AnimHash.Walk, true);
            mRenderer.flipX = false;
        }
        if (Hori < 0)//우
        {
            mAnim.SetBool(AnimHash.Walk, true);
            mRenderer.flipX = true;
        }
        if (mRB2D.velocity == Vector2.zero)
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }
    }

    public void JumpEnd()
    {
        isFalling = true;
        mRB2D.gravityScale = mGravityScale;
        mAnim.SetBool(AnimHash.Jump, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            mAnim.SetBool(AnimHash.Jump, false);
            isFalling = false;
            jump = 0;
        }
    }
}
