using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    public Transform mTarget;
    public bool mTracking;
    public Vector3 mOffset;
    public float mSmoothFactor;

    //Camera Clamp
    public Vector3 mMinValue, mMaxValue;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Follow()
    {
        Vector3 TargetPos = mTarget.position+ mOffset;
        Vector3 ClampPos = new Vector3(Mathf.Clamp(TargetPos.x, mMinValue.x, mMaxValue.x),
            Mathf.Clamp(TargetPos.y, mMinValue.y, mMaxValue.y),
            Mathf.Clamp(TargetPos.z, mMinValue.z, mMaxValue.z));

        Vector3 SmoothedPos = Vector3.Lerp(transform.position, ClampPos, mSmoothFactor*Time.fixedDeltaTime);
        transform.position = SmoothedPos;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mTracking)
        {
            Follow();
        }
    }

}
