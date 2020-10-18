using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Player mTarget;
    public List<Enemy> mTargetMob;
    public bool DamageOn;
    public eTrapType mType;
    public eTrapObjectType mObjType;
    public float mValue;
    public float mBackup;
    public float Dura;
    public bool EnemyDamage;
    public bool TrapTrigger;//애니메이션 비례 함정 작동
    public Enemy mEnemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameController.Instance.pause == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                switch (mType)
                {
                    case eTrapType.TickSpike:
                        mTarget = other.GetComponent<Player>();
                        DamageOn = true;
                        TrapTrigger = true;
                        break;
                    case eTrapType.Slow:
                        mTarget = other.GetComponent<Player>();
                        Slow();
                        break;
                    case eTrapType.Spike:
                        mTarget = other.GetComponent<Player>();
                        DamageOn = true;
                        TrapTrigger = true;
                        break;
                    case eTrapType.Heal:
                        mTarget = other.GetComponent<Player>();
                        TrapTrigger = true;
                        break;
                    default:
                        Debug.LogError("Wrong Trap Type");
                        break;
                }

            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (EnemyDamage == true)
                {
                    mTargetMob.Add(other.GetComponent<Enemy>());
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (mType)
            {
                case eTrapType.TickSpike:
                    TrapTrigger = false;
                    DamageOn = false;
                    mTarget = null;
                    break;
                case eTrapType.Slow:
                    if (Player.Instance.TrapResistance == false)
                    {
                        mTarget.buffIncrease[3] += mValue;
                    }
                    mTarget = null;
                    break;
                case eTrapType.Spike:
                    TrapTrigger = false;
                    DamageOn = false;
                    mTarget = null;
                    break;
                case eTrapType.Heal:
                    TrapTrigger = false;
                    mTarget = null;
                    break;
                default:
                    Debug.LogError("Wrong Trap Type");
                    break;
            }
        }
        if (EnemyDamage == true && other.gameObject.CompareTag("Enemy"))
        {
            mTargetMob.Remove(other.GetComponent<Enemy>());
        }

    }

    public void Damage()
    {
        if (TrapTrigger == true && mTarget != null)
        {
            if (mObjType == eTrapObjectType.normal)
            {
                if (DamageOn == true && Player.Instance.TrapResistance == false)
                {
                    mTarget.Hit(mValue + mTarget.mStats.Def);//고정피해
                    mTarget.LastHitEnemy = null;
                    mTarget.DeathBy = 2;

                    if (GameController.Instance.IsTutorial == false)
                    {
                        UIController.Instance.ShowHP();
                    }
                    else
                    {
                        TutorialUIController.Instance.ShowHP();
                    }
                }
            }
            else
            {
                if (DamageOn == true)
                {
                    mTarget.Hit(mValue + mTarget.mStats.Def);//고정피해
                    Player.Instance.LastHitEnemy = mEnemy;
                }
                if (GameController.Instance.IsTutorial == false)
                {
                    UIController.Instance.ShowHP();
                }
                else
                {
                    TutorialUIController.Instance.ShowHP();
                }
            }
        }
    }

    public IEnumerator Dispose()
    {
        WaitForSeconds delay = new WaitForSeconds(Dura);
        yield return delay;
        gameObject.SetActive(false);
    }

    public void Slow()
    {
        if (mTarget != null && Player.Instance.TrapResistance == false)
        {
            mTarget.buffIncrease[3] += -mValue;
        }
    }

    public void Heal()
    {
        if (TrapTrigger == true)
        {
            Player.Instance.Heal(mValue);
        }
    }
    public void EnemyHit()
    {
        for (int i = 0; i < mTargetMob.Count; i++)
        {
            mTargetMob[i].Hit((Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[1])) / 4,0,false);
        }
    }
}
