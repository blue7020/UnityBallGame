using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameClearUI : MonoBehaviour,IPointerClickHandler
{
    public static GameClearUI Instance;
    public Animator mAnim;
    public Image NotouchArea,mNPCWindow,mOpenItemWindow,AdsWindow;
    public Text NPCTitleText,OpenItemTitleText,MessageText,AdsText;
    public Button mAdsButton;
    public MapNPCSlot mNPCSlot;
    public Transform mParents;
    public int Sequence, PlusRewardMaterial;

    public MaterialSlot[] mMaterialSlot;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (GameSetting.Instance.Language == 0)
            {
                NPCTitleText.text = "구출한 시민";
                OpenItemTitleText.text = "클리어 보상";
                AdsText.text = "◀ 보상 2배!";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                NPCTitleText.text = "Rescued citizen";
                OpenItemTitleText.text = "Clear reward";
                AdsText.text = "◀ Double Reward!";
            }
            MessageText.text = "";
            AdsWindow.gameObject.SetActive(false);
            PlusRewardMaterial = 1;
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
            Sequence = Random.Range(0, mMaterialSlot.Length);
            for (int j=0; j< mMaterialSlot.Length;j++)
            {
                float rate = Random.Range(0,1f);
                if (rate> StageMaterialController.Instance.mStageMaterialArr[Sequence].mRate)
                {
                    mMaterialSlot[i].SetData(StageMaterialController.Instance.mStageMaterialArr[Sequence].mID);
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
        if (GameController.Instance.ReviveCode == 1)
        {
            GameSetting.Instance.ShowAds(eAdsReward.Revive);
        }
        GetItem(GameSetting.Instance.NowStage);
        if (GameSetting.Instance.NowStage < 6)
        {
            if (SaveDataController.Instance.mUser.StagePartsget[GameSetting.Instance.NowStage] == false)
            {
                StartCoroutine(PartsAnim());
            }
            else
            {
                UIController.Instance.mPieceImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
                UIController.Instance.mPieceImage.gameObject.SetActive(true);
                ShowNPCWindow();
                StageClearGift();
            }
        }
        else
        {
            UIController.Instance.mPieceImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
            UIController.Instance.mPieceImage.gameObject.SetActive(true);
            ShowNPCWindow();
            StageClearGift();
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
        SaveDataController.Instance.mUser.StageOpen[GameSetting.Instance.NowStage] = true;
        SaveDataController.Instance.mUser.StagePartsget[GameSetting.Instance.NowStage]=true;
        SoundController.Instance.SESoundUI(6);
        yield return delay;
        NotouchArea.gameObject.SetActive(false);
        ShowNPCWindow();
        StageClearGift();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GotoMain();
    }

    public void GotoMain()
    {
        if (SaveDataController.Instance.mUser.Syrup + GameController.Instance.SyrupInStage >= 99999)
        {
            SaveDataController.Instance.mUser.Syrup = 99999;
        }
        else
        {
            SaveDataController.Instance.mUser.Syrup += GameController.Instance.SyrupInStage;
        }
        for (int i=0; i<Sequence;i++)
        {
            if (SaveDataController.Instance.mUser.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] + PlusRewardMaterial > 99)
            {
                SaveDataController.Instance.mUser.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] = 99;
            }
            else
            {
                SaveDataController.Instance.mUser.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] += PlusRewardMaterial;
            }
        }
        gameObject.SetActive(false);
        GameController.Instance.MainMenu();
    }

    public void ShowNPCWindow()
    {
        if (GameController.Instance.RescueNPCList.Count > 0)
        {
            for (int i = 0; i < GameController.Instance.RescueNPCList.Count; i++)
            {
                SaveDataController.Instance.mUser.NPCOpen[GameController.Instance.RescueNPCList[i]] = true;
                MapNPCSlot slot = Instantiate(mNPCSlot, mParents);
                slot.SetData(GameController.Instance.RescueNPCList[i]);
            }
            mNPCWindow.gameObject.SetActive(true);
        }
    }

    public void StageClearGift()
    {
        mOpenItemWindow.gameObject.SetActive(true);
        bool weapon = false; bool skill = false; bool character = false;
        switch (GameSetting.Instance.NowStage)
        {
            case 1:
                break;
            case 2:
                if (SaveDataController.Instance.mUser.CharacterOpen[1]==false&& SaveDataController.Instance.mUser.SkillOpen[1]==false&& SaveDataController.Instance.mUser.WeaponOpen[5] == false && SaveDataController.Instance.mUser.WeaponOpen[9] == false&& SaveDataController.Instance.mUser.WeaponOpen[2]==false && SaveDataController.Instance.mUser.WeaponOpen[24] == false)
                {
                    SaveDataController.Instance.mUser.CharacterOpen[1] = true;
                    SaveDataController.Instance.mUser.SkillOpen[1] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[2] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[5] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[9] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[24] = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 3:
                if (SaveDataController.Instance.mUser.CharacterOpen[3] == false&& SaveDataController.Instance.mUser.SkillOpen[3] ==false&& SaveDataController.Instance.mUser.WeaponOpen[7]==false && SaveDataController.Instance.mUser.WeaponOpen[15] == false && SaveDataController.Instance.mUser.WeaponOpen[16]==false)
                {
                    SaveDataController.Instance.mUser.CharacterOpen[3] = true;
                    SaveDataController.Instance.mUser.SkillOpen[3] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[7] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[15] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[16] = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 4:
                if (SaveDataController.Instance.mUser.CharacterOpen[4] == false && SaveDataController.Instance.mUser.SkillOpen[4] == false && SaveDataController.Instance.mUser.WeaponOpen[10] == false && SaveDataController.Instance.mUser.WeaponOpen[19] == false && SaveDataController.Instance.mUser.WeaponOpen[20] == false && SaveDataController.Instance.mUser.WeaponOpen[23] == false)
                {
                    SaveDataController.Instance.mUser.CharacterOpen[4] = true;
                    SaveDataController.Instance.mUser.SkillOpen[4] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[10] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[19] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[20] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[23] = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 5:
                if (SaveDataController.Instance.mUser.CharacterOpen[5] == false && SaveDataController.Instance.mUser.SkillOpen[5] == false && SaveDataController.Instance.mUser.WeaponOpen[11] == false && SaveDataController.Instance.mUser.WeaponOpen[18] == false && SaveDataController.Instance.mUser.WeaponOpen[21] == false)
                {
                    SaveDataController.Instance.mUser.CharacterOpen[5] = true;
                    SaveDataController.Instance.mUser.SkillOpen[5] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[11] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[18] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[21] = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 6:
                if (SaveDataController.Instance.mUser.CharacterOpen[6] == false && SaveDataController.Instance.mUser.SkillOpen[6] == false && SaveDataController.Instance.mUser.WeaponOpen[17] == false && SaveDataController.Instance.mUser.WeaponOpen[22] == false)
                {
                    SaveDataController.Instance.mUser.CharacterOpen[6] = true;
                    SaveDataController.Instance.mUser.SkillOpen[6] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[17] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[22] = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                SaveDataController.Instance.mUser.GameClear = true;
                break;
        }
        if (GameSetting.Instance.Language == 0)
        {
            if (character == true)
            {
                MessageText.text += "-선택 가능한 캐릭터 종류 증가!\n";
            }
            if (skill == true)
            {
                MessageText.text += "-스킬 상점의 품목 증가!\n";
            }
            if (weapon == true)
            {
                MessageText.text += "-제작 가능한 무기 종류 증가!";
            }
            if (character == false && skill == false && weapon == false)
            {
                MessageText.text = "획득 가능한 보상이 없습니다";
            }
        }
        else if (GameSetting.Instance.Language == 1)
        {
            if (character == true)
            {
                MessageText.text += "-The number of playable character increased!\n";
            }
            if (skill == true)
            {
                MessageText.text += "-Skill shop has increased in skills!\n";
            }
            if (weapon == true)
            {
                MessageText.text += "-Smithy has increased in weapons!\n";
            }
            if (character == false && skill == false && weapon == false)
            {
                MessageText.text = "No reward";
            }
        }
    }

    public void ShowAdsButton()
    {
        AdsWindow.gameObject.SetActive(true);
    }

    public void ShowAds()
    {
        AdsWindow.gameObject.SetActive(false);
        GameSetting.Instance.ShowAds(eAdsReward.DoubleReward);
    }
}
