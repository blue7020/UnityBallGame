using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLinePortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           StartCoroutine(other.GetComponent<SpriteOutLine>().Outline());
        }
    }
}
