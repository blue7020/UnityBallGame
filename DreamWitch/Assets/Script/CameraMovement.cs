using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    public Transform mTarget;
    public Vector3 mOffset;
    [Range(1,10)]
    public float mSmoothFactor;

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
        Vector3 TargetPos = mTarget.position + mOffset;
        Vector3 SmoothedPos = Vector3.Lerp(transform.position,TargetPos, mSmoothFactor*Time.fixedDeltaTime);
        transform.position = SmoothedPos;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Follow();
    }

}
