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

    private void Awake()
    {
        if (SaveDataController.Instance.mUser.StageClear[mID] == false)
        {
            if (SaveDataController.Instance.mUser.StageShowEvent[mID] == false)
            {
                StageSelectController.Instance.isShowNewStage = true;
                StageSelectController.Instance.NowStage = mID;
            }
            else
            {
                if (mCloudAnim != null)
                {
                    mCloudAnim.gameObject.SetActive(false);
                }
                mAnim.SetBool("IsClear", false);
            }
        }
        else
        {
            if (mCloudAnim != null)
            {
                mCloudAnim.gameObject.SetActive(false);
            }
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

    public IEnumerator SelectDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        StartCoroutine(CameraMovement.Instance.CameraFollowDelay(1f));
        PlayerIconMove();
        StageSelectController.Instance.isSelectDelay = true;
        StageSelectController.Instance.ShowStageSelectUI(mID, IsRight);
        yield return delay;
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