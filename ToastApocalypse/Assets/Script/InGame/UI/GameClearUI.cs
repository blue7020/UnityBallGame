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

    public MaterialSlot[] mSlot;

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

    public void GetItem(int StageId)
    {
        StageMaterialController.Instance.GetMaterialArr(StageId);
        for (int i=0; i<mSlot.Length;i++)
        {
            int Sequence = Random.Range(0, mSlot.Length);
            for (int j=0; j< mSlot.Length;j++)
            {
                float rate = Random.Range(0,1f);
                if (rate< StageMaterialController.Instance.mStageMaterialArr[Sequence].mRate)
                {
                    mSlot[i].SetData(StageMaterialController.Instance.mStageMaterialArr[Sequence].mID);
                    if (GameSetting.Instance.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID]+1>99)
                    {
                        GameSetting.Instance.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] =99;
                    }
                    else
                    {
                        GameSetting.Instance.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] += 1;
                    }
                    
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void StageClear()
    {
        GetItem(GameSetting.Instance.NowStage);
        if (GameSetting.Instance.NowStage < 6)
        {
            if (GameSetting.Instance.StagePartsget[GameSetting.Instance.NowStage] == false)
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
        GameSetting.Instance.StageOpen[GameSetting.Instance.NowStage] = true;
        GameSetting.Instance.StagePartsget[GameSetting.Instance.NowStage]=true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        GameController.Instance.MainMenu();
    }
}
