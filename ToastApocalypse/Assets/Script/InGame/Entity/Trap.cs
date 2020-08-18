﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Player mTarget;
    public List<Enemy> mTargetMob;
    public bool DamageOn;
    public eTrapType mType;
    public float mValue;
    public float mBackup;
    public float Dura;
    public bool EnemyDamage;
    public bool TrapTrigger;//애니메이션 비례 함정 작동

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameController.Instance.pause==false)
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
                if (EnemyDamage==true)
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
                    mTarget.buffIncrease[3] += mValue;
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
        if (EnemyDamage==true&& other.gameObject.CompareTag("Enemy"))
        {
            mTargetMob.Remove(other.GetComponent<Enemy>());
        }

    }

    public void Damage()
    {
        if(mTarget!= null)
        {
            if(TrapTrigger==true&&DamageOn==true){
                mTarget.mCurrentHP -= mValue;//고정 피해
            }
            UIController.Instance.ShowHP();
            
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
        if (mTarget != null)
        {
            mTarget.buffIncrease[3] += -mValue;
        }
    }

    public void Heal()
    {
        if (TrapTrigger==true)
        {
            Player.Instance.Heal(mValue);
        }
    }
    public void EnemyHit()
    {
        for (int i = 0; i < mTargetMob.Count; i++)
        {
            mTargetMob[i].Hit((Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[1])) / 2);
        }
    }
}
