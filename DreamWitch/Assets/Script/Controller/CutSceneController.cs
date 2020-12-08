using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public static CutSceneController Instance;
    public Camera mMainCamera, mCutSceneCamera;
    public GameObject mTentacle,mFadeOut;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CutSceneCamera()
    {
        mMainCamera.gameObject.SetActive(false);
        mTentacle.SetActive(true);
        mCutSceneCamera.gameObject.SetActive(true);
        Vector3 TargetPos = new Vector3(-15.5f, 1, 10);

        Vector3 SmoothedPos = Vector3.Lerp(transform.position, TargetPos, 3f * Time.fixedDeltaTime);
        transform.position = SmoothedPos;
    }

    public void ChangeMainCamera()
    {
        mCutSceneCamera.gameObject.SetActive(false);
        mTentacle.SetActive(false);
        mMainCamera.gameObject.SetActive(true);
    }

    public void FadeOut()
    {
        mFadeOut.SetActive(true);
    }
}
