using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    public PlayerBullet mBullet;
    public bool NoDoubleDamage;
    public Enemy Target;

    private void BoomStart()
    {
        NoDoubleDamage = false;
        Target = null;
        SoundController.Instance.SESound(15);
    }

    private void ActiveFalse()
    {
        mBullet.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& NoDoubleDamage == false)
        {
            NoDoubleDamage = true;
            Player.Instance.Hit(mBullet.mDamage * 0.7f,1);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = other.GetComponent<Enemy>();
            if (Target.mCurrentHP > 0 && Target != null)
            {
                Target.Hit(mBullet.mDamage,1,false);
            }
        }
    }
}
