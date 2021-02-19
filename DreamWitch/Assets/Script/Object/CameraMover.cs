using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Vector3 mMinValue,mMaxValue;
    public bool isBGMChange,isChapterChange;
    public float CameraSize;
    public int BGMCode, mChapterCode;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isBGMChange)
            {
                SoundController.Instance.BGMChange(BGMCode);
            }
            if (isChapterChange)
            {
                GameController.Instance.ChapterChange(mChapterCode);
            }
            if (CameraSize!=0)
            {
                CameraMovement.Instance.gameObject.GetComponent<Camera>().orthographicSize= CameraSize;
            }
            CameraMovement.Instance.mMinValue = mMinValue;
            CameraMovement.Instance.mMaxValue = mMaxValue;
        }
    }
}
