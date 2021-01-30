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
    public bool mFollowing,CurserFollow;
    public Rigidbody2D mRB2D;

    //Camera Clamp
    public Vector3[] mMinValueArr;
    public Vector3[] mMaxValueArr;

    public Vector3 mMinValue;
    public Vector3 mMaxValue;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (!CurserFollow)
            {
                mMinValue = mMinValueArr[TitleController.Instance.NowStage];
                mMaxValue = mMaxValueArr[TitleController.Instance.NowStage];
            }
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
        Vector3 TargetPos = mTarget.position + mOffset;
        Vector3 ClampPos = new Vector3(Mathf.Clamp(TargetPos.x, mMinValue.x, mMaxValue.x),
            Mathf.Clamp(TargetPos.y, mMinValue.y, mMaxValue.y),
            Mathf.Clamp(TargetPos.z, mMinValue.z, mMaxValue.z));

        Vector3 SmoothedPos = Vector3.Lerp(transform.position, ClampPos, mSmoothFactor*Time.fixedDeltaTime);
        transform.position = SmoothedPos;
    }

    public void MouseFollow()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -10));
        Vector3 ClampPos = new Vector3(Mathf.Clamp(TargetPos.x, mMinValue.x, mMaxValue.x),
            Mathf.Clamp(TargetPos.y, mMinValue.y, mMaxValue.y),
            Mathf.Clamp(TargetPos.z, mMinValue.z, mMaxValue.z));

        Vector3 SmoothedPos = Vector3.Lerp(transform.position, ClampPos, mSmoothFactor * Time.fixedDeltaTime);
        transform.position = SmoothedPos;
    }

    public void CameraMove(Vector3 pos,float time)
    {
        mFollowing = false;
        Vector3 TargetPos = pos + mOffset;
        Vector3 ClampPos = new Vector3(Mathf.Clamp(TargetPos.x, mMinValue.x, mMaxValue.x),
    Mathf.Clamp(TargetPos.y, mMinValue.y, mMaxValue.y),
    Mathf.Clamp(TargetPos.z, mMinValue.z, mMaxValue.z));
        Vector3 SmoothedPos = Vector3.Lerp(ClampPos, TargetPos, mSmoothFactor);
        mRB2D.DOMove(SmoothedPos, time);
    }

    public IEnumerator CameraFollowDelay(float time)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        mFollowing = false;
        yield return delay;
        mFollowing = true;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        mFollowing = false;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.position = new Vector3(transform.position.x+x, transform.position.y+y, -10);

            elapsed += Time.deltaTime;

            yield return null;
        }
        mFollowing = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mFollowing)
        {
            if (mTarget != null)
            {
                Follow();
            }
            else if (CurserFollow)
            {
                MouseFollow();
            }
        }
    }

}
