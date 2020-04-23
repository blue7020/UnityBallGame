using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    //[SerializeField]
    //private float mJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer rand = GetComponent<SpriteRenderer>();
        //rand.sortingOrder = 10;//레이어 10으로 올림
        mAnim = GetComponent<Animator>();
        mRB2D = GetComponent<Rigidbody2D>();
        

        //AttackHash = Animator.StringToHash("IsAttack");
        //mAnim.SetBool(AttackHash,true);
        //mAnim.SetBool("IsAttack",true); //이것보단 위의 방식이 더 효율적이다. 미리 해쉬로 변환한 후 계속 해쉬가 등록된 변수를 불러오기만 하면 되기 때문
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);//해당 스프라이트가 오른쪽을 바라보고 있기 때문에 180도
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.identity;//기본 Rotation 값을 받음. 따라서 0,0,0
            //회전은 4차원임
        }

        if (Input.GetButtonDown("Fire1"))//누르면
        {
            mAnim.SetBool(AnimHash.Attack, true);
        }
        else if (Input.GetButtonUp("Fire1"))//떼면
        {
            mAnim.SetBool(AnimHash.Attack, false);
        }
        //if (Input.GetButtonDown("Jump"))
        //{
        //    mAnim.SetBool("IsJump", true);
        //    mRB2D.velocity = Vector2.up * mJumpForce;
        //}
        //mAnim.SetFloat("JumpVel",mRB2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ground")&&collision.enabled)
        //{
        //    mAnim.SetBool("IsJump", false);
        //}
    }
}
