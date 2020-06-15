using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBlackOut : MonoBehaviour
{
    [SerializeField]
    private Room room;

    private void Start()
    {
        room.IsFound = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            room.IsFound = true;
            gameObject.SetActive(false);
        }
    }
}
