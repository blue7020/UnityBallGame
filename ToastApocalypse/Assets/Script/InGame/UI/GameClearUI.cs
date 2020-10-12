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
    private int Sequence, PlusRewardMaterial;

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
            //광고 출력
        }
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
        GameSetting.Instance.StageOpen[GameSetting.Instance.NowStage] = true;
        GameSetting.Instance.StagePartsget[GameSetting.Instance.NowStage]=true;
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
        if (GameSetting.Instance.Syrup + GameController.Instance.SyrupInStage >= 99999)
        {
            GameSetting.Instance.Syrup = 99999;
        }
        else
        {
            GameSetting.Instance.Syrup += GameController.Instance.SyrupInStage;
        }
        for (int i=0; i<Sequence;i++)
        {
            if (GameSetting.Instance.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] + PlusRewardMaterial > 99)
            {
                GameSetting.Instance.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] = 99;
            }
            else
            {
                GameSetting.Instance.HasMaterial[StageMaterialController.Instance.mStageMaterialArr[Sequence].mID] += PlusRewardMaterial;
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
                GameSetting.Instance.NPCOpen[GameController.Instance.RescueNPCList[i]] = true;
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
                if (GameSetting.Instance.mWeaponInfoArr[5].Open==false && GameSetting.Instance.mWeaponInfoArr[9].Open==false)
                {
                    GameSetting.Instance.mWeaponInfoArr[5].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[9].Open = true;
                    weapon = true;
                }
                break;
            case 2:
                if (GameSetting.Instance.mPlayerInfoArr[1].Open==false&& GameSetting.Instance.mSkillInfoArr[1].Open==false&& GameSetting.Instance.mWeaponInfoArr[2].Open==false && GameSetting.Instance.mWeaponInfoArr[24].Open == false)
                {
                    GameSetting.Instance.mPlayerInfoArr[1].Open = true;
                    GameSetting.Instance.mSkillInfoArr[1].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[2].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[24].Open = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 3:
                if (GameSetting.Instance.mPlayerInfoArr[3].Open == false&& GameSetting.Instance.mSkillInfoArr[3].Open ==false&& GameSetting.Instance.mWeaponInfoArr[7].Open==false && GameSetting.Instance.mWeaponInfoArr[15].Open == false && GameSetting.Instance.mWeaponInfoArr[16].Open==false)
                {
                    GameSetting.Instance.mPlayerInfoArr[3].Open = true;
                    GameSetting.Instance.mSkillInfoArr[3].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[7].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[15].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[16].Open = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 4:
                if (GameSetting.Instance.mPlayerInfoArr[4].Open == false && GameSetting.Instance.mSkillInfoArr[4].Open == false && GameSetting.Instance.mWeaponInfoArr[10].Open == false && GameSetting.Instance.mWeaponInfoArr[19].Open == false && GameSetting.Instance.mWeaponInfoArr[20].Open == false && GameSetting.Instance.mWeaponInfoArr[23].Open == false)
                {
                    GameSetting.Instance.mPlayerInfoArr[4].Open = true;
                    GameSetting.Instance.mSkillInfoArr[4].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[10].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[19].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[20].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[23].Open = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 5:
                if (GameSetting.Instance.mPlayerInfoArr[5].Open == false && GameSetting.Instance.mSkillInfoArr[5].Open == false && GameSetting.Instance.mWeaponInfoArr[11].Open == false && GameSetting.Instance.mWeaponInfoArr[18].Open == false && GameSetting.Instance.mWeaponInfoArr[21].Open == false)
                {
                    GameSetting.Instance.mPlayerInfoArr[5].Open = true;
                    GameSetting.Instance.mSkillInfoArr[5].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[11].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[18].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[21].Open = true;
                    character = true;
                    skill = true;
                    weapon = true;
                }
                break;
            case 6:
                if (GameSetting.Instance.mPlayerInfoArr[6].Open == false && GameSetting.Instance.mSkillInfoArr[6].Open == false && GameSetting.Instance.mWeaponInfoArr[17].Open == false && GameSetting.Instance.mWeaponInfoArr[22].Open == false)
                {
                    GameSetting.Instance.mPlayerInfoArr[6].Open = true;
                    GameSetting.Instance.mSkillInfoArr[6].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[17].Open = true;
                    GameSetting.Instance.mWeaponInfoArr[22].Open = true;
                    character = true;
                    skill = true;
                    weapon = true;
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
        //광고 출력
        //광고 출력 후 아래 출력
        //SoundController.Instance.SESoundUI(6);
        //GameController.Instance.SyrupInStage *= 2;
        //PlusRewardMaterial+=1;
        //if (GameSetting.Instance.Language == 0)
        //{//한국어
        //    UIController.Instance.mSyrupText.text = "획득한 시럽: +<color=#FFFF00>" + GameController.Instance.SyrupInStage+"</color>";
        //    UIController.Instance.mItemText.text = "획득한 재료 x2";
        //}
        //else if (GameSetting.Instance.Language == 1)
        //{//영어
        //    UIController.Instance.mSyrupText.text = "Syrup: +<color=#FFFF00>" + GameController.Instance.SyrupInStage;
        //    UIController.Instance.mItemText.text = "Material x2";
        //}
    }
}
