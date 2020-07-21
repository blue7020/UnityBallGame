using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

    public Text mPlayernameText1, mPlayernameText2,mPlayerStatText;
    public Text mLevel,mHP, mMp,mEXP,mSlotText,mPopupText;
    public Image mHPbar, mMPBar, mEXPBar,PlayerHitImage,mPopUp;

    public const int SLOT_COUNT = 8;

    public Transform SkillSlotParents;
    public SkillSlot mSkillSlot;
    public SkillSlot[] mSkillSlotArr;
    public Sprite mVoidspt;

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
        mSkillSlotArr = new SkillSlot[SLOT_COUNT];
        for (int i=0; i<mSkillSlotArr.Length;i++)
        {
            mSkillSlotArr[i] = Instantiate(mSkillSlot,SkillSlotParents);
            mSkillSlotArr[i].Init(i, mVoidspt);
        }
    }

    private void Start()
    {
        for (int i = 0; i < Player.Instance.SkillSlotCount; i++)
        {
            mSkillSlotArr[i].SetSkill(i);
        }
    }

    public void ShowPlayerName()
    {
        mPlayernameText1.text = Player.Instance.Name;
        mPlayernameText2.text = Player.Instance.Name;
    }
    public void ShowPlayerStatusText()
    {
        mHPbar.fillAmount = Player.Instance.CurrentHP / Player.Instance.MaxHP;
        mHP.text = Player.Instance.CurrentHP.ToString() + "/" + Player.Instance.MaxHP.ToString();
        mMPBar.fillAmount = Player.Instance.CurrentMP / Player.Instance.MaxMP;
        mMp.text = Player.Instance.CurrentMP.ToString() + "/" + Player.Instance.MaxMP.ToString();
        mPlayerStatText.text = "HP: " + Player.Instance.CurrentHP.ToString() + "/" + Player.Instance.MaxHP.ToString() + "\n" +
            "MP: " + Player.Instance.CurrentMP.ToString() + "/" + Player.Instance.MaxMP.ToString() + "\n" +
            "ATK: " + Player.Instance.Atk.ToString() +"\n"+"DEF: "+Player.Instance.Def.ToString();
    }
    public void ShowPlayerExp()
    {
        mLevel.text = Player.Instance.Level.ToString();
        mEXPBar.fillAmount = Player.Instance.CurrentEXP / Player.Instance.MaxEXP;
        mEXP.text = Player.Instance.CurrentEXP.ToString()+"/"+ Player.Instance.MaxEXP.ToString();
    }

    public IEnumerator PlayerHit()
    {
        WaitForSeconds dura = new WaitForSeconds(3f);
        PlayerHitImage.gameObject.SetActive(true);
        yield return dura;
        PlayerHitImage.gameObject.SetActive(false);
    }

    public void ShowPopup(string message)
    {
        mPopupText.text = message;
        mPopUp.gameObject.SetActive(true);
    }
}
