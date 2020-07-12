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
    private bool PlayerOnTrap;//플레이어가 함정 위에 있는가

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameController.Instance.pause==false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                switch (mType)
                {
                    case eTrapType.Damage:
                        mTarget = other.GetComponent<Player>();
                        TrapTrigger = true;
                        //애니메이션에서 처리
                        break;
                    case eTrapType.Slow:
                        mTarget = other.GetComponent<Player>();
                        Slow();
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
                case eTrapType.Damage:
                    TrapTrigger = false;
                    //애니메이션에서 처리
                    break;
                case eTrapType.Slow:
                    mTarget.mStats.Spd += mValue;
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
}
