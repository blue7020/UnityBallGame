using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool isCollide;
    public float mDamage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollide = true;
            DamageCount();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollide = false;
            CancelInvoke();
        }
    }

    public void DamageCount()
    {
        if (isCollide)
        {
            Player.Instance.Damage(mDamage);
            Invoke("DamageCount",0.7f);
        }
    }
}
