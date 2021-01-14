using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool isCollide;
    public float mDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollide = true;
            DamageCount();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
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
