using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIController : InformationLoader
{

    public static UIController Instance;

    public Text mGoldText, mHPText,mStatText, mNameText, mbulletText,mLevelText,mAirText,mAirGaugeText,mShopSpendText,mStatueSpendText;
    public Image mPlayerImage, mStatPlayerImage, mMinimapPlayerImage, mWeaponImage, mSkillImage, mSkillCoolWheel, NoTouchArea, mShadow;
    public Image mitemImage, mArtifactImage, mUsingArtifactImage,ArtifactCoolWheel,mClearImage,mPlayerLookPoint,mAirGauge,mPieceImage,mMonsterImage,mDeathWindow,mDeathUI,mReviveWindow;
    public HPBar mAirBar, mHPBar;

    public Sprite DefaultItemSprite;

    public Button mStatButton, mSKillButton,mItemButton, mArtifactButton,mBGMplus, mBGMminus, mSEplus, mSEminus,mPortalButton,mReviveAdButton,mReviveSyrupButton,mWeaponChangeButton,mShopButton,mStatueButton, mUIChestButton;

    public RawImage mMiniMapImage;
    public Toggle mMiniMapButton;
    public Text mTitleText, mTouchGuideText;
    public Text mBGMText, mSEText, mStatTitle, mArtifactTitle, mClearText,mSyrupText,mItemText,mGuideText, YesText,NoText, WarningText,mReviveTitle, mReviveSyrup,mSyrupButtonText,mReviveText;
    public string maptext, bufftext;
    private TextEffect effect;
    public Tooltip tooltip;
    public MapText[] mInfoArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (GameSetting.Instance.Language == 0)
            {
                YesText.text = "<color=#FE2E2E>예</color>";
                NoText.text = "아니오";
                WarningText.text = "메인 로비로 돌아가시겠습니까?\n<color=#FE2E2E><size=80>(현재 스테이지가 저장되지 않습니다!)</size></color>";
                mReviveTitle.text = "부활하기";
                mReviveSyrup.text = "현재 시럽: "+ SaveDataController.Instance.mUser.Syrup.ToString();
                mSyrupButtonText.text = GameSetting.Instance.ReviveSyrup+"\n시럽";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                YesText.text = "<color=#FE2E2E>YES</color>";
                NoText.text = "NO";
                WarningText.text = "Do you want to return to the main lobby?\n<color=#FE2E2E><size=80>(This stage is not save!)</size></color>";
                mReviveTitle.text = "Revive";
                mReviveSyrup.text = "Now Syrup: " + SaveDataController.Instance.mUser.Syrup.ToString();
                mSyrupButtonText.text = GameSetting.Instance.ReviveSyrup + "\nSyrup";
            }
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        StartCoroutine(SceneMoveShadow());
        if (GameSetting.Instance.Language == 0)//한국어
        {
            maptext = GameSetting.Instance.mMapInfoArr[GameSetting.Instance.NowStage].Title;
            mAirText.text = "공기";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            maptext = GameSetting.Instance.mMapInfoArr[GameSetting.Instance.NowStage].EngTitle;
            mAirText.text = "AIR";
        }
        if (GameSetting.Instance.NowStage == 4)
        {
            mAirGauge.gameObject.SetActive(true);
        }
        StartCoroutine(ShowLevel());
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
        mBGMplus.onClick.AddListener(() => { BGMPlus(); });
        mBGMminus.onClick.AddListener(() => { BGMMinus(); });
        mSEplus.onClick.AddListener(() => { SEPlus(); });
        mSEminus.onClick.AddListener(() => { SEMinus(); });
        mItemButton.onClick.AddListener(() => { Player.Instance.ItemUse(); });
        mArtifactButton.onClick.AddListener(() => { Player.Instance.ArtifactUse(); });
        mSKillButton.onClick.AddListener(() => { Player.Instance.NowPlayerSkill.SkillUse(); });
        ReviveUI();
    }

    public IEnumerator SceneMoveShadow()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mShadow.gameObject.SetActive(true);
        yield return delay;
        mShadow.gameObject.SetActive(false);
    }
    public void ButtonPush()
    {
        SoundController.Instance.SESoundUI(0);
    }

    public void ReviveUI()
    {
        if (GameSetting.Instance.Language == 0)
        {
            mReviveText.text = "광고는 현재 스테이지가 끝나면 실행됩니다";
        }
        else if (GameSetting.Instance.Language==1)
        {
            mReviveText.text = "Ads show after now stage end";
        }
        if (GameController.Instance.ReviveCode >0)
        {
            mReviveAdButton.interactable = false;
            mReviveSyrupButton.interactable = false;
        }
    }
    public void ReviveWindow(int id)
    {
        if (GameController.Instance.ReviveCode ==0)
        {
            switch (id)
            {
                case 0:
                    GameController.Instance.GamePause();
                    StartCoroutine(Player.Instance.Revive());
                    GameController.Instance.ReviveCode = 1;
                    break;
                case 1:
                    if (SaveDataController.Instance.mUser.Syrup >= 200)
                    {
                        SaveDataController.Instance.mUser.Syrup -= 200;
                        GameController.Instance.GamePause();
                        StartCoroutine(Player.Instance.Revive());
                        GameController.Instance.ReviveCode = 2;
                    }
                    break;
            }
        }
        ReviveUI();
    }

    public void GameOver()
    {
        Player.Instance.GameOver();
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

    public IEnumerator ShowLevel()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);
        mLevelText.text = maptext+"\n-B" + GameController.Instance.StageLevel + "F-";
        mLevelText.gameObject.SetActive(true);
        yield return delay;
        mLevelText.gameObject.SetActive(false);
        if (GameSetting.Instance.NowStage == 6&& GameController.Instance.StageLevel==1)
        {
            if (SaveDataController.Instance.mUser.FirstGameClearEvent == false)
            {
                StartCoroutine(ShowLevelMessage());
            }
        }
    }
    public IEnumerator ShowLevelMessage()
    {
        WaitForSeconds delay = new WaitForSeconds(3.3f);
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mLevelText.text = "<size=100>\'눅눅함의 저주\'로 인해 능력치가 감소했습니다:\n</size><size=80><color=#2EFE2E>최대 체력, 공격 속도, 이동 속도</color></size>";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mLevelText.text = "<size=85>Due to a \'curse of sogginess\', the following stats have been decreased:\n</size><size=80><color=#2EFE2E>Max HP, Attack Spd, Movement Spd</color></size>";
        }
        mLevelText.gameObject.SetActive(true);
        yield return delay;
        mLevelText.gameObject.SetActive(false);
    }

    public IEnumerator ShowBossLevel()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mLevelText.text = "<size=85><color=#E3242B>보스 출현!</color></size>";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mLevelText.text = "<size=85><color=#E3242B>Boss Appears!</color></size>";
        }
        mLevelText.gameObject.SetActive(true);
        yield return delay;
        mLevelText.gameObject.SetActive(false);
    }

    public void ShowDeathWindow()
    {
        if (Player.Instance.LastHitEnemy!=null)
        {
            if (GameSetting.Instance.Language == 0)
            {//한국어
                mTitleText.text = Player.Instance.LastHitEnemy.mStats.Name+"에 의해 사망";
                mTouchGuideText.text = "터치 시 로비로 이동합니다.";
            }
            else if (GameSetting.Instance.Language == 1)
            {//영어
                mTitleText.text = "Death by "+ Player.Instance.LastHitEnemy.mStats.EngName;
                mTouchGuideText.text = "Touch to move to the lobby.";
            }
            mMonsterImage.sprite = Player.Instance.LastHitEnemy.UISprite;
        }
        else
        {
            if (Player.Instance.DeathBy==1)
            {
                if (GameSetting.Instance.Language == 0)
                {//한국어
                    mTitleText.text = "부주의한 무기 사용으로 사망";
                    mTouchGuideText.text = "터치 시 로비로 이동합니다.";
                }
                else if (GameSetting.Instance.Language == 1)
                {//영어
                    mTitleText.text = "Death by careless use of weapon";
                    mTouchGuideText.text = "Touch to move to the lobby.";
                }
            }
            else if (Player.Instance.DeathBy == 2)
            {
                if (GameSetting.Instance.Language == 0)
                {//한국어
                    mTitleText.text = "함정에 걸려 사망";
                    mTouchGuideText.text = "터치 시 로비로 이동합니다.";
                }
                else if (GameSetting.Instance.Language == 1)
                {//영어
                    mTitleText.text = "Death by trapped";
                    mTouchGuideText.text = "Touch to move to the lobby.";
                }
            }
            else if (Player.Instance.DeathBy == 3)
            {
                if (GameSetting.Instance.Language == 0)
                {//한국어
                    mTitleText.text = "익사";
                    mTouchGuideText.text = "터치 시 로비로 이동합니다.";
                }
                else if (GameSetting.Instance.Language == 1)
                {//영어
                    mTitleText.text = "Drowned";
                    mTouchGuideText.text = "Touch to move to the lobby.";
                }
            }
        }
        mDeathWindow.gameObject.SetActive(true);
    }

    public void ShowGetArtifact(ArtifactTextStat art)
    {
        effect = null;
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "유물 획득: "+art.Title;
        }
        else
        {
            bufftext = "Get Artifact: "+art.EngTitle;
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.transform.position = new Vector3(0, 174.5f, 0);
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
        effect.transform.position = new Vector3(0, 174.5f, 0);
        effect.SetText(bufftext);
    }

    public void ShowClearText()
    {
        GameController.Instance.pause = true;
        Player.Instance.mRB2D.velocity = Vector3.zero;
        Player.Instance.Stun = true;
        if (GameSetting.Instance.Language == 0)
        {//한국어
            mClearText.text = GameSetting.Instance.NowStage + "스테이지 클리어!";
            mSyrupText.text= "획득한 시럽: +" + GameController.Instance.SyrupInStage;
            mItemText.text = "획득한 재료";
            mGuideText.text = "터치 시 로비로 이동합니다.";
        }
        else if (GameSetting.Instance.Language == 1)
        {//영어
            mClearText.text = GameSetting.Instance.NowStage+ "Stage Clear!";
            mSyrupText.text = "Syrup: +" + GameController.Instance.SyrupInStage;
            mItemText.text = "Material";
            mGuideText.text = "Touch to move to the lobby.";
        }
        mPieceImage.sprite = GameSetting.Instance.mParts[GameSetting.Instance.NowStage];
        mClearImage.gameObject.SetActive(true);
        GameClearUI.Instance.StageClear();
    }

    public void ShowItemImage()
    {
        if (Player.Instance.NowItem==null)
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
        if (Player.Instance.NowPlayerWeapon==null)
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
        mPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[GameSetting.Instance.PlayerID];
        mMinimapPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[GameSetting.Instance.PlayerID];
        mStatPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[GameSetting.Instance.PlayerID];
    }

    public void ShowAirGaugeBar()
    {
        mAirGaugeText.text = Player.Instance.mCurrentAir+"/ "+Player.MAX_AIR;
        mAirBar.ShowAirBar();
    }

    public void ShowMiniMap(bool ButtonOn)
    {
        if (ButtonOn)
        {
            mMiniMapImage.gameObject.SetActive(true);
            MiniMap.Instance.MinimapOn = true;
        }
        else
        {
            mMiniMapImage.gameObject.SetActive(false);
            MiniMap.Instance.MinimapOn = false;
        }
    }

    public float StatSetting
    {
        set
        {
            mGoldText.text = value.ToString();
            mHPText.text = value.ToString();
        }
    }

    public void ShowGold()
    {
        mGoldText.text = Player.Instance.mStats.Gold.ToString();
    }

    public void ShowHP()
    {
        mHPBar.ShowHPBar();
        string HP = string.Format("{0} / {1}", Player.Instance.mCurrentHP.ToString("N1"), Player.Instance.mMaxHP.ToString("N1"));
        mHPText.text = HP;
    }

    public void ShowStat()
    {
        float Crit = 0;
        if (Player.Instance.mStats.Crit+ Player.Instance.buffIncrease[5]>=1)
        {
            Crit = 1;
        }
        else
        {
            Crit = Player.Instance.mStats.Crit + Player.Instance.buffIncrease[5];
        }
        float CCreduce = 0;
        if (Player.Instance.NoCC==true)
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
                                      (Player.Instance.mStats.Spd * (1 + Player.Instance.buffIncrease[3])).ToString("N1"), Crit.ToString("P1"),
                                      "1"+Player.Instance.mStats.CritDamage.ToString("P1"),
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
                                      (Player.Instance.mStats.Def *(1+Player.Instance.buffIncrease[1])).ToString("N1"), (Player.Instance.mStats.AtkSpd / (1 + Player.Instance.AttackSpeedStat + Player.Instance.buffIncrease[2])).ToString("N2"),
                                      (Player.Instance.mStats.Spd * (1 + Player.Instance.buffIncrease[3])).ToString("N1"), Crit.ToString("P1"),
                                      "1"+Player.Instance.mStats.CritDamage.ToString("P1"),
                                      CooltimeReduce.ToString("P0"), CCreduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = Name;
        }
    }
}
