using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : Timer
{
    public static AttackArea instance;

    private Animator mAnim;
    [SerializeField]
    private bool mAttackEnd;
    [SerializeField]
    private Weapon weapon;
    public Transform BulletStarter;
    private SpriteRenderer mRenderer;

    private Enemy Target;


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
        mRenderer = GetComponent<SpriteRenderer>();
        mAnim = GetComponent<Animator>();
        mAttackEnd=false;
    }

    //TODO 이펙트 풀을 사용하여 플레이어 캐릭터에 맞는 공격 스프라이트로 변경 원거리, 근접 공격, 근접 범위 공격
    public void Melee()
    {
        if (weapon.eType == eWeaponType.Melee)
        {
            gameObject.SetActive(true);
            if (mAttackEnd == true)
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
        }
    }

    public void Range()
    {
        if (weapon.eType == eWeaponType.Range)
        {
            PlayerBullet bolt = PlayerBulletPool.Instance.GetFromPool(0);//TODO 플레이어 무기에 따라 투사체 ID변경
            bolt.transform.position = Player.Instance.transform.position;
            bolt.ResetDir();
        }
    }

    public void AttackEnd()
    {
        mAnim.SetBool(AnimHash.Attack, false);
        gameObject.SetActive(false);
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
                    float rand = UnityEngine.Random.Range(0, 1f);
                    if (rand <= Player.Instance.Stats.Crit / 100)
                    {
                        Target.Hit(Player.Instance.Stats.Atk * (1 + (Player.Instance.Stats.CritDamage / 100)));

                    }
                    else
                    {
                        Target.Hit(Player.Instance.Stats.Atk);
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
