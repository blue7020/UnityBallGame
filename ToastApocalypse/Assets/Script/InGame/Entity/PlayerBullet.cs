using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int mWeaponID;
    public float mDamage;
    public ePlayerBulletType eType;
    public bool returnPlayer;
    public bool returnCheck;
    public float mSpeed;
    public Rigidbody2D mRB2D;
    public Enemy Target;
    public Vector3 mboltscale;
    public Animator mAnim;
    public BoomEffect mBoomEffect;
    public BoxCollider2D mBC2D;
    public bool IsBoom;

    private void OnEnable()
    {
        if (eType == ePlayerBulletType.boom)
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
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            yield return delay;
            float damage = mDamage;
            mDamage = damage / 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = other.GetComponent<Enemy>();
            if (Target.mCurrentHP > 0 && Target != null)
            {
                Target.Hit(mDamage);
            }
            if (eType == ePlayerBulletType.normal || eType == ePlayerBulletType.shotgun)
            {
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Walls"))
        {
            if (eType == ePlayerBulletType.normal|| eType == ePlayerBulletType.fire|| eType == ePlayerBulletType.shotgun)
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
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (eType == ePlayerBulletType.boom)
            {
                IsBoom = true;
                StartCoroutine(Stop());
            }
        }
    }
}
