using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    public float mDamage;
    public bool CritOn;
    public eBulletEffect eType;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy.mCurrentHP >0)
            {
                if (enemy.isFire==false)
                {
                    if (CritOn==true)
                    {
                        float rand = UnityEngine.Random.Range(0, 1f);
                        if (rand <= Player.Instance.mStats.Crit / 100)
                        {
                            mDamage = (Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage);

                        }
                        else
                        {
                            mDamage = Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]);
                        }
                    }
                    else
                    {
                        mDamage = Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]);
                    }
                    enemy.StartCoroutine(enemy.FireDamage(mDamage));
                    if (eType==eBulletEffect.slow)
                    {
                        enemy.StartCoroutine(enemy.SpeedNurf(1,0.2f));
                    }
                }
            }
        }
    }
}
