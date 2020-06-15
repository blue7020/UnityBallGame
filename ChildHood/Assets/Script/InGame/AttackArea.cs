using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : Timer
{
    public static AttackArea instance;

    private Animator mAnim;
    [SerializeField]
    private bool mAttackEnd;
    private SpriteRenderer mRenderer;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        mRenderer = GetComponent<SpriteRenderer>();
        mAnim = GetComponent<Animator>();
        mAttackEnd=false;
    }

    //TODO 이펙트 풀을 사용하여 플레이어 캐릭터에 맞는 공격 스프라이트로 변경
    public void Attack()
    {
        gameObject.SetActive(true);
        if (mAttackEnd == true)
        {
            mAnim.SetBool(AnimHash.Attack, true);
            if (Player.Instance.ver > 0) //상
            {
                mRenderer.sortingOrder = 0;

            }
            if (Player.Instance.ver < 0) //하
            {
                mRenderer.sortingOrder = 3;
            }
        }  
    }

    public void AttackEnd()
    {
        mAnim.SetBool(AnimHash.Attack, false);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy Target = other.GetComponent<Enemy>();
            if (Target.mCurrentHP > 0&&Target!=null)
            {
                Target.Hit(Player.Instance.mInfoArr[Player.Instance.mID].Atk);
            }


        }
    }
}
