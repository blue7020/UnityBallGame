using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Player mTarget;
    [SerializeField]
    private eTrapType mType;
    [SerializeField]
    private float mValue;
    private bool TrapTrigger;//애니메이션 비례 함정 작동

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
                        StartCoroutine(Spike());
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
                    //애니메이션에서 처리
                    break;
                case eTrapType.Slow:
                    mTarget.mStats.Spd += mValue;
                    mTarget = null;
                    break;
                case eTrapType.Spike:
                    TrapTrigger = false;
                    StopCoroutine(Spike());
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
            if(TrapTrigger==true){
                mTarget.Hit(mValue);
            }
            
        }
    }

    public void Slow()
    {
        if (mTarget != null)
        {
            mTarget.mStats.Spd -= mValue;
        }
    }

    private IEnumerator Spike()
    {
        WaitForSeconds Delay = new WaitForSeconds(0.5f);
        Damage();
        yield return Delay;
    }
}
