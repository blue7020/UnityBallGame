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
                Darkness.Instance.isMoving = false;
                Darkness.Instance.isHide=true;
            }
            else
            {
                Darkness.Instance.Show();
                Darkness.Instance.isHide = false;
                Darkness.Instance.isMoving = true;
                Darkness.Instance.ResetPattern();
            }
        }
    }
}
