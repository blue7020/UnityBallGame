using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.FallingDamage();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().Damage(other.gameObject.GetComponent<Enemy>().mMaxHP);
        }
        if (other.gameObject.CompareTag("EnemyBolt"))
        {
            other.gameObject.GetComponent<EnemyBolt>().gameObject.SetActive(false);
        }
    }
}
