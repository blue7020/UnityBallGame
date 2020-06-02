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
    private Text mStatText;

    [SerializeField]
    private Button mStatButton;
    [SerializeField]
    private Button mSKillButton;

    [SerializeField]
    private RawImage mMiniMapCamera;
    private bool Minimap;

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
        Minimap = false;
    }

    //Plz Fix
    public void UnButtonSKill()
    {
        mStatButton.interactable = true;
        mSKillButton.interactable = false;
    }
    public void UnButtonStat()
    {
        mStatButton.interactable = false;
        mSKillButton.interactable = true;
    }
    

    public void ShowMiniMap()
    {
        if (Minimap == true)
        {
            Minimap = false;
            mMiniMapCamera.gameObject.SetActive(false);
        }
        else if(Minimap==false)
        {
            Minimap = true;
            mMiniMapCamera.gameObject.SetActive(true);

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
        string HP = string.Format("{0} / {1}", Player.Instance.mCurrentHP.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].Hp.ToString());
        mHPText.text = HP;
    }

    public void ShowStat()
    {
        string Stat = string.Format("체력: {0} / {1}\n" +
                                  "공격력: {2}\n" +
                                  "방어력: {3}\n" +
                                  "공격속도: {4}\n" +
                                  "이동속도: {5}\n" +
                                  "치명타 확률: {6}\n" +
                                  "치명타 피해: {7}\n" +
                                  "총 피해량: {8}\n" +
                                  "\n" +
                                  "쿨타임 감소: {9}\n" +
                                  "상태이상 저항: {10}", Player.Instance.mCurrentHP.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].Hp.ToString(),
                                  Player.Instance.mInfoArr[Player.Instance.mID].Atk.ToString(),
                                  Player.Instance.mInfoArr[Player.Instance.mID].Def.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd.ToString("N2"),
                                  Player.Instance.mInfoArr[Player.Instance.mID].Spd.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].Crit.ToString("P1"),
                                  Player.Instance.mInfoArr[Player.Instance.mID].CritDamage.ToString("P1"), Player.Instance.mInfoArr[Player.Instance.mID].Damage.ToString("P0"),
                                  Player.Instance.mInfoArr[Player.Instance.mID].CooltimeReduce.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].CCReduce.ToString("P0"));
        mStatText.text = Stat;
    }
}
