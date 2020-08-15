using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    public Enemy mEnemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Player.Instance.mCurrentHP > 0)
            {
                other.gameObject.GetComponent<Player>().Hit(mEnemy.mStats.Atk);
            }
        }
    }
}
