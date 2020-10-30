using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public bool IsSpin;//무기 회전 시 xflip할지 안할지
    public Sprite mWeaponImage;
    public GameObject Aim;
    public bool Equip;

    public Room Currentroom;
    public eWeaponType eType;

    public WeaponStat mStats;
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
    public int ListIndex;
    public bool mAttackCooltime;
    public bool Attackon;
    public bool Animation;
    public int BoltID;
    public int SoundId;

    private bool isTutorial;
    private Button mUIWeaponButton, mTutorialUIWeaponButton;
    private Text mTutoriaAttackPadText;
    private Vector2 mRangeSize;

    private void Awake()
    {
        mStats = SaveDataController.Instance.mWeaponInfoArr[mID];
        mAttackCooltime = false;
        Attackon = false;
        Equip = false;
        MaxBullet = mStats.Bullet;
        nowBullet = MaxBullet;
        mRangeSize = mAttackArea.transform.localScale;
        isTutorial = GameController.Instance.IsTutorial;
        if (isTutorial==true)
        {
            mTutorialUIWeaponButton = TutorialUIController.Instance.mWeaponChangeButton;
            mTutoriaAttackPadText = TutorialUIController.Instance.mAttackPadText;
        }
        else
        {
            mUIWeaponButton = UIController.Instance.mWeaponChangeButton;
        }
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

    public void WeaponRangeSize()
    {
        if (eType == eWeaponType.Melee)
        {
            mAttackArea.transform.localScale = mRangeSize;
            mAttackArea.transform.localScale *= new Vector2(PassiveArtifacts.Instance.AdditionalMeleeRangeSize, PassiveArtifacts.Instance.AdditionalMeleeRangeSize);
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
            WeaponRangeSize();
            WeaponController.Instance.mWeaponSkillCount = 0;
            Player.Instance.EquipWeapon(this);
            Currentroom = Player.Instance.CurrentRoom;
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.localPosition = Vector3.zero;
            if (GameController.Instance.IsTutorial==false)
            {
                UIController.Instance.ShowNowBulletText();
                UIController.Instance.ShowWeaponImage();
            }
            else
            {
                TutorialUIController.Instance.ShowNowBulletText();
                TutorialUIController.Instance.ShowWeaponImage();
            }
            SoundController.Instance.SESound(7);

        }
    }

    public void UnequipWeapon()
    {
        if (Equip == true)
        {
            AttackPad.Instance.WeaponChangeReset();
            mRenderer.sortingOrder = 8;
            if (eType == eWeaponType.Range)
            {
                Aim.gameObject.SetActive(false);
                mAttackArea.gameObject.SetActive(false);
            }
            else if (eType == eWeaponType.Fire)
            {
                Aim.gameObject.SetActive(false);
                mAttackArea.gameObject.SetActive(false);
                SoundController.Instance.mBGSE.Stop();
                mAttackArea.FireStarter.Stop();
            }
            gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
            gameObject.transform.position = Player.Instance.transform.position;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Equip == false)
            {
                if (isTutorial == true)
                {
                    mTutoriaAttackPadText.gameObject.SetActive(false);
                    mTutorialUIWeaponButton.onClick.RemoveAllListeners();
                    mTutorialUIWeaponButton.onClick.AddListener(() => { WeaponChange(); });
                    mTutorialUIWeaponButton.gameObject.SetActive(true);
                }
                else
                {
                    mUIWeaponButton.onClick.RemoveAllListeners();
                    mUIWeaponButton.onClick.AddListener(() => { WeaponChange(); });
                    mUIWeaponButton.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isTutorial==true)
            {
                mTutoriaAttackPadText.gameObject.SetActive(true);
                mTutorialUIWeaponButton.gameObject.SetActive(false);
            }
            else
            {
                mUIWeaponButton.gameObject.SetActive(false);
            }
        }
    }

}
