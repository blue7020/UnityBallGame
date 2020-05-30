using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    private Rigidbody2D mRB2D;
    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Walls"))
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

}
