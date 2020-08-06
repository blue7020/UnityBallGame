using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

    public Text mGoldText, mHPText,mStatText, mNameText, bulletText,mLevelText;
    public Image mPlayerImage, mMinimapPlayerImage,mWeaponImage,mSkillImage, SkillCoolWheel;
    public Image mitemImage, mArtifactImage, mUsingArtifactImage,ArtifactCoolWheel,mClearImage,mPlayerLookPoint;

    public Sprite[] mCharacterSprite;
    public Sprite DefaultItemSprite;

    public Button mStatButton, mSKillButton,mItemButton, mArtifactButton;

    public RawImage mMiniMapImage;
    public Toggle mMiniMapButton;
    public Text mBGMText, mSEText, mStatTitle, mArtifactTitle, mClearText,mGuideText;
    public Tooltip tooltip;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        StartCoroutine(ShowLevel());
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
        if (GameSetting.Instance.BGMSetting < 10)
        {
            GameSetting.Instance.BGMSetting++;
        }
        else
        {
            GameSetting.Instance.BGMSetting = 10;
        }
        mBGMText.text = GameSetting.Instance.BGMSetting.ToString();
    }
    public void BGMMinus()
    {
        if (GameSetting.Instance.BGMSetting > 0)
        {
            GameSetting.Instance.BGMSetting--;
        }
        else
        {
            GameSetting.Instance.BGMSetting = 0;
        }
        mBGMText.text = GameSetting.Instance.BGMSetting.ToString();
    }
    public void SEPlus()
    {
        if (GameSetting.Instance.SESetting < 10)
        {
            GameSetting.Instance.SESetting++;
        }
        else
        {
            GameSetting.Instance.SESetting = 10;
        }
        mSEText.text = GameSetting.Instance.SESetting.ToString();
    }
    public void SEMinus()
    {
        if (GameSetting.Instance.SESetting > 0)
        {
            GameSetting.Instance.SESetting--;
        }
        else
        {
            GameSetting.Instance.SESetting = 0;
        }
        mSEText.text = GameSetting.Instance.SESetting.ToString();
    }

    public IEnumerator ShowLevel()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);
        mLevelText.text = "-B" + GameController.Instance.Level + "F-";
        mLevelText.gameObject.SetActive(true);
        yield return delay;
        mLevelText.gameObject.SetActive(false);
    }

    public void ShowClearText()
    {
        GameController.Instance.pause = true;
        Time.timeScale = 0;
        if (GameSetting.Instance.Language == 0)
        {//한국어
            mClearText.text = GameSetting.Instance.NowStage + "스테이지 클리어!\n\n획득한 시럽: +"+GameController.Instance.SyrupInStage;
            mGuideText.text = "터치 시 로비로 이동합니다.";
        }
        else if (GameSetting.Instance.Language == 1)
        {//영어
            mClearText.text = GameSetting.Instance.NowStage+ "Stage Clear!\n\nSyrup: +" + GameController.Instance.SyrupInStage;
            mGuideText.text = "Touch to move to the lobby.";
        }
        mClearImage.gameObject.SetActive(true);
        GameSetting.Instance.Syrup += GameController.Instance.SyrupInStage;
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
        if (Player.Instance.NowUsingArtifact == null)
        {
            mArtifactImage.sprite = DefaultItemSprite;
        }
        else
        {
            mArtifactImage.sprite = Player.Instance.NowUsingArtifact.mRenderer.sprite;
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
        if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Range)
        {
            bulletText.gameObject.SetActive(true);
            bulletText.text = Player.Instance.NowPlayerWeapon.nowBullet.ToString();
        }
        else if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Melee)
        {
            bulletText.gameObject.SetActive(false);
        }
    }

    public void CharacterImage()
    {
        switch (GameSetting.Instance.PlayerID)
        {
            case 0:
                mPlayerImage.sprite = mCharacterSprite[0];
                mMinimapPlayerImage.sprite = mCharacterSprite[0];
                break;
            case 1:
                mPlayerImage.sprite = mCharacterSprite[1];
                mMinimapPlayerImage.sprite = mCharacterSprite[1];
                break;
            default:
                Debug.LogError("Wrong Character Image");
                break;
        }
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
        HPBar.Instance.ShowHPBar();
        string HP = string.Format("{0} / {1}", Player.Instance.mCurrentHP.ToString("N1"), Player.Instance.mMaxHP.ToString("N1"));
        mHPText.text = HP;
    }

    public void ShowStat()
    {

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
                                      Player.Instance.mStats.Atk.ToString("N1"),
                                      Player.Instance.mStats.Def.ToString("N1"), Player.Instance.mStats.AtkSpd.ToString("N2"),
                                      Player.Instance.mStats.Spd.ToString("N1"), Player.Instance.mStats.Crit.ToString("P1"),
                                      Player.Instance.mStats.CritDamage.ToString("P1"),
                                      Player.Instance.mStats.CooltimeReduce.ToString("P0"), Player.Instance.mStats.CCReduce.ToString("P0"));
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
                                      Player.Instance.mStats.Atk.ToString("N1"),
                                      Player.Instance.mStats.Def.ToString("N1"), Player.Instance.mStats.AtkSpd.ToString("N2"),
                                      Player.Instance.mStats.Spd.ToString("N1"), Player.Instance.mStats.Crit.ToString("P1"),
                                      Player.Instance.mStats.CritDamage.ToString("P1"),
                                      Player.Instance.mStats.CooltimeReduce.ToString("P0"), Player.Instance.mStats.CCReduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = Name;
        }
    }
}
