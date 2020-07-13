using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance;

    public Sprite mWeaponImage;

    public eWeaponType eType;

    public WeaponStat mStats;

    private Animator mAnim;
    public SpriteRenderer mRenderer;

    [SerializeField]
    private AttackArea mAttackArea;
    public int mID;
    private bool mAttackCooltime;
    public bool Attackon;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mStats = WeaponController.Instance.mInfoArr[mID];
        mAnim = GetComponent<Animator>();
        mAttackCooltime = false;
        Attackon = false;
    }

    private void FixedUpdate()
    {
        if (Player.Instance.hori > 0) //우
        {
            mRenderer.sortingOrder = 10;

        }
        else if (Player.Instance.hori < 0)//좌
        {
            mRenderer.sortingOrder = 8;
        }
        else if (Player.Instance.ver > 0) //상
        {
            mRenderer.sortingOrder = 8;
        }
        else if (Player.Instance.ver < 0) //하
        {
            mRenderer.sortingOrder = 10;
        }

    }

    public void MeleeAttack()
    {
        if (mAttackCooltime == false)
        {
            StartCoroutine(MeleeCool());
        }
    }
    private IEnumerator MeleeCool()
    {
        WaitForSeconds Cool =new WaitForSeconds(Player.Instance.mStats.AtkSpd);
        mAttackCooltime = true;
        mAttackArea.Melee();
        yield return Cool;
        mAttackCooltime = false;
    }

    public void RangeAttack()
    {
        if (mAttackCooltime == false)
        {
            StartCoroutine(RangeCool());
        }
    }
    private IEnumerator RangeCool()
    {
        WaitForSeconds Cool = new WaitForSeconds(Player.Instance.mStats.AtkSpd);
        mAttackCooltime = true;
        mAttackArea.Range();
        yield return Cool;
        mAttackCooltime = false;
    }

}
