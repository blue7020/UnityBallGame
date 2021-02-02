using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Darkness.Instance.CancelInvoke();
            Darkness.Instance.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
