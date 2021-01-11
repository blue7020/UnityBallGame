using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IslandSelect : MonoBehaviour,IPointerClickHandler
{
    public int mID;
    public Vector3 pos;
    public Animator mAnim;

    private void Awake()
    {
        if (SaveDataController.Instance.mUser.StageClear[mID] == false)
        {
            pos = transform.position;
            if (SaveDataController.Instance.mUser.StageShowEvent[mID] == false)
            {
                StageSelectController.Instance.isShowNewStage = true;
                StageSelectController.Instance.NowStage = mID;
            }
            else
            {
                mAnim.SetBool("IsClear", false);
            }
        }
        else
        {
            mAnim.SetBool("IsClear", false);
        }
    }

    public void ShowStage()
    {
        mAnim.SetBool("IsClear", true);
        SaveDataController.Instance.mUser.StageShowEvent[mID] = true;
    }

    public IEnumerator SelectDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        StartCoroutine(CameraMovement.Instance.CameraFollowDelay(1f));
        StageSelectController.Instance.isSelectDelay = true;
        StageSelectController.Instance.ShowStageSelectUI(mID);
        yield return delay;
        StageSelectController.Instance.isSelectDelay = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!StageSelectController.Instance.isSelectDelay)
        {
            StartCoroutine(SelectDelay());
        }
    }
}