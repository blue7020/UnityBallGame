using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Sprite mWeaponImage;
    public GameObject Aim;
    public bool Equip;

    public Room Currentroom;
    public eWeaponType eType;

    public WeaponStat mStats;
    public int nowBullet;
    public int MaxBullet;

    public SpriteRenderer mRenderer;
    public Animator mAnim;

    public AttackArea mAttackArea;
    public int mID;
    public bool mAttackCooltime;
    public bool Attackon;
    public bool GetCooltime;

    private void Awake()
    {
        mStats = WeaponController.Instance.mInfoArr[mID];
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

    public void EquipWeapon()
    {
        if (Equip == false)
        {
            Equip = true;
            if (eType == eWeaponType.Range)
            {
                Aim.gameObject.SetActive(true);
                mAttackArea.gameObject.SetActive(true);
            }
            Player.Instance.EquipWeapon(this);
            Currentroom = Player.Instance.CurrentRoom;
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.localPosition = Vector3.zero;
            WeaponController.Instance.mWeapons.Remove(this);
            UIController.Instance.ShowNowBulletText();
            UIController.Instance.ShowWeaponImage();
        }
    }

    public void UnequipWeapon()
    {
        if (Equip == true)
        {
            mRenderer.sortingOrder = 8;
            if (eType == eWeaponType.Range)
            {
                Aim.gameObject.SetActive(false);
                mAttackArea.gameObject.SetActive(false);
            }
            gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
            gameObject.transform.position = Player.Instance.transform.position;
            StartCoroutine(DropCool());
            WeaponController.Instance.mWeapons.Add(this);
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

    private void OnTriggerStay2D(Collider2D other)
    {

        if (Equip==false)
        {
            if (other.gameObject.CompareTag("Walls"))
            {
                if (Currentroom != null)
                {
                    switch (other.GetComponent<WallDir>().Type)
                    {
                        case eWallType.Top:
                            int rand = UnityEngine.Random.Range(0, 5);
                            switch (rand)
                            {
                                case 0:
                                    gameObject.transform.position += new Vector3(0, -1, 0);
                                    break;
                                case 1:
                                    gameObject.transform.position += new Vector3(1, 0, 0);
                                    break;
                                case 2:
                                    gameObject.transform.position += new Vector3(-1, 0, 0);
                                    break;
                                case 3:
                                    gameObject.transform.position += new Vector3(1, -1, 0);
                                    break;
                                case 4:
                                    gameObject.transform.position += new Vector3(-1, -1, 0);
                                    break;
                            }
                            break;
                        case eWallType.Bot:
                            int rand2 = UnityEngine.Random.Range(0, 5);
                            switch (rand2)
                            {
                                case 0:
                                    gameObject.transform.position += new Vector3(0, 1, 0);
                                    break;
                                case 1:
                                    gameObject.transform.position += new Vector3(1, 0, 0);
                                    break;
                                case 2:
                                    gameObject.transform.position += new Vector3(-1, 0, 0);
                                    break;
                                case 3:
                                    gameObject.transform.position += new Vector3(1, 1, 0);
                                    break;
                                case 4:
                                    gameObject.transform.position += new Vector3(-1, 1, 0);
                                    break;
                            }
                            break;
                        case eWallType.Right:
                            int rand3 = UnityEngine.Random.Range(0, 5);
                            switch (rand3)
                            {
                                case 0:
                                    gameObject.transform.position += new Vector3(-1, 0, 0);
                                    break;
                                case 1:
                                    gameObject.transform.position += new Vector3(0, -1, 0);
                                    break;
                                case 2:
                                    gameObject.transform.position += new Vector3(0, 1, 0);
                                    break;
                                case 3:
                                    gameObject.transform.position += new Vector3(-1, 1, 0);
                                    break;
                                case 4:
                                    gameObject.transform.position += new Vector3(-1, -1, 0);
                                    break;
                            }
                            break;
                        case eWallType.Left:
                            int rand4 = UnityEngine.Random.Range(0, 5);
                            switch (rand4)
                            {
                                case 0:
                                    gameObject.transform.position += new Vector3(1, 0, 0);
                                    break;
                                case 1:
                                    gameObject.transform.position += new Vector3(0, -1, 0);
                                    break;
                                case 2:
                                    gameObject.transform.position += new Vector3(0, 1, 0);
                                    break;
                                case 3:
                                    gameObject.transform.position += new Vector3(1, 1, 0);
                                    break;
                                case 4:
                                    gameObject.transform.position += new Vector3(1, -1, 0);
                                    break;
                            }
                            break;
                        default:
                            Debug.LogError("Wrong Wall Type");
                            break;
                    }
                }
            }
        }
        
    }


    
}
