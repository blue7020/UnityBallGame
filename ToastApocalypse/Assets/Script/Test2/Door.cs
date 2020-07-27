using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    top,
    bot,
    right,
    left
}

public enum RoomType
{
    Normal,
    Special
}

public class Door : MonoBehaviour
{
    public DoorType doorType;
    public RoomType roomType;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (roomType == RoomType.Special)
        {
            if (other.gameObject.CompareTag("Walls"))
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
