using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IslandSelect : MonoBehaviour,IPointerClickHandler
{
    public int mID;
    public bool IsRight;
    public Vector3 pos;
    public Transform IconPos;
    public Animator mAnim,mCloudAnim;
    public StageSelectController mController;

    private void Awake()
    {
        if (SaveDataController.Instance.mUser.StageShow[mID]==true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (SaveDataController.Instance.mUser.StageClear[mID] == false)
        {
            if (SaveDataController.Instance.mUser.StageShowEvent[mID] == false)
            {
                mController.isShowNewStage = true;
                mController.ShowNewStage(mID);
            }
            else
            {
                HideCloud();
                mAnim.SetBool("IsClear", false);
            }
        }
        else
        {
            HideCloud();
            mAnim.SetBool("IsClear", false);
        }
    }

    public void PlayerIconMove()
    {
        StageSelectController.Instance.mPlayerIcon.transform.position=IconPos.position;
    }

    public void ShowStage()
    {
        mAnim.SetBool("IsClear", true);
        mCloudAnim.SetBool("IsClear", true);
        SaveDataController.Instance.mUser.StageShowEvent[mID] = true;
    }

    public void HideCloud()
    {
        if (mCloudAnim != null)
        {
            mCloudAnim.gameObject.SetActive(false);
        }
    }

    public void CameraFollowing()
    {
        mController.isShowPopup = false;
        StartCoroutine(SelectDelay());
        CameraMovement.Instance.mFollowing = true;
    }

    public IEnumerator SelectDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        PlayerIconMove();
        StageSelectController.Instance.mChapterWindow.gameObject.SetActive(false);
        StageSelectController.Instance.isSelectDelay = true;
        StageSelectController.Instance.ShowStageSelectUI(mID, IsRight);
        yield return delay;
        if (StageSelectController.Instance.StageChapterCountArr[mID]>1)
        {
            StageSelectController.Instance.ChapterWindowSetting(mID);
        }
        StageSelectController.Instance.isSelectDelay = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!StageSelectController.Instance.isSelectDelay&& SaveDataController.Instance.mUser.StageShowEvent[mID] == true)
        {
            StartCoroutine(SelectDelay());
        }
    }
}