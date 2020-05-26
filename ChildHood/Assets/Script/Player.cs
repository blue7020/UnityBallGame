using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : InformationLoader
{

    public int mID = 0;//나중에 캐릭터 선택 시 해당 아이디를 부여하는 것으로 수정
    [SerializeField]
    private PlayerStat[] mInfoArr;
    private Sprite[] mPlayerSpriteArr;

    [SerializeField]
    private PlayerStatText[] mTextInfoArr;

    private Rigidbody2D mRB2D;

    //public PlayerStat[] GetInfoArr()
    //{
    //    return mInfoArr;
    //}
    //public PlayerStatText[] GetTextInfoArr()
    //{
    //    return mTextInfoArr;
    //}


    void Awake()
    {
        LoadJson(out mInfoArr, Path.PLAYER_STAT);
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Moveing();
    }

    private void Moveing()
    {
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(hori, ver);
        dir = dir.normalized * mInfoArr[mID].Spd;
        if (hori > 0)
        {
            //mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.identity;
        }
        else if (hori < 0)
        {
            //mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (ver > 0)
        {
            //mAnim.SetBool(AnimHash.Walk, true);
        }
        else if (ver < 0)
        {
            //mAnim.SetBool(AnimHash.Walk, true);
        }
        else
        {
            //mAnim.SetBool(AnimHash.Walk, false);
        }

        mRB2D.velocity = dir;

    }
}
