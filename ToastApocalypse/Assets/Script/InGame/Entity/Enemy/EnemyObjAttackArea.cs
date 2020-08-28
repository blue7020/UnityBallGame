using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjAttackArea : MonoBehaviour
{
    public bool TargetSetting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TargetSetting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TargetSetting = false;
        }
    }
}
