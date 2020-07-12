using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>().Focus == false)
            {
                Player.Instance.TargetList.Add(other.GetComponent<Enemy>());
                other.GetComponent<Enemy>().Focus = true;
            }

        }
    }
}
