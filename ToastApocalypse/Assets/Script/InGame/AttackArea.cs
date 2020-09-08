using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public Weapon weapon;
    public Animator mAnim;
    public Transform BulletStarter;
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
            weapon.nowBullet--;
            UIController.Instance.mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
            WeaponController.Instance.WeaponSkill(weapon.mID, Target, IsCrit);
            ResetDir();
            if (weapon.PlusBulletCount > 1&& weapon.nowBullet > weapon.PlusBulletCount)
            {
                StartCoroutine(PlusBullet());
            }
        }
    }
    
    private IEnumerator PlusBullet()
    {
        int count = 1;
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            if (count==weapon.PlusBulletCount)
            {
                break;
            }
            weapon.nowBullet--;
            UIController.Instance.mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
            ResetDir();
            count++;
            yield return delay;
        }
    }

    private void ResetDir()
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
                    if (rand <= Player.Instance.mStats.Crit / 100)
                    {
                        Target.Hit((Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage));
                        IsCrit = true;
                    }
                    else
                    {
                        Target.Hit(Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]));
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
