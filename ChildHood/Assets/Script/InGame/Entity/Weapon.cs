using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Sprite mWeaponImage;
    public bool Equip;

    public eWeaponType eType;

    public WeaponStat mStats;
    public int nowBullet;
    public int MaxBullet;

    private Animator mAnim;
    public SpriteRenderer mRenderer;

    public AttackArea mAttackArea;
    public int mID;
    private bool mAttackCooltime;
    public bool Attackon;

    private void Awake()
    {
        mStats = WeaponController.Instance.mInfoArr[mID];
        mAnim = GetComponent<Animator>();
        mAttackCooltime = false;
        Attackon = false;
        Equip = false;
        MaxBullet = mStats.Bullet;
        nowBullet = MaxBullet;
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
        if (mAttackCooltime == false && Player.Instance.mStats.AtkSpd > 0f)
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
        if (mAttackCooltime == false && Player.Instance.mStats.AtkSpd > 0f)
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


    public void EquipWeapon()
    {
        if (Equip == false)
        {
            Player.Instance.EquipWeapon(this);
        }
    }

    public void UnequipWeapon()
    {
        if (Equip == true)
        {
            Player.Instance.UnequipWeapon(this);
        }
    }
}
