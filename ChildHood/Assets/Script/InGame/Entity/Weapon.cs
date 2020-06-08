using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;
    //TODO 해당 캐릭터 선택 시 캐릭터의 ID와 같은 ID의 무기 스프라이트 불러오기
    private SpriteRenderer mRenderer;

    [SerializeField]
    private AttackArea mAttackArea;
    private int mID=0;
    private bool mAttackCooltime = false;

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
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //TODO 현재 공격 패드 방향 따라가기 , 공격 버튼 이동 패드같이 수정
        if (Player.Instance.hori > 0) //좌
        {
                mRenderer.sortingOrder = 10;
                transform.rotation = Quaternion.Euler(0, 180, 45);
            
        }
        else if (Player.Instance.hori < 0)//우
        {
                mRenderer.sortingOrder = 8;
                transform.rotation = Quaternion.Euler(0, 180, -135);
        }
        else if (Player.Instance.ver > 0) //상
        {
                mRenderer.sortingOrder = 8;
                transform.rotation = Quaternion.Euler(0, 180, -45);
            
        }
        else if (Player.Instance.ver < 0) //하
        {
                mRenderer.sortingOrder = 10;
                transform.rotation = Quaternion.Euler(0, 180, 135);
            
        }

    }

    public void Attack()
    {
        if (mAttackCooltime == false)
        {
            StartCoroutine(AttackCooltime());
        }
    }

    private IEnumerator AttackCooltime()
    {
        WaitForSeconds Cool =new WaitForSeconds(Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd * Time.fixedDeltaTime);
        mAttackCooltime = true;
        mAttackArea.Attack();
        yield return Cool;
        mAttackCooltime = false;
    }
}
