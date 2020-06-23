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
    private Image mPlayerImage;
    [SerializeField]
    private Image mitemImage;

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
    public Button mItemButton;

    [SerializeField]
    private RawImage mMiniMapCamera;
    [SerializeField]
    private Toggle mMiniMapButton;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CharacterImage();
        mItemButton.onClick.AddListener(() => { Player.Instance.ItemUse(); });
    }

    private void Update()
    {
        ShowItemImage();
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

    public void CharacterImage()
    {
        switch (Player.Instance.mID)
        {
            case 0:
                mPlayerImage.sprite = mCharacterSprite[0];
                break;
            case 1:
                mPlayerImage.sprite = mCharacterSprite[1];
                break;
            case 2:
                mPlayerImage.sprite = mCharacterSprite[2];
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

    public void ShowSkill()
    {
        switch (Player.Instance.mID)
        {
            case 0:
                //스킬 이미지
                break;
            default:
                Debug.LogError("Wrong Skill Sprites");
                break;
        }
    }

    public void ShowStat()
    {
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
}
