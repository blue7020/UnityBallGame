using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectController : InformationLoader
{
    public static PlayerSelectController Instance;

    public MainLobbyPlayer[] mPlayer;
    public Image mBlackScreen, mPlayerImage,mWindow, mShadow;
    public GameObject mPlayerShadow;
    public Sprite mLockPlayer;
    public VirtualJoyStick mStick;
    public Button LeftButton, RightButton, SelectButton, UpgradeButton;
    public int NowPlayerID, PlayerID;
    public Text mNameText, mStatText, mSelectText,mTitleText, mUpgradeButtonText,mUpgradeGuideText;
    public PlayerStat[] mPlayerStat;
    public List<PlayerStat> mPlayerStatList;
    public int[] mUpgradePrice;
    public float[] mUpgradeStattoCritText, mUpgradeStattoCooltimeText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerID = 0;
            mPlayerStat = SaveDataController.Instance.mPlayerInfoArr;
            if (GameSetting.Instance.NowScene == 1)
            {
                mWindow.gameObject.SetActive(false);
                GameSetting.Instance.NowScene = 1;
            }
            PlayerSetting();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GameSetting.Instance.NowScene == 0)
        {
            ShowStat();
        }
        else if(GameSetting.Instance.NowScene == 1)
        {
            ReturnLobby();
        }
        mBlackScreen.gameObject.SetActive(false);

    }

    public void PlayerSetting()
    {
        mPlayerStatList = new List<PlayerStat>();
        for (int i = 0; i < mPlayerStat.Length; i++)
        {
            if (SaveDataController.Instance.mUser.CharacterHas[i] == true || SaveDataController.Instance.mUser.CharacterOpen[i] == true)
            {
                mPlayerStatList.Add(mPlayerStat[i]);
            }
        }
        NowPlayerID = 0;
        LeftButton.gameObject.SetActive(false);
        if (mPlayerStatList.Count == 1)
        {
            RightButton.gameObject.SetActive(false);
        }
        else
        {
            RightButton.gameObject.SetActive(true);
        }
    }

    public void LeftCharacterSelect()
    {
        NowPlayerID -=1;
        ShowStat();
        if (NowPlayerID-1<0)
        {
            LeftButton.gameObject.SetActive(false);
        }
        if (mPlayerStatList.Count==1)
        {
            RightButton.gameObject.SetActive(false);
        }
        else
        {
            RightButton.gameObject.SetActive(true);
        }
    }
    public void RightCharacterSelect()
    {
        NowPlayerID +=1;
        ShowStat();
        if (NowPlayerID + 1 >= mPlayerStatList.Count)
        {
            RightButton.gameObject.SetActive(false);
        }
        LeftButton.gameObject.SetActive(true);
    }

    public void CharaChange()
    {
        MainLobbyUIController.Instance.IsSelect = false;
        StartCoroutine(ShowBlackScreen(mPlayerStatList[NowPlayerID].ID));
        SaveDataController.Instance.Save();
    }

    public void CharaBuy()
    {
        if (SaveDataController.Instance.mUser.Syrup>= mPlayerStatList[NowPlayerID].Price)
        {
            SoundController.Instance.SESoundUI(3);
            SaveDataController.Instance.mUser.CharacterHas[NowPlayerID] = true;
            SaveDataController.Instance.mUser.CharacterOpen[NowPlayerID] = true;
            Debug.Log(mUpgradePrice[SaveDataController.Instance.mUser.CharacterUpgrade[NowPlayerID]]);
            SaveDataController.Instance.mUser.Syrup -= mUpgradePrice[SaveDataController.Instance.mUser.CharacterUpgrade[NowPlayerID]];
            MainLobbyUIController.Instance.ShowSyrupText();
            ShowStat();
            SaveDataController.Instance.Save();
        }
    }

    public void CharaUpGrade()
    {
        if (SaveDataController.Instance.mUser.Syrup >= mUpgradePrice[SaveDataController.Instance.mUser.CharacterUpgrade[PlayerID]] - 500)
        {
            SoundController.Instance.SESoundUI(8);
            SaveDataController.Instance.mUser.CharacterUpgrade[PlayerID] += 1;
            SaveDataController.Instance.mUser.Syrup -= mUpgradePrice[SaveDataController.Instance.mUser.CharacterUpgrade[PlayerID]]-500;
            MainLobbyUIController.Instance.ShowSyrupText();
            ShowStat();
            SaveDataController.Instance.Save();
        }
    }

    public IEnumerator ShowBlackScreen(int id)
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mBlackScreen.gameObject.SetActive(true);
        GameSetting.Instance.PlayerID = id;
        MainLobbyPlayer player= Instantiate(mPlayer[id], Vector3.zero, Quaternion.identity);
        Instantiate(mPlayerShadow, player.transform);
        MainLobbyCamera.Instance.CameraSetting(player);
        yield return delay;
        mBlackScreen.gameObject.SetActive(false);
        mWindow.gameObject.SetActive(false);
    }

    public void ReturnLobby()
    {
        MainLobbyPlayer player = Instantiate(mPlayer[GameSetting.Instance.PlayerID], Vector3.zero, Quaternion.identity);
        MainLobbyCamera.Instance.CameraSetting(player);
        NowPlayerID = GameSetting.Instance.PlayerID;
        mWindow.gameObject.SetActive(false);
        StartCoroutine(SceneMoveShadow());
    }

    public IEnumerator SceneMoveShadow()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mShadow.gameObject.SetActive(true);
        yield return delay;
        mShadow.gameObject.SetActive(false);
    }

    public void ShowStat()
    {
        PlayerID = mPlayerStatList[NowPlayerID].ID;
        if (SaveDataController.Instance.mUser.CharacterOpen[PlayerID] == true)
        {
            if (SaveDataController.Instance.mUser.CharacterHas[PlayerID] == true)
            {
                mPlayerImage.sprite = mPlayer[PlayerID].mRenderer.sprite;
                int upgradePrice = mUpgradePrice[SaveDataController.Instance.mUser.CharacterUpgrade[PlayerID]];
                int upgradeCharaID = SaveDataController.Instance.mUser.CharacterUpgrade[PlayerID];
                if (GameSetting.Instance.Language == 0)
                {//한국어
                    string Stat = string.Format("최대 체력: {0}\n" +
                                              "공격력: {1}\n" +
                                              "방어력: {2}\n" +
                                              "이동속도: {3}\n" +
                                              "치명타 확률: {4}\n" +
                                              "치명타 피해: {5}\n" +
                                              "\n" +
                                              "쿨타임 감소: {6}\n" +
                                              "상태이상 저항: {7}", (mPlayerStatList[NowPlayerID].Hp + upgradeCharaID).ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Spd.ToString("N1"), (mPlayerStatList[NowPlayerID].Crit + mUpgradeStattoCritText[upgradeCharaID]).ToString("P1"),
                                              "1"+ mPlayerStatList[NowPlayerID].CritDamage.ToString("P1"),
                                              (mPlayerStatList[NowPlayerID].CooltimeReduce+mUpgradeStattoCooltimeText[upgradeCharaID]).ToString("P0"), mPlayerStatList[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    if (upgradeCharaID == 5)
                    {
                        mUpgradeGuideText.gameObject.SetActive(false);
                        string name = string.Format("<color=#FFEE00>{0}</color>", mPlayerStatList[NowPlayerID].Name);
                        mNameText.text = name;
                        string upgradetext = string.Format("\n<color=#FFEE00>강화 {0 }/ 5</color>", upgradeCharaID);
                        mStatText.text += upgradetext;
                        mUpgradeButtonText.text = "최대 강화";
                        UpgradeButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (upgradeCharaID ==4)
                        {
                            mUpgradeGuideText.text = "+1\n\n\n+5%\n\n+10%\n\n\n\n";
                        }
                        else
                        {
                            mUpgradeGuideText.text = "+1\n\n\n\n\n\n\n\n\n\n";
                        }
                        mUpgradeGuideText.gameObject.SetActive(true);
                        mNameText.text = mPlayerStatList[NowPlayerID].Name;
                        mStatText.text += "\n강화 " + upgradeCharaID + "/ 5";
                        mUpgradeButtonText.text = "강화: " + upgradePrice + "시럽";
                        UpgradeButton.gameObject.SetActive(true);
                    }
                    mSelectText.text = "선택";
                    mTitleText.text = "캐릭터 선택";
                }
                else if (GameSetting.Instance.Language == 1)
                {//영어
                    string Stat = string.Format("Max HP: {0}\n" +
                                              "Atk: {1}\n" +
                                              "Def: {2}\n" +
                                              "Movement Spd: {3}\n" +
                                              "Crit: {4}\n" +
                                              "Crit Damage: {5}\n" +
                                              "\n" +
                                              "Cooldown reduce: {6}\n" +
                                              "Resistance: {7}", (mPlayerStatList[NowPlayerID].Hp + upgradeCharaID).ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Spd.ToString("N1"), (mPlayerStatList[NowPlayerID].Crit + mUpgradeStattoCritText[upgradeCharaID]).ToString("P1"),
                                              "1"+ mPlayerStatList[NowPlayerID].CritDamage.ToString("P1"),
                                              (mPlayerStatList[NowPlayerID].CooltimeReduce + mUpgradeStattoCooltimeText[upgradeCharaID]).ToString("P0"), mPlayerStatList[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    if (upgradeCharaID == 5)
                    {
                        mUpgradeGuideText.gameObject.SetActive(false);
                        string name = string.Format("<color=#FFEE00>{0}</color>", mPlayerStatList[NowPlayerID].EngName);
                        mNameText.text = name;
                        string upgradetext = string.Format("\n<color=#FFEE00>Upgrade {0 }/ 5</color>", upgradeCharaID);
                        mStatText.text += upgradetext;
                        mUpgradeButtonText.text = "Max Upgrade";
                        UpgradeButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (upgradeCharaID == 4)
                        {
                            mUpgradeGuideText.text = "+1\n\n\n+5%\n\n+10%\n\n\n\n";
                        }
                        else
                        {
                            mUpgradeGuideText.text = "+1\n\n\n\n\n\n\n\n\n\n";
                        }
                        mUpgradeGuideText.gameObject.SetActive(true);
                        mNameText.text = mPlayerStatList[NowPlayerID].EngName;
                        mStatText.text += "\nUpgrade " + upgradeCharaID + "/ 5";
                        mUpgradeButtonText.text = "Upgrade: " + upgradePrice + "Syrup";
                        UpgradeButton.gameObject.SetActive(true);
                    }
                    mSelectText.text = "Select";
                    mTitleText.text = "Character Select";
                }
                SelectButton.onClick.RemoveAllListeners();
                SelectButton.onClick.AddListener(() => { CharaChange(); });
                SelectButton.onClick.AddListener(() => { MainLobbyUIController.Instance.ButtonPush(); });
            }
            else
            {
                UpgradeButton.gameObject.SetActive(false);
                mUpgradeGuideText.gameObject.SetActive(false);
                mPlayerImage.sprite = mPlayer[PlayerID].mRenderer.sprite;
                if (GameSetting.Instance.Language == 0)
                {//한국어
                    string Stat = string.Format("최대 체력: {0}\n" +
                                              "공격력: {1}\n" +
                                              "방어력: {2}\n" +
                                              "이동속도: {3}\n" +
                                              "치명타 확률: {4}\n" +
                                              "치명타 피해: {5}\n" +
                                              "\n" +
                                              "쿨타임 감소: {6}\n" +
                                              "상태이상 저항: {7}", mPlayerStatList[NowPlayerID].Hp.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Spd.ToString("N1"), mPlayerStatList[NowPlayerID].Crit.ToString("P1"),
                                              "1"+ mPlayerStatList[NowPlayerID].CritDamage.ToString("P1"),
                                              mPlayerStatList[NowPlayerID].CooltimeReduce.ToString("P0"), mPlayerStatList[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    mNameText.text = mPlayerStatList[NowPlayerID].Name;
                    mTitleText.text = "캐릭터 선택";
                    if (mPlayerStatList[NowPlayerID].PurchaseID == 0)
                    {
                        mSelectText.text = "구매: " + mPlayerStatList[NowPlayerID].Price + "시럽";
                        SelectButton.onClick.RemoveAllListeners();
                        SelectButton.onClick.AddListener(() => { CharaBuy(); });
                        SelectButton.onClick.AddListener(() => { MainLobbyUIController.Instance.ButtonPush(); });
                    }
                }
                else if (GameSetting.Instance.Language == 1)
                {//영어
                    string Stat = string.Format("Max HP: {0}\n" +
                                              "Atk: {1}\n" +
                                              "Def: {2}\n" +
                                              "Movement Spd: {3}\n" +
                                              "Crit: {4}\n" +
                                              "Crit Damage: {5}\n" +
                                              "\n" +
                                              "Cooldown reduce: {6}\n" +
                                              "Resistance: {7}", mPlayerStatList[NowPlayerID].Hp.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStatList[NowPlayerID].Spd.ToString("N1"), mPlayerStatList[NowPlayerID].Crit.ToString("P1"),
                                              "1"+ mPlayerStatList[NowPlayerID].CritDamage.ToString("P1"),
                                              mPlayerStatList[NowPlayerID].CooltimeReduce.ToString("P0"), mPlayerStatList[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    mNameText.text = mPlayerStatList[NowPlayerID].EngName;
                    mTitleText.text = "Character Select";
                    if (mPlayerStatList[NowPlayerID].PurchaseID == 0)
                    {
                        mSelectText.text = "Buy: " + mPlayerStatList[NowPlayerID].Price + "Syrup";
                        SelectButton.onClick.RemoveAllListeners();
                        SelectButton.onClick.AddListener(() => { CharaBuy(); });
                        SelectButton.onClick.AddListener(() => { MainLobbyUIController.Instance.ButtonPush(); });
                    }
                }
            }
            SelectButton.interactable = true;
        }
        else
        {
            mPlayerImage.sprite = mLockPlayer;
            UpgradeButton.gameObject.SetActive(false);
            mUpgradeGuideText.gameObject.SetActive(false);
            if (GameSetting.Instance.Language == 0)
            {//한국어
                string Stat = string.Format("최대 체력: ?\n" +
                                          "공격력: ?\n" +
                                          "방어력: ?\n" +
                                          "이동속도: ?\n" +
                                          "치명타 확률: ?\n" +
                                          "치명타 피해: ?\n" +
                                          "\n" +
                                          "쿨타임 감소: ?\n" +
                                          "상태이상 저항: ?");
                mStatText.text = Stat;
                mNameText.text = "잠김";
                mSelectText.text = "선택";
                mTitleText.text = "캐릭터 선택";
            }
            else if (GameSetting.Instance.Language == 1)
            {//영어
                string Stat = string.Format("Max HP: ?\n" +
                                          "Atk: ?\n" +
                                          "Def: ?\n" +
                                          "Movement Spd: ?\n" +
                                          "Crit: ?\n" +
                                          "Crit Damage: ?\n" +
                                          "\n" +
                                          "Cooldown reduce: ?\n" +
                                          "Resistance: ?");
                mStatText.text = Stat;
                mNameText.text = "Unlock";
                mSelectText.text = "Select";
                mTitleText.text = "Character Select";
            }
            SelectButton.interactable = false;
        }
    }
}
