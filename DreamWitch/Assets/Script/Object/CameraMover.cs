using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Vector3 mMinValue,mMaxValue;
    public bool isStop, isBGMChange;
    public float CameraSize;
    public int BGMCode;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isBGMChange)
            {
                SoundController.Instance.BGMChange(BGMCode);
            }
            if (CameraSize!=0)
            {
                CameraMovement.Instance.gameObject.GetComponent<Camera>().orthographicSize= CameraSize;
            }
            CameraMovement.Instance.mMinValue = mMinValue;
            CameraMovement.Instance.mMaxValue = mMaxValue;
            if (!isStop)
            {
                Player.Instance.mRB2D.velocity = Vector2.zero;
            }
        }
    }
}
