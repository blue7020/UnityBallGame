using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Player mPlayer;
    private void LateUpdate()
    {
        Vector2 Pos = mPlayer.transform.position;
    }
}
