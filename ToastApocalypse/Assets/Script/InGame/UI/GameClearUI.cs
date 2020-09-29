using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameClearUI : MonoBehaviour,IPointerClickHandler
{
    public static GameClearUI Instance;
    public Animator mAnim;
    public Image NotouchArea,mNPCWindow;
    public Text TitleText;
    public MapNPCSlot mNPCSlot;
    public Transform mParents;

    public MaterialSlot[] mMaterialSlot;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (GameSetting.Instance.Language==0)
            {
                TitleText.text = "구출한 시민";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                TitleText.text = "Rescued citizen";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetItem(int StageId)
    {
        StageMaterialController.Instance.GetMaterialArr(StageId);
        for (int i=0; i<mMaterialSlot.Length;i++)
        {
            int Sequence = Random.Range(0, mMaterialSlot.Length);
            for (int j=0; j< mMaterialSlot.Length;j++)
            {
                float rate = Random.Range(0,1f);
                if (rate< StageMaterialController.Instance.mStageMaterialArr[Sequence].mRate)
                {
                    mMaterialSlot[i].SetData(StageMaterialController.Instance.mStageMaterialArr[Sequence].mID);
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
                ShowNPCWindow();
            }
        }
        else
        {
            UIController.Instance.mPieceImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
            UIController.Instance.mPieceImage.gameObject.SetActive(true);
            ShowNPCWindow();
        }
    }

    private IEnumerator PartsAnim()
    {
        NotouchArea.gameObject.SetActive(true);
        UIController.Instance.mPieceImage.gameObject.SetActive(true);
        mAnim.SetBool(AnimHash.Parts, true);
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        yield return delay;
        delay = new WaitForSeconds(1f);
        mAnim.SetBool(AnimHash.Parts, false);
        GameSetting.Instance.StageOpen[GameSetting.Instance.NowStage] = true;
        GameSetting.Instance.StagePartsget[GameSetting.Instance.NowStage]=true;
        SoundController.Instance.SESoundUI(6);
        yield return delay;
        NotouchArea.gameObject.SetActive(false);
        ShowNPCWindow();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GotoMain();
    }

    public void GotoMain()
    {
        gameObject.SetActive(false);
        GameController.Instance.MainMenu();
    }

    public void ShowNPCWindow()
    {
        if (GameController.Instance.RescueNPCList.Count > 0)
        {
            for (int i = 0; i < GameController.Instance.RescueNPCList.Count; i++)
            {
                GameSetting.Instance.NPCOpen[GameController.Instance.RescueNPCList[i]] = true;
                MapNPCSlot slot = Instantiate(mNPCSlot, mParents);
                slot.SetData(GameController.Instance.RescueNPCList[i]);
            }
            mNPCWindow.gameObject.SetActive(true);
        }
    }
}
