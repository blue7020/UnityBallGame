using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Player mTarget;
    public bool DamageOn;
    public eTrapType mType;
    public float mValue;
    public float mBackup;
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
                        TrapTrigger = true;
                        //애니메이션에서 처리
                        break;
                    case eTrapType.Slow:
                        mTarget = other.GetComponent<Player>();
                        Slow();
                        break;
                    case eTrapType.Spike:
                        mTarget = other.GetComponent<Player>();
                        TrapTrigger = true;
                        break;
                    default:
                        Debug.LogError("Wrong Trap Type");
                        break;
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
                    break;
                case eTrapType.Slow:
                    mTarget.buffIncrease[3] += mValue;
                    mTarget = null;
                    break;
                case eTrapType.Spike:
                    TrapTrigger = false;
                    mTarget = null;
                    break;
                default:
                    Debug.LogError("Wrong Trap Type");
                    break;
            }
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

    public void Slow()
    {
        if (mTarget != null)
        {
            mTarget.buffIncrease[3] += -mValue;
        }
    }
}
