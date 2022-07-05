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
    public Transform mParents, mRewardMaterialParents;
    public int PlusRewardMaterial;

    public MaterialSlot mMaterialSlot;

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

    public void SetReward()
    {
        for (int i = 0; i < GameController.Instance.mRewardMaterialList.Count; i++)
        {
            MaterialSlot slot = Instantiate(mMaterialSlot, mRewardMaterialParents);
            slot.SetData(GameController.Instance.mRewardMaterialList[i]);
            slot.HideAmount();
        }
    }

    public void StageClear()
    {
        if (GameController.Instance.ReviveCode == 1)
        {
            RewardAdsManager.Instance.ShowRewardAd(2);
        }
        SetReward();
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
        else if(GameSetting.Instance.NowStage == 6)
        {
            UIController.Instance.mPieceImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
            UIController.Instance.mPieceImage.gameObject.SetActive(true);
            ShowNPCWindow();
            StageClearGift();
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
        GameSetting.Instance.GetSyrup(GameController.Instance.SyrupInStage);
        GetStageReward();
        gameObject.SetActive(false);
        GameController.Instance.MainMenu();
    }

    public void GetStageReward()
    {
        for (int i = 0; i < GameController.Instance.mRewardMaterialList.Count; i++)
        {
            if (SaveDataController.Instance.mUser.HasMaterial[GameController.Instance.mRewardMaterialList[i]] + PlusRewardMaterial >= Constants.MAX_MATERIAL)
            {
                SaveDataController.Instance.mUser.HasMaterial[GameController.Instance.mRewardMaterialList[i]] = Constants.MAX_MATERIAL;
            }
            else
            {
                SaveDataController.Instance.mUser.HasMaterial[GameController.Instance.mRewardMaterialList[i]] += PlusRewardMaterial;
            }
        }
        Debug.Log("보상 획득");
        SaveDataController.Instance.Save();
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
        bool weapon = false; bool skill = false; bool character = false; bool furniture = false; bool item =false;
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
                if (SaveDataController.Instance.mUser.GameClear == false)
                {
                    furniture = true;
                }
                SaveDataController.Instance.mUser.GameClear = true;
                break;
            case 7:
                if (SaveDataController.Instance.mUser.CharacterOpen[13] == false && SaveDataController.Instance.mUser.SkillOpen[13] == false && SaveDataController.Instance.mUser.WeaponOpen[26] == false
                    && SaveDataController.Instance.mUser.WeaponOpen[27] == false && SaveDataController.Instance.mUser.WeaponOpen[28] == false && SaveDataController.Instance.mUser.WeaponOpen[29] == false)
                {
                    SaveDataController.Instance.mUser.CharacterOpen[13] = true;
                    SaveDataController.Instance.mUser.SkillOpen[13] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[26] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[27] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[28] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[29] = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                if (SaveDataController.Instance.mUser.ItemOpen[10]==false)
                {
                    SaveDataController.Instance.mUser.ItemOpen[10] = true;
                    item = true;
                }
                break;
            case 8:
                if (SaveDataController.Instance.mUser.CharacterOpen[14] == false && SaveDataController.Instance.mUser.SkillOpen[14] == false && SaveDataController.Instance.mUser.WeaponOpen[30] == false
                    && SaveDataController.Instance.mUser.WeaponOpen[31] == false && SaveDataController.Instance.mUser.WeaponOpen[32] == false && SaveDataController.Instance.mUser.WeaponOpen[33] == false)
                {
                    SaveDataController.Instance.mUser.CharacterOpen[14] = true;
                    SaveDataController.Instance.mUser.SkillOpen[14] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[30] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[31] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[32] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[33] = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                if (SaveDataController.Instance.mUser.ItemOpen[11] == false)
                {
                    SaveDataController.Instance.mUser.ItemOpen[11] = true;
                    item = true;
                }
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
            if (weapon == true && SaveDataController.Instance.mUser.NPCOpen[2])
            {
                MessageText.text += "-제작 가능한 무기 종류 증가!\n";
            }
            if (item == true&&SaveDataController.Instance.mUser.NPCOpen[8])
            {
                MessageText.text += "-선술집의 품목 증가!\n";
            }
            if (furniture == true)
            {
                MessageText.text += "<color=#FFFF00>-로비에 가구가 추가되었습니다!</color>\n";
            }
            if (character == false && skill == false && weapon == false&& furniture==false)
            {
                MessageText.text = "획득 가능한 추가 보상이 없습니다";
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
            if (weapon == true && SaveDataController.Instance.mUser.NPCOpen[2])
            {
                MessageText.text += "-Smithy has increased in weapons!\n";
            }
            if (item == true && SaveDataController.Instance.mUser.NPCOpen[8])
            {
                MessageText.text += "-Pub has increased in items!\n";
            }
            if (furniture == true)
            {
                MessageText.text += "<color=#FFFF00>-Furniture added in the lobby!</color>\n";
            }
            if (character == false && skill == false && weapon == false&& furniture == false)
            {
                MessageText.text = "No additional reward";
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
        RewardAdsManager.Instance.ShowRewardAd(3);
    }
}
