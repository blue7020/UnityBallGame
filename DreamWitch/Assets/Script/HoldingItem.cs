using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.mHold = this;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.mHold = null;
        }

    }
}
