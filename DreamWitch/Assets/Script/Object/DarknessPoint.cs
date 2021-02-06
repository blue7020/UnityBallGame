using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessPoint : MonoBehaviour
{
    public bool isEnd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isEnd)
            {
                Darkness.Instance.CancelInvoke();
                Darkness.Instance.isHide=true;
            }
            else
            {
                Darkness.Instance.isHide = false;
                Darkness.Instance.Show();
                Darkness.Instance.isMoving = true;
                Darkness.Instance.Moving();
            }
        }
    }
}
