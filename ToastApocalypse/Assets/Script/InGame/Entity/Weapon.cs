using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool IsSpin;//무기 회전 시 xflip할지 안할지
    public Sprite mWeaponImage;
    public GameObject Aim;
    public bool Equip;

    public Room Currentroom;
    public eWeaponType eType;

    public WeaponStat mStats;
    public int PlusBulletCount;
    public int PlusWideBulletCount;
    public float mBoltXGap;
    public int nowBullet;
    public int MaxBullet;

    public SpriteRenderer mRenderer;
    public Animator mAnim;
    public MaterialObj[] Recipe;
    public int[] RecipeAmount;

    public AttackArea mAttackArea;
    public int mID;
    public bool mAttackCooltime;
    public bool Attackon;
    public bool GetCooltime;
    public bool Animation;
    public int BoltID;

    private void Awake()
    {
        mStats = GameSetting.Instance.mInfoArr[mID];
        mAttackCooltime = false;
        Attackon = false;
        Equip = false;
        MaxBullet = mStats.Bullet;
        nowBullet = MaxBullet;
        GetCooltime = false;
    }

    private void FixedUpdate()
    {
        if (Equip==true)
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
        StartCoroutine(Slash());
        yield return Cool;
        mAttackCooltime = false;
    }

    private IEnumerator Slash()
    {
        WaitForSeconds few = new WaitForSeconds(0.3f);
        mAnim.SetBool(AnimHash.Attack, true);
        mAttackArea.gameObject.SetActive(true);
        mAttackArea.Melee();
        yield return few;
        mAnim.SetBool(AnimHash.Attack, false);
        mAttackArea.gameObject.SetActive(false);
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

    public void FireAttack()
    {
        if (mAttackCooltime == false)
        {
            mAttackArea.Fire();
        }
    }

    public void EquipWeapon()
    {
        if (Equip == false)
        {
            Equip = true;
            if (eType == eWeaponType.Range|| eType == eWeaponType.Fire)
            {
                Aim.gameObject.SetActive(true);
                mAttackArea.gameObject.SetActive(true);
            }
            WeaponController.Instance.mWeaponSkillCount = 0;
            Player.Instance.EquipWeapon(this);
            Currentroom = Player.Instance.CurrentRoom;
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.localPosition = Vector3.zero;
            UIController.Instance.ShowNowBulletText();
            UIController.Instance.ShowWeaponImage();
        }
    }

    public void UnequipWeapon()
    {
        if (Equip == true)
        {
            mRenderer.sortingOrder = 8;
            if (eType == eWeaponType.Range || eType == eWeaponType.Fire)
            {
                Aim.gameObject.SetActive(false);
                mAttackArea.gameObject.SetActive(false);
            }
            gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
            gameObject.transform.position = Player.Instance.transform.position;
            StartCoroutine(DropCool());
            Player.Instance.UnequipWeapon(this);
            Equip = false;
        }
    }

    public void WeaponChange()
    {
        if (Player.Instance.NowPlayerWeapon == null)
        {
            EquipWeapon();
        }
        else
        {
            Weapon drop = Player.Instance.NowPlayerWeapon;
            drop.UnequipWeapon();
            EquipWeapon();
        }
    }

    private IEnumerator DropCool()
    {
        WaitForSeconds cool = new WaitForSeconds(1f);
        GetCooltime = true;
        yield return cool;
        GetCooltime = false;
        StopCoroutine(DropCool());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Equip == false && GetCooltime == false)
            {
                WeaponChange();
            }
        }

    } 
    
}
