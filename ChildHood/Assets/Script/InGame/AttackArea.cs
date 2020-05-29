using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : Timer
{
    private Animator mAnim;
    //private Enemy mEnemy;
    [SerializeField]
    private bool mAttackEnd;
    private SpriteRenderer mRenderer;

    private void Awake()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        mAnim = GetComponent<Animator>();
        mAttackEnd=false;
    }

    //TODO 이펙트 풀을 사용하여 플레이어 캐릭터에 맞는 공격 스프라이트로 변경

    private void FixedUpdate()
    {
        
        if (mAttackEnd == true)
        {
            if (Player.Instance.ver > 0) //상
            {
                mRenderer.sortingOrder = 0;

            }
            if (Player.Instance.ver < 0) //하
            {
                mRenderer.sortingOrder = 3;

            }
            mAnim.SetBool(AnimHash.Attack, false);
            gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        gameObject.SetActive(true);
        mAnim.SetBool(AnimHash.Attack, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Enemy.Instance.mCurrentHP>0)
            {
                other.gameObject.GetComponent<Enemy>().Hit(Player.Instance.mInfoArr[Player.Instance.mID].Atk);
            }
            

        }
    }
}
