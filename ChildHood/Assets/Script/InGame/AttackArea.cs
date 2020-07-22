using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public Weapon weapon;
    public Animator mAnim;
    public Transform BulletStarter;
    public SpriteRenderer mRenderer;
    private PlayerBullet bolt;

    private Enemy Target;

    //TODO 이펙트 풀을 사용하여 플레이어 캐릭터에 맞는 공격 스프라이트로 변경 원거리, 직선 공격, 범위 공격
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
        UIController.Instance.bulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
        bolt = PlayerBulletPool.Instance.GetFromPool(0);//TODO 플레이어 무기에 따라 투사체 ID변경
        ResetDir();
    }

    private void ResetDir()
    {
        bolt.mWeaponID = weapon.mID;
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
                        Target.Hit(Player.Instance.mStats.Atk + (Player.Instance.mStats.Atk * (1 + Player.Instance.mStats.CritDamage)));

                    }
                    else
                    {
                        Target.Hit(Player.Instance.mStats.Atk);
                    }


                }
            }
            if (other.gameObject.CompareTag("Bullet"))
            {
                Bullet Target = other.GetComponent<Bullet>();
                Target.gameObject.SetActive(false);

            }
        }
    }
}
