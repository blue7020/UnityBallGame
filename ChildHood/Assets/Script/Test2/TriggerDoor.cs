using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField]
    public GameObject mDoor;

    private void Start()
    {
        mDoor.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            {
            mDoor.gameObject.SetActive(true);
        }

    }
}
