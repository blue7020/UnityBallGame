﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public Weapon weapon;
    public Animator mAnim;
    public Transform BulletStarter;
    public SpriteRenderer mRenderer;
    private PlayerBullet bolt;

    private Enemy Target;

    public void Melee()
    {
        mAnim.SetBool(AnimHash.Attack, true);
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
        weapon.nowBullet--;
        UIController.Instance.mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
        bolt = PlayerBulletPool.Instance.GetFromPool(weapon.BoltID);
        ResetDir();
        if (weapon.PlusBulletCount >1)
        {
            StartCoroutine(PlusBullet());
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
            bolt = PlayerBulletPool.Instance.GetFromPool(weapon.BoltID);
            ResetDir();
            count++;
            yield return delay;
        }
    }

    private void ResetDir()
    {
        bolt.mWeaponID = weapon.mID;
        float rand = UnityEngine.Random.Range(0, 1f);
        if (rand <= Player.Instance.mStats.Crit / 100)
        {
            bolt.mDamage = (Player.Instance.mStats.Atk *(1+ Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage);

        }
        else
        {
            bolt.mDamage = Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]);
        }
        bolt.transform.position = BulletStarter.position;
        bolt.mRB2D.AddForce(BulletStarter.up*bolt.mSpeed,ForceMode2D.Impulse);
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
                    WeaponController.Instance.WeaponSkill(weapon.mID, Target);
                    float rand = UnityEngine.Random.Range(0, 1f);
                    if (rand <= Player.Instance.mStats.Crit / 100)
                    {
                        Target.Hit((Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage));

                    }
                    else
                    {
                        Target.Hit(Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]));
                    }


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
