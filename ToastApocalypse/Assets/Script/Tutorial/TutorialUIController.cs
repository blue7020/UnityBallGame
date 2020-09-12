using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIController : InformationLoader
{

    public static TutorialUIController Instance;

    public Text mHPText, mStatText, mNameText, mbulletText, mLevelText;
    public Image mPlayerImage, mStatPlayerImage, mWeaponImage, mSkillImage, mSkillCoolWheel;
    public Image mitemImage, mArtifactImage, mUsingArtifactImage, ArtifactCoolWheel, mClearImage, mPlayerLookPoint;
    public HPBar mHPBar;

    public Sprite DefaultItemSprite;

    public Button mStatButton, mSKillButton, mItemButton, mArtifactButton, mBGMplus, mBGMminus, mSEplus, mSEminus;
    public Text mTitleText, mTouchGuideText;
    public Text mBGMText, mSEText, mStatTitle, mArtifactTitle, mClearText, mGuideText;
    public Tooltip tooltip;

    public MapText[] mInfoArr;

    public MapText[] GetInfoArr()
    {
        return mInfoArr;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.MAP_TEXT);
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
        mBGMplus.onClick.AddListener(() => { BGMPlus(); });
        mBGMminus.onClick.AddListener(() => { BGMMinus(); });
        mSEplus.onClick.AddListener(() => { SEPlus(); });
        mSEminus.onClick.AddListener(() => { SEMinus(); });
        mItemButton.onClick.AddListener(() => { Player.Instance.ItemUse(); });
        mArtifactButton.onClick.AddListener(() => { Player.Instance.ArtifactUse(); });
        mSKillButton.onClick.AddListener(() => { Player.Instance.NowPlayerSkill.SkillUse(); });
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

    public void ShowDeathWindow()
    {
        //현재 방 번호 기억한 다음 사망한 방 번호의 시작위치에서 다시 시작
    }

    public void ShowClearText()
    {
        GameController.Instance.pause = true;
        Player.Instance.mRB2D.velocity = Vector3.zero;
        Player.Instance.Stun = true;
        if (GameSetting.Instance.Language == 0)
        {//한국어
            mClearText.text = "튜토리얼 클리어!";
            mGuideText.text = "터치 시 로비로 이동합니다.";
        }
        else if (GameSetting.Instance.Language == 1)
        {//영어
            mClearText.text = "Tutorial Clear!";
            mGuideText.text = "Touch to move to the lobby.";
        }
        mClearImage.gameObject.SetActive(true);
        GameClearUI.Instance.StageClear();
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
        mPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[GameSetting.Instance.PlayerID];
        mMinimapPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[GameSetting.Instance.PlayerID];
        mStatPlayerImage.sprite = GameSetting.Instance.mPlayerSpt[GameSetting.Instance.PlayerID];
    }

    public float StatSetting
    {
        set
        {
            mHPText.text = value.ToString();
        }
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
