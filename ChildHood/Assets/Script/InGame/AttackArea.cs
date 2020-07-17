using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : Timer
{
    public static AttackArea Instance;

    private Animator mAnim;
#pragma warning disable 0649
    [SerializeField]
    private bool mAttackEnd;
    [SerializeField]
    private Weapon weapon;
#pragma warning restore 0649
    public Transform BulletStarter;
    private SpriteRenderer mRenderer;
    private PlayerBullet bolt;

    private Enemy Target;


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
        mRenderer = GetComponent<SpriteRenderer>();
        mAnim = GetComponent<Animator>();
        mAttackEnd=false;
    }

    //TODO 이펙트 풀을 사용하여 플레이어 캐릭터에 맞는 공격 스프라이트로 변경 원거리, 직선 공격, 범위 공격
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
            weapon.nowBullet--;
            UIController.Instance.bulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
            bolt = PlayerBulletPool.Instance.GetFromPool(0);//TODO 플레이어 무기에 따라 투사체 ID변경
            ResetDir();
        }
    }

    private void ResetDir()
    {
        bolt.transform.position = BulletStarter.position;
        bolt.mRB2D.AddForce(BulletStarter.up*bolt.mSpeed,ForceMode2D.Impulse);
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
                    if (rand <= Player.Instance.mStats.Crit / 100)
                    {
                        Target.Hit(Player.Instance.mStats.Atk * (1 + (Player.Instance.mStats.CritDamage / 100)));

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
