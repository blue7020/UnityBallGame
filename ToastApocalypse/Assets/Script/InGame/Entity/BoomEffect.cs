using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    public PlayerBullet mBullet;
    public bool PlayerIn;
    public bool NoDoubleDamage;
    public Enemy Target;

    private void BoomStart()
    {
        NoDoubleDamage = false;
        PlayerIn = false;
        Target = null;
        StartCoroutine(Boom());
    }
    private IEnumerator Boom()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        SoundController.Instance.SESound(15);
        while (true)
        {
            if (PlayerIn == true&& NoDoubleDamage== false)
            {
                Player.Instance.Hit(mBullet.mDamage / 2);
                NoDoubleDamage = true;
            }
            if (Target != null && NoDoubleDamage == false)
            {
                Target.Hit(mBullet.mDamage,1,false);
                NoDoubleDamage = true;
            }
            yield return delay;
        }
    }

    private void ActiveFalse()
    {
        mBullet.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerIn = true;
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
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerIn = false;
        }
    }
}
