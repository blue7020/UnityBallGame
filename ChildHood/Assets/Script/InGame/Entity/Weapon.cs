using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;
    [SerializeField]
    private GameObject WeaponImage;

    private Animator mAnim;
    private SpriteRenderer mRenderer;
    [SerializeField]
    private eDirection dir;

    [SerializeField]
    private AttackArea mAttackArea;
    //TODO 해당 캐릭터 선택 시 캐릭터의 ID와 같은 ID의 무기 스프라이트 불러오기
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
        mAnim = GetComponent<Animator>();
        mRenderer = WeaponImage.GetComponent<SpriteRenderer>();
        dir = eDirection.Left;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //TODO 현재 공격 패드 방향 따라가기 , 공격 버튼 이동 패드같이 수정
        //if (Player.Instance.hori > 0) //우
        //{

        //    dir = eDirection.Right;
        //    WeaponImage.transform.rotation = Quaternion.Euler(0, 180, 135);
        //    mRenderer.sortingOrder = 10;

        //}
        //else if (Player.Instance.hori < 0)//좌
        //{
        //    dir = eDirection.Left;
            
        //    WeaponImage.transform.rotation = Quaternion.Euler(0, 180, -45);
        //    mRenderer.sortingOrder = 8;
        //}
        //else if (Player.Instance.ver > 0) //상
        //{
        //    dir = eDirection.Up;
        //    WeaponImage.transform.rotation = Quaternion.Euler(0, 180, 45);
        //    mRenderer.sortingOrder = 8;
        //}
        //else if (Player.Instance.ver < 0) //하
        //{
        //    dir = eDirection.Down;
        //    WeaponImage.transform.rotation = Quaternion.Euler(0, 180, -135);
        //    mRenderer.sortingOrder = 10;
        //}

    }

    public void Attack()
    {
        if (mAttackCooltime == false)
        {
            StartCoroutine(AttackCooltime());
        }
    }

    public void Anime()
    {
        if (dir==eDirection.Left) //좌
        {
            transform.position = Player.Instance.transform.position + new Vector3(-0.5f, 0, 0);
        }
        else if (dir == eDirection.Right)//우
        {
            transform.position = Player.Instance.transform.position + new Vector3(0.5f, 0, 0);
        }
        else if (dir == eDirection.Up)//상
        {
            transform.position = Player.Instance.transform.position + new Vector3(0, 0.5f, 0);
        }
        else if (dir == eDirection.Down)//하
        {
            transform.position = Player.Instance.transform.position + new Vector3(0, -0.5f, 0);
        }
    }
    public void AnimeEnd()
    {
        transform.position = Player.Instance.transform.position;
    }

    private IEnumerator AttackCooltime()
    {
        mAnim.SetBool(AnimHash.Attack, true);
        WaitForSeconds Cool =new WaitForSeconds(Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd * Time.fixedDeltaTime);
        mAttackCooltime = true;
        mAttackArea.Attack();
        yield return Cool;
        mAttackCooltime = false;
        mAnim.SetBool(AnimHash.Attack, false);
    }
}
