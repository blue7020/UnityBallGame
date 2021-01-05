using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    public Transform mTarget;
    public Vector3 mOffset;
    public float mSmoothFactor;
    public bool mFollowing;
    public Rigidbody2D mRB2D;

    //Camera Clamp
    public Vector3 mMinValue, mMaxValue;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mFollowing = true;
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
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

    public void CameraMove(Vector3 pos,float time)
    {
        mFollowing = false;
        Vector3 TargetPos = pos + mOffset;
        Vector3 SmoothedPos = Vector3.Lerp(transform.position, TargetPos, mSmoothFactor);
        mRB2D.DOMove(SmoothedPos, time);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mTarget!=null&& mFollowing)
        {
            Follow();
        }
    }

}
