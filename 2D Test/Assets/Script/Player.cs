using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    private Vector2 dir;

    public float hori = 0;
    public float ver = 0;

    [Header("Status")]
    [SerializeField]
    public float mAtk;
    [SerializeField]
    public float mSpeed;
    [SerializeField]
    public float mMaxHP;
    [SerializeField]
    public float mAttackSpeed;//초기값은 1.2

    public float mCurrentHP;


    private void Awake()
    {
        mCurrentHP = mMaxHP;
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        PlayerMovement();

    }

    public void Hit(float damage)
    {
        mCurrentHP -= damage;
    }

   


    private void PlayerMovement()
    {
        hori = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        dir = new Vector2(hori, ver);
        dir = dir.normalized * mSpeed;
        if (hori > 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.identity;
        }
        else if (hori < 0)   
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (ver > 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else if (ver < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }

        mRB2D.velocity = dir;

    }
}
