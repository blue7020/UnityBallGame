using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialClearUI : MonoBehaviour,IPointerClickHandler
{
    public Image mWindow, NotouchArea;
    public Animator mAnim;

    private void Awake()
    {
        StageClear();
    }
    public void StageClear()
    {
        if (GameSetting.Instance.StagePartsget[GameSetting.Instance.NowStage] == false)
        {
            StartCoroutine(PartsAnim());
        }
        else
        {
            TutorialUIController.Instance.mPieceImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
            TutorialUIController.Instance.mPieceImage.gameObject.SetActive(true);
        }
    }

    private IEnumerator PartsAnim()
    {
        NotouchArea.gameObject.SetActive(true);
        TutorialUIController.Instance.mPieceImage.gameObject.SetActive(true);
        mAnim.SetBool(AnimHash.Parts, true);
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        yield return delay;
        NotouchArea.gameObject.SetActive(false);
        mAnim.SetBool(AnimHash.Parts, false);
        GameSetting.Instance.GetParts(GameSetting.Instance.NowStage);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameController.Instance.MainLobby();
    }
}
