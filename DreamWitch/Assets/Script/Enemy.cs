using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public int mTypeCode;
    public float mMaxHP, mCurrentHP,mAtk;
    public bool isNoDamage,isDeath;
    public int mNextMove;

    public Transform mHead;
    public Animator mAnim;
    public Rigidbody2D mRB2D;
    public SpriteRenderer mRenderer;

    private void Awake()
    {
        mCurrentHP = mMaxHP;
        Invoke("Think", 2f);
    }

    private void FixedUpdate()
    {
        //이동
        mRB2D.velocity = new Vector2(mNextMove, mRB2D.velocity.y);


        Vector2 frontVec = new Vector2(mRB2D.position.x + mNextMove*0.7f, mRB2D.position.y-1f);
        
        //낭떠러지 체크
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));//한칸 앞 부분아래 쪽으로 ray를 쏨
        //레이를 쏴서 맞은 오브젝트를 탐지 
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (raycast.collider == null)
        {
            Turn();
        }
    }
    
    public void Turn()
    {
        mNextMove =mNextMove * (-1);//직접 방향을 바꾸어 주었으니 Think는 잠시 멈추어야함
        mRenderer.flipX = mNextMove == 1;

        CancelInvoke(); //think를 잠시 멈춘 후 재실행
        Invoke("Think", 2f);
    }

    public void Think()
    {
        mNextMove = Random.Range(-1, 2);

        mAnim.SetInteger("WalkSpeed", mNextMove);
        if (mNextMove != 0)
        {
            mRenderer.flipX = mNextMove == 1;//nextmove 가 1이면 방향을 반대로 변경  
        }
        float time = Random.Range(2f, 5f);
        Invoke("Think", time);
    }

    //mTypeCode 0=좌우 이동, 1=접근형, 2=원거리 공격

    public void Damage(float damage)
    {
        if (isNoDamage==false)
        {
            mCurrentHP -= damage;
            if (mCurrentHP < 1)
            {
                StartCoroutine(Death());
            }
            else
            {
                SoundController.Instance.SESound(1);
                StartCoroutine(DamageAnimation());
            }
        }
    }
    public IEnumerator DamageAnimation()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        isNoDamage = true;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        isNoDamage = false;
    }

    public IEnumerator Death()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mAnim.SetBool(AnimHash.Enemy_Death, true);
        SoundController.Instance.SESound(0);
        CancelInvoke();
        mNextMove = 0;
        mRB2D.velocity = Vector2.zero;
        isDeath = true;
        isNoDamage = true;
        gameObject.layer = 10;
        yield return delay;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBolt"))
        {
            Damage(other.gameObject.GetComponent<PlayerBolt>().mDamage);
            Destroy(other.gameObject);
        }
    }
}
