using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWallType
{
    Top,
    Bot,
    Right,
    Left
}
public class WallDir : MonoBehaviour
{
    public eWallType Type;
}
