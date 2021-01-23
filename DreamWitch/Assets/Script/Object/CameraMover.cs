using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Vector3 mMinValue,mMaxValue;
    public float CameraSize;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CameraSize!=0)
            {
                CameraMovement.Instance.gameObject.GetComponent<Camera>().orthographicSize= CameraSize;
            }
            CameraMovement.Instance.mMinValue = mMinValue;
            CameraMovement.Instance.mMaxValue = mMaxValue;
            Player.Instance.mRB2D.velocity = Vector2.zero;
        }
    }
}
