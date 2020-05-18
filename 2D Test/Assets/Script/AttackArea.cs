using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Animator mAnim;
    [SerializeField]
    private Player mPlayer;
    private Enemy mEnemy;
    [SerializeField]
    private bool mAttackEnd;
    private SpriteRenderer mRenderer;

    private void Awake()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        mAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (mAttackEnd == true)
        {
            //애니메이션끝난 후 mAttackEnd는 true
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
            other.gameObject.GetComponent<Enemy>().Hit(mPlayer.mAtk);
            
        }
    }
}
