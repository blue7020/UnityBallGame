using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAttackBlock : MonoBehaviour
{

    public bool isGround,isDamaged,isEnemy;
    public Rigidbody2D mRB2D;
    public Enemy mEnemy;

    private void Awake()
    {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        isGround = false;
        isDamaged = false;
        if (isEnemy)
        {
            mEnemy.BoltAttack();
        }
        yield return delay;
        mRB2D.gravityScale = 1;
    }

    private void FixedUpdate()
    {
        if (!isGround)
        {
            Vector2 Vec = new Vector2(mRB2D.position.x + 1f, mRB2D.position.y - 2.7f);

            //낭떠러지 체크
            Debug.DrawRay(Vec, Vector3.down, new Color(0, 1, 0));//한칸 아래 쪽으로 ray를 쏨
            //레이를 쏴서 맞은 레이어를 탐지 
            RaycastHit2D raycast = Physics2D.Raycast(Vec, Vector3.down, 1, LayerMask.GetMask("Ground"));
            if (raycast.collider != null)
            {
                mRB2D.mass=8;
                isGround = true;
                isDamaged = true;
                if (isEnemy)
                {
                    mEnemy.Damage(mEnemy.mMaxHP);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& !isDamaged)
        {
            isDamaged = true;
            other.gameObject.GetComponent<Player>().Damage(1);
        }
    }
}
