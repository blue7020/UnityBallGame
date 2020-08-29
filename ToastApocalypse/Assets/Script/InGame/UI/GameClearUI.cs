using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameClearUI : MonoBehaviour,IPointerClickHandler
{
    public static GameClearUI Instance;
    public Animator mAnim;
    public Image NotouchArea;

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

    public void StageClear()
    {
        if (GameSetting.Instance.StageOpen[5] == false)
        {
            GameSetting.Instance.StageOpen[GameSetting.Instance.NowStage] = true;
            if (GameSetting.Instance.StagePartsget[GameController.Instance.MapLevel] == false)
            {
                StartCoroutine(PartsAnim());
            }
            else
            {
                UIController.Instance.mPieceImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
                UIController.Instance.mPieceImage.gameObject.SetActive(true);
            }
        }
        else
        {
            UIController.Instance.mPieceImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
            UIController.Instance.mPieceImage.gameObject.SetActive(true);
        }
    }

    private IEnumerator PartsAnim()
    {
        NotouchArea.gameObject.SetActive(true);
        UIController.Instance.mPieceImage.gameObject.SetActive(true);
        mAnim.SetBool(AnimHash.Parts, true);
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        yield return delay;
        NotouchArea.gameObject.SetActive(false);
        mAnim.SetBool(AnimHash.Parts, false);
        GameSetting.Instance.GetParts(GameSetting.Instance.NowStage);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        GameController.Instance.MainMenu();
    }
}
