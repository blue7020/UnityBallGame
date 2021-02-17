using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTile : MonoBehaviour
{
    public GameObject mTiles;
    public bool isHide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mTiles.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isHide)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                mTiles.gameObject.SetActive(true);
            }
        }
    }
}
