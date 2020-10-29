using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int mWeaponID;
    public float mDamage;
    public ePlayerBulletType eType;
    public ePlayerBulletCritEffectType eCritEffectType;
    public ePlayerBulletEffectType eEffectType;
    public float mEffectTime;
    public float mEffectAmount;
    public bool returnPlayer;
    public bool returnCheck;
    public float mSpeed;
    public Rigidbody2D mRB2D;
    public Enemy Target;
    public Vector3 mboltscale;
    public Animator mAnim;
    public BoomEffect mBoomEffect;
    public BoxCollider2D mBC2D;
    public bool IsBoom,DeleteBullet;

    private void OnEnable()
    {
        if (eType == ePlayerBulletType.granade)
        {
            mBoomEffect.gameObject.SetActive(false);
            mBC2D.isTrigger = false;
            IsBoom = false;
        }
    }

    private void Awake()
    {
        mboltscale = transform.localScale;
        returnPlayer = false;
        returnCheck = false;
        Target = null;
        if (eType==ePlayerBulletType.shotgun)
        {
            StartCoroutine(ShotGunDamage());
        }
        else if (eType == ePlayerBulletType.fire)
        {
            StartCoroutine(Fire());
        }
    }

    public void Boom()
    {
        gameObject.SetActive(false);
    }

    private void DoStop()
    {
        if (IsBoom==false)
        {
            IsBoom = true;
            StartCoroutine(Stop());
        }
    }
    public IEnumerator Stop()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        mBoomEffect.gameObject.SetActive(true);
        mBC2D.isTrigger = true;
        while (true)
        {
            mRB2D.velocity = Vector3.zero;
            yield return delay;
        }
    }

    private IEnumerator Fire()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);
        yield return delay;
        gameObject.SetActive(false);
    }

    private IEnumerator ShotGunDamage()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);
        while (true)
        {
            yield return delay;
            float damage = mDamage;
            mDamage = damage / 2;
            if (mDamage<1)
            {
                break;
            }
        }
    }

    private void Effect()
    {
        switch (eEffectType)
        {
            case ePlayerBulletEffectType.none:
                break;
            case ePlayerBulletEffectType.slow:
                Target.StartCoroutine(Target.SpeedNurf(mEffectAmount, mEffectTime));
                break;
            case ePlayerBulletEffectType.stun:
                Target.StartCoroutine(Target.Stuned(mEffectTime));
                break;
        }
    }

    public void CritEffect()
    {
        switch (eCritEffectType)
        {
            case ePlayerBulletCritEffectType.none:
                break;
            case ePlayerBulletCritEffectType.slow:
                Target.StartCoroutine(Target.SpeedNurf(mEffectAmount, mEffectTime));
                break;
            case ePlayerBulletCritEffectType.stun:
                Target.StartCoroutine(Target.Stuned(mEffectTime));
                break;
        }
    }

    public IEnumerator MovetoEnemyTargetBolt(Enemy TurretTarget)
    {
        WaitForSeconds one = new WaitForSeconds(0.1f);
        Vector3 Pos = TurretTarget.transform.position;
        Vector3 dir = Pos - transform.position;
        mRB2D.velocity = dir.normalized * mSpeed;
        yield return one;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = other.GetComponent<Enemy>();
            if (Target.mCurrentHP > 0 && Target != null)
            {
                Effect();
                float rand = Random.Range(0, 1f);
                if (rand <= Player.Instance.mStats.Crit+ Player.Instance.buffIncrease[5])
                {
                    CritEffect();
                    Target.Hit(mDamage, 1, true);
                }
                else
                {
                    Target.Hit(mDamage, 1, false);
                }
            }
            if (eType == ePlayerBulletType.normal || eType == ePlayerBulletType.shotgun|| eType == ePlayerBulletType.homing)
            {
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            if (eType != ePlayerBulletType.granade|| eType != ePlayerBulletType.boom)
            {
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (returnPlayer == true && eType == ePlayerBulletType.boomerang)
            {
                returnCheck = true;
                gameObject.SetActive(false);
            }

        }
        if (other.gameObject.CompareTag("Bullet")&& DeleteBullet==true)
        {
            Bullet Target = other.GetComponent<Bullet>();
            if (Target != null && Target.eType != eEnemyBulletType.boom)
            {
                Target.RemoveBullet();
            }

        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (eType == ePlayerBulletType.granade)
            {
                IsBoom = true;
                StartCoroutine(Stop());
            }
        }
    }
}
