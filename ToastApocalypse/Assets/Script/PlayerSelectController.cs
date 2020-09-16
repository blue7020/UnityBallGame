using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectController : InformationLoader
{
    public static PlayerSelectController Instance;

    public MainLobbyPlayer[] mPlayer;
    public Image mBlackScreen, mPlayerImage,mWindow;
    public VirtualJoyStick mStick;
    public Button LeftButton, RightButton, SelectButton;
    public int NowPlayerID;
    public Text mNameText, mStatText, mSelectText;
    public PlayerStat[] mInfoArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.PLAYER_STAT);
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
        else
        {
            ReturnLobby();
        }
        mBlackScreen.gameObject.SetActive(false);
    }

    public void LeftCharacterSelect()
    {
        NowPlayerID--;
        ShowStat();
        if (NowPlayerID-1<0)
        {
            LeftButton.gameObject.SetActive(false);
        }
        RightButton.gameObject.SetActive(true);
    }
    public void RightCharacterSelect()
    {
        NowPlayerID++;
        ShowStat();
        if (NowPlayerID + 1 >= mPlayer.Length)
        {
            RightButton.gameObject.SetActive(false);
        }
        LeftButton.gameObject.SetActive(true);
    }

    public void CharaChange()
    {
        StartCoroutine(ShowBlackScreen(NowPlayerID));
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
                                      "상태이상 저항: {7}", mInfoArr[NowPlayerID].Hp.ToString("N1"),
                                      mInfoArr[NowPlayerID].Atk.ToString("N1"),
                                      mInfoArr[NowPlayerID].Def.ToString("N1"),
                                      mInfoArr[NowPlayerID].Spd.ToString("N1"), mInfoArr[NowPlayerID].Crit.ToString("P1"),
                                      mInfoArr[NowPlayerID].CritDamage.ToString("P1"),
                                      mInfoArr[NowPlayerID].CooltimeReduce.ToString("P0"), mInfoArr[NowPlayerID].CCReduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = mInfoArr[NowPlayerID].Name;
            mSelectText.text = "선택";
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
                                      "Resistance: {7}", mInfoArr[NowPlayerID].Hp.ToString("N1"),
                                      mInfoArr[NowPlayerID].Atk.ToString("N1"),
                                      mInfoArr[NowPlayerID].Def.ToString("N1"),
                                      mInfoArr[NowPlayerID].Spd.ToString("N1"), mInfoArr[NowPlayerID].Crit.ToString("P1"),
                                      mInfoArr[NowPlayerID].CritDamage.ToString("P1"),
                                      mInfoArr[NowPlayerID].CooltimeReduce.ToString("P0"), mInfoArr[NowPlayerID].CCReduce.ToString("P0"));
            mStatText.text = Stat;
            mNameText.text = mInfoArr[NowPlayerID].EngName;
            mSelectText.text = "Select";
        }
    }
}
