using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public PlayerBullet mBullet;
    public float mDamage;
    public float mPlayerAttackPer;
    public float AttackSpeed;
    public float LifeTime;
    public eTurretType eType;
    public Rigidbody2D mRB2D;
    public float mSpeed;

    public Enemy mTarget;

    private void Awake()
    {
        mTarget = null;
        mDamage = (Player.Instance.mStats.Atk + Player.Instance.buffIncrease[0])* mPlayerAttackPer;
        StartCoroutine(LifeTimeCycle());
    }

    public IEnumerator LifeTimeCycle()
    {
        WaitForSeconds delay = new WaitForSeconds(LifeTime);
        StartCoroutine(AttackCycle());
        yield return delay;
        Destroy(gameObject);
    }

    public IEnumerator AttackCycle()
    {
        WaitForSeconds delay = new WaitForSeconds(AttackSpeed);
        while (true)
        {
            if (mTarget!=null)
            {
                PlayerBullet bullet = Instantiate(mBullet,transform);
                bullet.transform.position = transform.position;
                bullet.mDamage = mDamage;
                bullet.StartCoroutine(bullet.MovetoEnemyTargetBolt(mTarget));
            }
            yield return delay;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (mTarget == null)
            {
                mTarget = other.GetComponent<Enemy>();
            }
            else
            {
                if (mTarget.mCurrentHP < 1)
                {
                    mTarget = null;
                }
            }
        }
    }
}
