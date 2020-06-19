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

public class Door : MonoBehaviour
{
    public DoorType doorType;

}

