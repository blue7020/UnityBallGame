﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIController : InformationLoader
{

    public static TutorialUIController Instance;

    public Text mHPText, mNameText, mbulletText, mStatText, mSkillButtonText, mItemButtonText, mArtifactButtonText, mAttackPadText, mWeaponChangeButtonText,mStatusButtonText, mStatueSpendText;
    public Image mPlayerImage, mStatPlayerImage, mWeaponImage, mSkillImage, mSkillCoolWheel, mShadow;
    public Image mitemImage, mArtifactImage, mUsingArtifactImage, ArtifactCoolWheel, mClearImage, mPlayerLookPoint, mPieceImage,mWarningWindow;
    public HPBar mHPBar;
    public TutorialDialog Dialog;
    public Sprite DefaultItemSprite;

    public Button mStatButton, mSKillButton, mItemButton, mArtifactButton, mBGMplus, mBGMminus, mSEplus, mSEminus,MainMenuButton,StartMenuButton,YesButton, mWeaponChangeButton,mStatueButton;
    public Text mBGMText, mSEText, mStatTitle, mArtifactTitle,YesText,NoText, WarningText;
    public Tooltip tooltip;
    public PlayerSkill mPlayerSkill;
    public Enemy[] mEnemy;
    public Transform[] mSpawnPos;
    public int TutorialStatueCheck;
    public MapText[] mInfoArr;
    public string bufftext;
    private TextEffect effect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.MAP_TEXT);
            if (SaveDataController.Instance.mUser.TutorialEnd==false)
            {
                StartMenuButton.gameObject.SetActive(true);
                MainMenuButton.gameObject.SetActive(false);
                if (GameSetting.Instance.Language == 0)
                {
                    YesText.text = "<color=#FE2E2E>예</color>";
                    NoText.text = "아니오";
                    WarningText.text = "<color=#FE2E2E><size=80>튜토리얼을 스킵하시겠습니까?</size></color>";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    YesText.text = "<color=#FE2E2E>YES</color>";
                    NoText.text = "NO";
                    WarningText.text = "<color=#FE2E2E><size=80>Skip the tutorial?</size></color>";
                }
                YesButton.onClick.AddListener(() => { GameController.Instance.TutorialSkip(); });
            }
            else
            {
                if (GameSetting.Instance.Language == 0)
                {
                    YesText.text = "<color=#FE2E2E>예</color>";
                    NoText.text = "아니오";
                    WarningText.text = "메인 로비로 돌아가시겠습니까?\n<color=#FE2E2E><size=80>(현재 스테이지가 저장되지 않습니다!)</size></color>";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    YesText.text = "<color=#FE2E2E>YES</color>";
                    NoText.text = "NO";
                    WarningText.text = "Do you want to return to the main lobby?\n<color=#FE2E2E><size=80>(This stage is not save!)</size></color>";
                }
                YesButton.onClick.AddListener(() => { GameController.Instance.MainMenu(); });
            }
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        if (GameSetting.Instance.Language == 0)
        {
            mSkillButtonText.text = "스킬";
            mItemButtonText.text = "아이템";
            mArtifactButtonText.text = "사용 유물";
            mAttackPadText.text = "공격";
            mWeaponChangeButtonText.text = "무기 교체";
            mStatusButtonText.text = "능력치";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mSkillButtonText.text = "Skill";
            mItemButtonText.text = "Item";
            mArtifactButtonText.text = "Active Artifact";
            mAttackPadText.text = "Attack";
            mWeaponChangeButtonText.text = "Weapon Change";
            mStatusButtonText.text = "Status";
        }
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
        mBGMplus.onClick.AddListener(() => { BGMPlus(); });
        mBGMminus.onClick.AddListener(() => { BGMMinus(); });
        mSEplus.onClick.AddListener(() => { SEPlus(); });
        mSEminus.onClick.AddListener(() => { SEMinus(); });
        mItemButton.onClick.AddListener(() => { Player.Instance.ItemUse(); });
        mArtifactButton.onClick.AddListener(() => { Player.Instance.ArtifactUse(); });
        mSKillButton.onClick.AddListener(() => { Player.Instance.NowPlayerSkill.SkillUse(); });
        CharacterImage();
        for (int i=0; i<3;i++)
        {
            Instantiate(mEnemy[i], mSpawnPos[i]);
        }
        Dialog.gameObject.SetActive(true);
        Time.timeScale = 0;
        Dialog.ShowDialog();
        StartCoroutine(SceneMoveShadow());
    }

    public void ButtonPush()
    {
        SoundController.Instance.SESoundUI(0);
    }

    public IEnumerator SceneMoveShadow()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
        mShadow.gameObject.SetActive(true);
        yield return delay;
        TutorialDialog.Instance.StartCoroutine(TutorialDialog.Instance.Delay());
        mShadow.gameObject.SetActive(false);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void BGMPlus()
    {
        SoundController.Instance.PlusBGM();
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
    }
    public void BGMMinus()
    {
        SoundController.Instance.MinusBGM();
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
    }

    public void SEPlus()
    {
        SoundController.Instance.PlusSE();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
    }
    public void SEMinus()
    {
        SoundController.Instance.MinusSE();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
    }

    public void ShowItemImage()
    {
        if (Player.Instance.NowItem == null)
        {
            mitemImage.sprite = DefaultItemSprite;
        }
        else
        {
            mitemImage.sprite = Player.Instance.NowItem.mRenderer.sprite;
        }
    }
    public void ShowArtifactImage()
    {
        if (Player.Instance.NowActiveArtifact == null)
        {
            mArtifactImage.sprite = DefaultItemSprite;
        }
        else
        {
            mArtifactImage.sprite = Player.Instance.NowActiveArtifact.mRenderer.sprite;
        }
    }

    public void ShowSkillImage()
    {
        mSkillImage.sprite = Player.Instance.NowPlayerSkill.mSkillIcon;
    }


    public void ShowWeaponImage()
    {
        if (Player.Instance.NowPlayerWeapon == null)
        {
            mWeaponImage.sprite = DefaultItemSprite;
        }
        else
        {
            mWeaponImage.sprite = Player.Instance.NowPlayerWeapon.mRenderer.sprite;
        }
    }
    public void ShowNowBulletText()
    {
        if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Range || Player.Instance.NowPlayerWeapon.eType == eWeaponType.Fire)
        {
            mbulletText.gameObject.SetActive(true);
            mbulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
        }
        else if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Melee)
        {
            mbulletText.gameObject.SetActive(false);
        }
    }

    public void CharacterImage()
    {
        mPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[0];
        mStatPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[0];
    }

    public float StatSetting
    {
        set
        {
            mHPText.text = value.ToString();
        }
    }

    public void ShowGetArtifact(ArtifactTextStat art)
    {
        effect = null;
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "유물 획득: " + art.Title;
        }
        else
        {
            bufftext = "Get Artifact: " + art.EngTitle;
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
    }

    public void ShowGetItem(ItemStat item)
    {
        effect = null;
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "아이템 획득: " + item.Name;
        }
        else
        {
            bufftext = "Get Item: " + item.EngName;
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
    }

    public void ShowHP()
    {
        mHPBar.ShowHPBar();
        string HP = string.Format("{0} / {1}", Player.Instance.mCurrentHP.ToString("N1"), Player.Instance.mMaxHP.ToString("N1"));
        mHPText.text = HP;
    }

    public void ShowStat()
    {
        float CCreduce = 0;
        if (Player.Instance.NoCC == true)
        {
            CCreduce = 1f;
        }
        else
        {
            if (Player.Instance.mStats.CCReduce * (1 + Player.Instance.buffIncrease[6]) >= 0.5f)
            {
                CCreduce = 0.5f;
            }
            else
            {
                CCreduce = Player.Instance.mStats.CCReduce * (1 + Player.Instance.buffIncrease[6]);
            }
        }
        float CooltimeReduce = 0;
        if (Player.Instance.mStats.CooltimeReduce >= 0.5f)
        {
            CooltimeReduce = 0.5f;
        }
        else
        {
            CooltimeReduce = Player.Instance.mStats.CooltimeReduce;
        }
        if (GameSetting.Instance.Language == 0)
        {//한국어
            mStatTitle.text = "캐릭터정보";
            mArtifactTitle.text = "유물";
            string Name = Player.Instance.mStats.Name;
            string Stat = string.Format("체력: {0} / {1}\n" +
                                      "공격력: {2}\n" +
                                      "방어력: {3}\n" +
                                      "공격속도: {4}\n" +
                                      "이동속도: {5}\n" +
                                      "치명타 확률: {6}\n" +
                                      "치명타 피해: {7}\n" +
                                      "\n" +
                                      "쿨타임 감소: {8}\n" +
                                      "상태이상 저항: {9}", Player.Instance.mCurrentHP.ToString("N1"), Player.Instance.mMaxHP.ToString("N1"),
                                      (Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])).ToString("N1"),
                                      (Player.Instance.mStats.Def * (1 + Player.Instance.buffIncrease[1])).ToString("N1"), (Player.Instance.mStats.AtkSpd / (1 + Player.Instance.AttackSpeedStat + Player.Instance.buffIncrease[2])).ToString("N2"),
                                      (Player.Instance.mStats.Spd * (1 + Player.Instance.buffIncrease[3])).ToString("N1"), Player.Instance.mStats.Crit.ToString("P1"),
                                      Player.Instance.mStats.CritDamage.ToString("P1"),
                                      CooltimeReduce.ToString("P0"), CCreduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = Name;
        }
        else if (GameSetting.Instance.Language == 1)
        {//영어
            mStatTitle.text = "Stat";
            mArtifactTitle.text = "Artifact";
            string Name = Player.Instance.mStats.EngName;
            string Stat = string.Format("HP: {0} / {1}\n" +
                                      "Atk: {2}\n" +
                                      "Def: {3}\n" +
                                      "Atk Spd: {4}\n" +
                                      "Movement Spd: {5}\n" +
                                      "Crit: {6}\n" +
                                      "Crit Damage: {7}\n" +
                                      "\n" +
                                      "Cooldown reduce: {8}\n" +
                                      "Resistance: {9}", Player.Instance.mCurrentHP.ToString("N1"), Player.Instance.mMaxHP.ToString("N1"),
                                      (Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])).ToString("N1"),
                                      (Player.Instance.mStats.Def * (1 + Player.Instance.buffIncrease[1])).ToString("N1"), (Player.Instance.mStats.AtkSpd / (1 + Player.Instance.AttackSpeedStat + Player.Instance.buffIncrease[2])).ToString("N2"),
                                      (Player.Instance.mStats.Spd * (1 + Player.Instance.buffIncrease[3])).ToString("N1"), Player.Instance.mStats.Crit.ToString("P1"),
                                      Player.Instance.mStats.CritDamage.ToString("P1"),
                                      CooltimeReduce.ToString("P0"), CCreduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = Name;
        }
    }
}
