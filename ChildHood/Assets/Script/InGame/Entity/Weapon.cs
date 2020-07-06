using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;
    [SerializeField]
    public GameObject WeaponImage;

    private Animator mAnim;
    public SpriteRenderer mRenderer;

    [SerializeField]
    private AttackArea mAttackArea;
    //TODO 해당 캐릭터 선택 시 캐릭터의 ID와 같은 ID의 무기 스프라이트 불러오기
    private int mID=0;
    private bool mAttackCooltime;
    public bool Attackon;

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
        mAnim = GetComponent<Animator>();
        mRenderer = WeaponImage.GetComponent<SpriteRenderer>();
        mAttackCooltime = false;
        Attackon = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //TODO 현재 공격 패드 방향 따라가기 , 공격 버튼 이동 패드같이 수정
        if (Player.Instance.hori > 0) //우
        {
            //WeaponImage.transform.rotation = Quaternion.Euler(0, 180, 135);
            mRenderer.sortingOrder = 10;

        }
        else if (Player.Instance.hori < 0)//좌
        {

            //WeaponImage.transform.rotation = Quaternion.Euler(0, 180, -45);
            mRenderer.sortingOrder = 8;
        }
        else if (Player.Instance.ver > 0) //상
        {
            //WeaponImage.transform.rotation = Quaternion.Euler(0, 180, 45);
            mRenderer.sortingOrder = 8;
        }
        else if (Player.Instance.ver < 0) //하
        {
            //WeaponImage.transform.rotation = Quaternion.Euler(0, 180, -135);
            mRenderer.sortingOrder = 10;
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
        mAnim.SetBool(AnimHash.Attack, true);
        WaitForSeconds Cool =new WaitForSeconds(Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd);
        mAttackCooltime = true;
        mAttackArea.Attack();
        yield return Cool;
        mAttackCooltime = false;
        mAnim.SetBool(AnimHash.Attack, false);
    }
}
