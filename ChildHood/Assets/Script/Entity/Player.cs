using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : InformationLoader
{
    //TODO 캐릭터 선택 시 해당 ID에 맞는 캐릭터의 정보와 스프라이트를 출력하게끔

    public static Player Instance;

    public int mID = 0;//나중에 캐릭터 선택 시 해당 아이디를 부여하는 것으로 수정
    private Sprite[] mPlayerSpriteArr;
    public float mMaxHP;
    public float mCurrentHP;

    [SerializeField]
    public PlayerStat[] mInfoArr;

    private Rigidbody2D mRB2D;
    private Animator mAnim;

    public float hori;
    public float ver;

    //public PlayerStat[] GetInfoArr()
    //{
    //    return mInfoArr;
    //}


    void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadJson(out mInfoArr, Path.PLAYER_STAT);
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mMaxHP = mInfoArr[mID].Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
    }

    private void FixedUpdate()
    {

        Moveing();
        
    }

    private void Moveing()
    {
        hori = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(hori, ver);
        dir = dir.normalized * mInfoArr[mID].Spd;
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
        else if (ver > 0 || ver < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }

        mRB2D.velocity = dir;
    }

    public void Hit(float damage)
    {
        mCurrentHP -= damage;
    }

}
