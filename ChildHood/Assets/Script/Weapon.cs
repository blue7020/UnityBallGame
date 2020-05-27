using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer mRenderer;

    [SerializeField]
    private AttackArea mAttackArea;
    private bool mAttackCooltime = false;

    private void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //블렌드 트리 애니메이션을 사용해 비슷하게 만들 수 있음
        //TODO 현재 공격 패드 방향 따라가기 , 공격 버튼 이동 패드같이 수정
        if (Player.Instance.hori > 0) //좌
        {
                mRenderer.sortingOrder = 2;
                transform.rotation = Quaternion.Euler(0, 180, 45);
            
        }
        else if (Player.Instance.hori < 0)//우
        {
                mRenderer.sortingOrder = 0;
                transform.rotation = Quaternion.Euler(0, 180, -135);
        }
        else if (Player.Instance.ver > 0) //상
        {
                mRenderer.sortingOrder = 0;
                transform.rotation = Quaternion.Euler(0, 180, -45);
            
        }
        else if (Player.Instance.ver < 0) //하
        {
                mRenderer.sortingOrder = 2;
                transform.rotation = Quaternion.Euler(0, 180, 135);
            
        }
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mAttackCooltime == false)
            {
                StartCoroutine(Attack());
            }

        }
        
    }

    IEnumerator Attack()
    {
        mAttackCooltime = true;
        mAttackArea.Attack();
        yield return new WaitForSeconds(Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd);
        mAttackCooltime = false;
    }
}
