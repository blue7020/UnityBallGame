using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

    [SerializeField]
    private Text mGoldText;
    [SerializeField]
    private Text mHPText;
    [SerializeField]
    private Text mStatText, mNameText;
    [SerializeField]
    public Image mPlayerImage, mMinimapPlayerImage;
    [SerializeField]
    public Image mitemImage, mArtifactImage, mUsingArtifactImage;

    [SerializeField]
    public Sprite[] mCharacterSprite;
    [SerializeField]
    private Sprite[] mSkillSprite;
    [SerializeField]
    public Sprite DefaultItemSprite;

    [SerializeField]
    private Button mStatButton;
    [SerializeField]
    public Button mSKillButton;
    [SerializeField]
    private Button mAttackButton;
    [SerializeField]
    public Button mItemButton, mArtifactButton;

    [SerializeField]
    private RawImage mMiniMapCamera;
    [SerializeField]
    private Toggle mMiniMapButton;
    [SerializeField]
    private Text mBGMText, mSEText, mStatTitle, mArtifactTitle;

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
        CharacterImage();
        mItemButton.onClick.AddListener(() => { Player.Instance.ItemUse(); });
        mArtifactButton.onClick.AddListener(() => { Player.Instance.ArtifactUse(); });
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        ShowItemImage();
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


    public void ShowSkill()
    {
        switch (GameSetting.Instance.PlayerID)
        {
            case 0:
                //스킬 이미지
                break;
            default:
                Debug.LogError("Wrong Skill Sprites");
                break;
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
            mMiniMapCamera.gameObject.SetActive(true);
        }
        else
        {
            mMiniMapCamera.gameObject.SetActive(false);
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
        mGoldText.text = Player.Instance.mInfoArr[Player.Instance.mID].Gold.ToString();
    }

    public void ShowHP()
    {
        string HP = string.Format("{0} / {1}", Player.Instance.mCurrentHP.ToString(), Player.Instance.mMaxHP.ToString());
        mHPText.text = HP;
    }

    public void ShowStat()
    {

        if (GameSetting.Instance.Language == 0)
        {//한국어
            mStatTitle.text = "캐릭터정보";
            mArtifactTitle.text = "유물";
            string Name = Player.Instance.mInfoArr[Player.Instance.mID].Name;
            string Stat = string.Format("체력: {0} / {1}\n" +
                                      "공격력: {2}\n" +
                                      "방어력: {3}\n" +
                                      "공격속도: {4}\n" +
                                      "이동속도: {5}\n" +
                                      "치명타 확률: {6}\n" +
                                      "치명타 피해: {7}\n" +
                                      "\n" +
                                      "쿨타임 감소: {8}\n" +
                                      "상태이상 저항: {9}", Player.Instance.mCurrentHP.ToString(), Player.Instance.mMaxHP.ToString(),
                                      Player.Instance.mInfoArr[Player.Instance.mID].Atk.ToString(),
                                      Player.Instance.mInfoArr[Player.Instance.mID].Def.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd.ToString("N2"),
                                      Player.Instance.mInfoArr[Player.Instance.mID].Spd.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].Crit.ToString("P1"),
                                      Player.Instance.mInfoArr[Player.Instance.mID].CritDamage.ToString("P1"),
                                      Player.Instance.mInfoArr[Player.Instance.mID].CooltimeReduce.ToString("P0"), Player.Instance.mInfoArr[Player.Instance.mID].CCReduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = Name;
        }
        else if (GameSetting.Instance.Language == 1)
        {//영어
            mStatTitle.text = "Stat";
            mArtifactTitle.text = "Artifact";
            string Name = Player.Instance.mInfoArr[Player.Instance.mID].EngName;
            string Stat = string.Format("HP: {0} / {1}\n" +
                                      "Atk: {2}\n" +
                                      "Def: {3}\n" +
                                      "Atk Spd: {4}\n" +
                                      "Movement Spd: {5}\n" +
                                      "Crit: {6}\n" +
                                      "Crit Damage: {7}\n" +
                                      "\n" +
                                      "Cooldown reduce: {8}\n" +
                                      "Resistance: {9}", Player.Instance.mCurrentHP.ToString(), Player.Instance.mMaxHP.ToString(),
                                      Player.Instance.mInfoArr[Player.Instance.mID].Atk.ToString(),
                                      Player.Instance.mInfoArr[Player.Instance.mID].Def.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd.ToString("N2"),
                                      Player.Instance.mInfoArr[Player.Instance.mID].Spd.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].Crit.ToString("P1"),
                                      Player.Instance.mInfoArr[Player.Instance.mID].CritDamage.ToString("P1"),
                                      Player.Instance.mInfoArr[Player.Instance.mID].CooltimeReduce.ToString("P0"), Player.Instance.mInfoArr[Player.Instance.mID].CCReduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = Name;
        }
    }
}
