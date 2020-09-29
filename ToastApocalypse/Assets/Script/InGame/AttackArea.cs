using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public Weapon weapon;
    public Animator mAnim;
    public Transform BulletStarter;
    public ParticleSystem FireStarter;
    public SpriteRenderer mRenderer;

    private Enemy Target;
    public bool IsCrit;

    private void Awake()
    {
        IsCrit = false;
    }

    public void Melee()
    {
        mAnim.SetBool(AnimHash.Attack, true);
        SoundController.Instance.SESound(8);
        WeaponController.Instance.WeaponSkill(weapon.mID, Target, IsCrit);
        if (Player.Instance.ver > 0) //상
        {
            mRenderer.sortingOrder = 0;
        }
        if (Player.Instance.ver < 0) //하
        {
            mRenderer.sortingOrder = 3;
        }
    }

    public void Range()
    {
        if (weapon.nowBullet>0)
        {
            switch (weapon.SoundId)
            {
                case 0://일반
                    SoundController.Instance.SESound(9);
                    break;
                case 1://소스류
                    SoundController.Instance.SESound(10);
                    break;
                case 2://트리플샷
                    SoundController.Instance.SESound(11);
                    break;
                case 3://샷건
                    SoundController.Instance.SESound(13);
                    break;
                case 4://소드오프
                    SoundController.Instance.SESound(12);
                    break;
                case 5://유탄발사기
                    SoundController.Instance.SESound(14);
                    break;
                case 6://투석
                    SoundController.Instance.SESound(8);
                    break;
                case 7://방사기
                    SoundController.Instance.SESound(16);
                    break;
                case 8://표창
                    SoundController.Instance.SESound(8);
                    break;
            }
            if (Player.Instance.InfiniteAmmo==false)
            {
                weapon.nowBullet--;
            }
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
            }
            else
            {
                TutorialUIController.Instance.mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
            }
            WeaponController.Instance.WeaponSkill(weapon.mID, Target, IsCrit);
            ResetDir();
        }
    }

    public void Fire()
    {
        FireStarter.gameObject.SetActive(true);
        if (weapon.nowBullet > 0)
        {
            SoundController.Instance.SESoundLong(16);
            if (Player.Instance.InfiniteAmmo == false)
            {
                weapon.nowBullet--;
            }
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
            }
            else
            {
                TutorialUIController.Instance.mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
            }
            WeaponController.Instance.WeaponSkill(weapon.mID, Target, IsCrit);
            FireStarter.Play();
        }
    }

    public void ResetDir()
    {
        float currentXStart = weapon.mBoltXGap * ((weapon.PlusWideBulletCount - 1) / 2);
        Vector3 Xpos = new Vector3(currentXStart, 0, 0);
        for (int i = 0; i < weapon.PlusWideBulletCount; i++)
        {
            PlayerBullet bolt = PlayerBulletPool.Instance.GetFromPool(weapon.BoltID);
            bolt.mWeaponID = weapon.mID;
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand <= Player.Instance.mStats.Crit / 100)
            {
                bolt.mDamage = (Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage);

            }
            else
            {
                bolt.mDamage = Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]);
            }
            bolt.transform.position = BulletStarter.position+ Xpos;
            bolt.transform.localScale = bolt.mboltscale * (1 + PassiveArtifacts.Instance.AdditionalBulletSize);
            bolt.mRB2D.AddForce(BulletStarter.up * bolt.mSpeed, ForceMode2D.Impulse);
            Xpos.x += weapon.mBoltXGap;
        }
    }


    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon.eType == eWeaponType.Melee)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Target = other.GetComponent<Enemy>();
                if (Target.mCurrentHP > 0 && Target != null)
                {
                    float rand = Random.Range(0, 1f);
                    if (rand <= Player.Instance.mStats.Crit)
                    {
                        Target.Hit((Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage),0,true);
                        IsCrit = true;
                    }
                    else
                    {
                        Target.Hit(Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]),0,false);
                    }
                    IsCrit = false;

                }
            }
            if (other.gameObject.CompareTag("Bullet"))
            {
                Bullet Target = other.GetComponent<Bullet>();
                if (Target != null && Target.eType != eEnemyBulletType.boom)
                {
                    Target.RemoveBullet();
                }

            }
        }
    }
}
