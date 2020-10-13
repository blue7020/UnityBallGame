using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectController : InformationLoader
{
    public static PlayerSelectController Instance;

    public MainLobbyPlayer[] mPlayer;
    public Image mBlackScreen, mPlayerImage,mWindow;
    public Sprite mLockPlayer;
    public VirtualJoyStick mStick;
    public Button LeftButton, RightButton, SelectButton;
    public int NowPlayerID;
    public Text mNameText, mStatText, mSelectText,mTitleText;
    public PlayerStat[] mPlayerStat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mPlayerStat = SaveDataController.Instance.mPlayerInfoArr;
            if (GameSetting.Instance.NowScene == 1)
            {
                mWindow.gameObject.SetActive(false);
                GameSetting.Instance.NowScene = 1;
            }
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
            NowPlayerID = 0;
            LeftButton.gameObject.SetActive(false);
            ShowStat();
        }
        else if(GameSetting.Instance.NowScene == 1)
        {
            NowPlayerID = GameSetting.Instance.PlayerID;
            if (NowPlayerID==0)
            {
                LeftButton.gameObject.SetActive(false);
            }
            ReturnLobby();
        }
        mBlackScreen.gameObject.SetActive(false);

    }

    public void LeftCharacterSelect()
    {
        NowPlayerID -=1;
        ShowStat();
        if (NowPlayerID-1<0)
        {
            LeftButton.gameObject.SetActive(false);
        }
        RightButton.gameObject.SetActive(true);
    }
    public void RightCharacterSelect()
    {
        NowPlayerID +=1;
        ShowStat();
        if (NowPlayerID + 1 >= mPlayer.Length)
        {
            RightButton.gameObject.SetActive(false);
        }
        LeftButton.gameObject.SetActive(true);
    }

    public void CharaChange()
    {
        MainLobbyUIController.Instance.IsSelect = false;
        StartCoroutine(ShowBlackScreen(NowPlayerID));
    }

    public void CharaBuy()
    {
        if (SaveDataController.Instance.mUser.Syrup>=mPlayerStat[NowPlayerID].Price)
        {
            SaveDataController.Instance.mUser.CharacterHas[NowPlayerID] = true;
            SaveDataController.Instance.mUser.Syrup -= mPlayerStat[NowPlayerID].Price;
            MainLobbyUIController.Instance.ShowSyrupText();
            ShowStat();
        }
    }

    public IEnumerator ShowBlackScreen(int id)
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mBlackScreen.gameObject.SetActive(true);
        GameSetting.Instance.PlayerID = id;
        MainLobbyPlayer player= Instantiate(mPlayer[id], Vector3.zero, Quaternion.identity);
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
    }

    public void ShowStat()
    {
        if (SaveDataController.Instance.mUser.CharacterOpen[NowPlayerID] == false)
        {
            mPlayerImage.sprite = mLockPlayer;
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
        else
        {
            if (SaveDataController.Instance.mUser.CharacterHas[NowPlayerID] ==true)
            {
                mPlayerImage.sprite = mPlayer[NowPlayerID].mRenderer.sprite;
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
                                              "상태이상 저항: {7}", mPlayerStat[NowPlayerID].Hp.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Spd.ToString("N1"), mPlayerStat[NowPlayerID].Crit.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CritDamage.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CooltimeReduce.ToString("P0"), mPlayerStat[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    mNameText.text = mPlayerStat[NowPlayerID].Name;
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
                                              "Resistance: {7}", mPlayerStat[NowPlayerID].Hp.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Spd.ToString("N1"), mPlayerStat[NowPlayerID].Crit.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CritDamage.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CooltimeReduce.ToString("P0"), mPlayerStat[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    mNameText.text = mPlayerStat[NowPlayerID].EngName;
                    mSelectText.text = "Select";
                    mTitleText.text = "Character Select";
                }
                SelectButton.onClick.RemoveAllListeners();
                SelectButton.onClick.AddListener(() => { CharaChange(); });
                SelectButton.onClick.AddListener(() => { MainLobbyUIController.Instance.ButtonPush(); });
            }
            else
            {
                mPlayerImage.sprite = mPlayer[NowPlayerID].mRenderer.sprite;
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
                                              "상태이상 저항: {7}", mPlayerStat[NowPlayerID].Hp.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Spd.ToString("N1"), mPlayerStat[NowPlayerID].Crit.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CritDamage.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CooltimeReduce.ToString("P0"), mPlayerStat[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    mNameText.text = mPlayerStat[NowPlayerID].Name;
                    mTitleText.text = "캐릭터 선택";
                    if (mPlayerStat[NowPlayerID].PurchaseID == 0)
                    {
                        mSelectText.text = "구매: " + mPlayerStat[NowPlayerID].Price + "시럽";
                        SelectButton.onClick.RemoveAllListeners();
                        SelectButton.onClick.AddListener(() => { CharaBuy(); });
                        SelectButton.onClick.AddListener(() => { MainLobbyUIController.Instance.ButtonPush(); });
                    }
                    else if (mPlayerStat[NowPlayerID].PurchaseID == 1)
                    {
                        mSelectText.text = "가격기재 바람";
                        //유료 상점 버튼 연결
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
                                              "Resistance: {7}", mPlayerStat[NowPlayerID].Hp.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Atk.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Def.ToString("N1"),
                                              mPlayerStat[NowPlayerID].Spd.ToString("N1"), mPlayerStat[NowPlayerID].Crit.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CritDamage.ToString("P1"),
                                              mPlayerStat[NowPlayerID].CooltimeReduce.ToString("P0"), mPlayerStat[NowPlayerID].CCReduce.ToString("P0"));
                    mStatText.text = Stat;
                    mNameText.text = mPlayerStat[NowPlayerID].EngName;
                    mTitleText.text = "Character Select";
                    if (mPlayerStat[NowPlayerID].PurchaseID == 0)
                    {
                        mSelectText.text = "Buy: " + mPlayerStat[NowPlayerID].Price + "Syrup";
                        SelectButton.onClick.RemoveAllListeners();
                        SelectButton.onClick.AddListener(() => { CharaBuy(); });
                        SelectButton.onClick.AddListener(() => { MainLobbyUIController.Instance.ButtonPush(); });
                    }
                    else if (mPlayerStat[NowPlayerID].PurchaseID == 1)
                    {
                        mSelectText.text = "가격기재 바람";
                        //유료 상점 버튼 연결
                    }
                }
            }
            SelectButton.interactable = true;
        }
    }
}
