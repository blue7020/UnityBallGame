using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector2 Pos = Player.Instance.transform.position;
    }
}
