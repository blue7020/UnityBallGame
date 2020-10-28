using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Statue : MonoBehaviour
{
    public SpriteRenderer mRenderer;

    public int mID;
    public int mStatueID;
    public StatueStat mStats;

    public float SpendGold;
    public Text mPriceText,mStatueSpendText;

    public eStatueType eType;
    public eStatuePay ePayType;
    private bool IsUse, isTutorial;
    private string text,bufftext, bufftext2;
    private TextEffect effect;
    private SkillEffect buffEffect;
    private Button mUIStatueButton, mTutorialUIStatueButton;

    private void Awake()
    {
        IsUse = false;
        isTutorial = GameController.Instance.IsTutorial;
        if (isTutorial==true)
        {
            mTutorialUIStatueButton = TutorialUIController.Instance.mStatueButton;
        }
        else
        {
            mUIStatueButton = UIController.Instance.mStatueButton;
            mStatueSpendText = UIController.Instance.mStatueSpendText;
        }
    }

    public void StatSetting(int id)
    {
        mStatueID = id;
        switch (mStatueID)
        {
            case 0:
                eType = eStatueType.Heal;
                mRenderer.sprite = GameController.Instance.mStatueSprites[0];
                break;
            case 1:
                eType = eStatueType.Strength;
                mRenderer.sprite = GameController.Instance.mStatueSprites[2];
                break;
            case 2:
                eType = eStatueType.Speed;
                mRenderer.sprite = GameController.Instance.mStatueSprites[4];
                break;
            case 3:
                eType = eStatueType.Def;
                mRenderer.sprite = GameController.Instance.mStatueSprites[6];
                break;
            case 4:
                eType = eStatueType.Gold;
                mRenderer.sprite = GameController.Instance.mStatueSprites[8];
                break;
            case 5:
                eType = eStatueType.War;
                mRenderer.sprite = GameController.Instance.mStatueSprites[10];
                break;
            case 6:
                eType = eStatueType.Heart;
                mRenderer.sprite = GameController.Instance.mStatueSprites[12];
                break;
            case 7:
                eType = eStatueType.Harvest;
                mRenderer.sprite = GameController.Instance.mStatueSprites[14];
                break;
            case 8:
                eType = eStatueType.Resistance;
                mRenderer.sprite = GameController.Instance.mStatueSprites[16];
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
        mStats = StatueController.Instance.mStatInfoArr[mStatueID];
        if (GameController.Instance.IsTutorial==false)
        {
            mPriceText.transform.position = transform.position + new Vector3(0, -0.5f, 0);
            mPriceText.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        }
    }

    //석상의 buff코드
    //20=무적 21=공격력 22=방어력 23=공격속도 24=이동속도 25=치명타 28= cc기 저항
    private void StatueUse()
    {
        SoundController.Instance.SESound(20);
        switch (eType)
        {
            case eStatueType.Heal:
                Player.Instance.Heal(mStats.Hp);
                mRenderer.sprite = GameController.Instance.mStatueSprites[1];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "체력 회복!";
                }
                else
                {
                    bufftext = "Restore HP!";
                }
                buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
                buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 0.5f,0,Color.green);
                BuffEffectController.Instance.EffectList.Add(buffEffect);
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            case eStatueType.Strength:
                StartCoroutine(Player.Instance.Atk(mStats.Atk,21, mStats.Duration));
                mRenderer.sprite = GameController.Instance.mStatueSprites[3];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "공격력 증가!";
                }
                else
                {
                    bufftext = "Attack increase!";
                }
                buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
                buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0, Color.red);
                BuffEffectController.Instance.EffectList.Add(buffEffect);
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            case eStatueType.Speed:
                StartCoroutine(Player.Instance.Speed(mStats.Spd,24, mStats.Duration));
                StartCoroutine(Player.Instance.AtkSpeed(mStats.AtkSpd,23, mStats.Duration));
                mRenderer.sprite = GameController.Instance.mStatueSprites[5];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "이동 속도 증가!";
                    bufftext2 = "공격 속도 증가!";
                }
                else
                {
                    bufftext = "Movement speed increase!";
                    bufftext2 = "Attack speed increase!";
                }
                buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
                buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0, Color.cyan);
                BuffEffectController.Instance.EffectList.Add(buffEffect);
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
                buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0,Color.yellow);
                BuffEffectController.Instance.EffectList.Add(buffEffect);
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.transform.position += new Vector3(0, -0.65f, 0);
                effect.SetText(bufftext2);
                break;
            case eStatueType.Def:
                StartCoroutine(Player.Instance.Def(mStats.Def,22, mStats.Duration));
                mRenderer.sprite = GameController.Instance.mStatueSprites[7];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "방어력 증가!";
                }
                else
                {
                    bufftext = "Defence increase!";
                }
                buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
                buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0,Color.blue);
                BuffEffectController.Instance.EffectList.Add(buffEffect);
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            case eStatueType.Gold:
                Player.Instance.mGoldBonus += 0.5f;
                mRenderer.sprite = GameController.Instance.mStatueSprites[9];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "골드 획득량 증가!";
                }
                else
                {
                    bufftext = "Drop gold increase!";
                }
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            case eStatueType.War:
                Player.Instance.mStats.Crit += Player.Instance.mStats.Crit*0.3f;
                if (Player.Instance.mStats.Crit >= 1f)
                {
                    Player.Instance.mStats.Crit = 1f;
                }
                mRenderer.sprite = GameController.Instance.mStatueSprites[11];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "치명타 확률 증가!";
                }
                else
                {
                    bufftext = "Critical rate increase!";
                }
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            case eStatueType.Heart:
                Player.Instance.Heal(Player.Instance.mMaxHP);
                mRenderer.sprite = GameController.Instance.mStatueSprites[13];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "체력 전부 회복!";
                }
                else
                {
                    bufftext = "Restore full HP!";
                }
                buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
                buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 0.5f,0,Color.green);
                BuffEffectController.Instance.EffectList.Add(buffEffect);
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            case eStatueType.Harvest:
                mRenderer.sprite = GameController.Instance.mStatueSprites[15];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "재료 획득률 상승!";
                    GameController.Instance.MaterialDropRate += 0.3f;
                }
                else
                {
                    bufftext = "Material drop rate increase!";
                }
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            case eStatueType.Resistance:
                mRenderer.sprite = GameController.Instance.mStatueSprites[17];
                if (GameSetting.Instance.Language == 0)
                {
                    bufftext = "상태 이상 면역!";
                }
                else
                {
                    bufftext = "Debuff Resistance!";
                }
                BuffController.Instance.SetBuff(8, 38, eBuffType.Buff, mStats.Duration);
                buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
                buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration, 0, Color.magenta);
                BuffEffectController.Instance.EffectList.Add(buffEffect);
                effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(bufftext);
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
        effect = null;
        IsUse = true;
    }

    public void StatueUseButton()
    {
        if (isTutorial == true)
        {
            mTutorialUIStatueButton.gameObject.SetActive(false);
        }
        else
        {
            mUIStatueButton.gameObject.SetActive(false);
        }
        if (ePayType == eStatuePay.Pay)
        {
            if (Player.Instance.mStats.Gold >= SpendGold)
            {
                Player.Instance.mStats.Gold -= SpendGold;
                StatueUse();
                UIController.Instance.ShowGold();
                SoundController.Instance.SESoundUI(3);
                CanvasFinder.Instance.DeletdStatuePrice(mID);
            }
            else
            {
                if (GameSetting.Instance.Language == 0)
                {
                    text = "골드가 부족합니다!";
                }
                else
                {
                    text = "Not enough Gold!";
                }
                TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(text);
            }
        }
        else
        {
            StatueUse();
            if (isTutorial == true)
            {
                TutorialUIController.Instance.TutorialStatueCheck++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsUse == false)
            {
                if (isTutorial == true)
                {
                    mTutorialUIStatueButton.onClick.RemoveAllListeners();
                    mTutorialUIStatueButton.onClick.AddListener(() => { StatueUseButton(); });
                    mTutorialUIStatueButton.gameObject.SetActive(true);
                }
                else
                {
                    mUIStatueButton.onClick.RemoveAllListeners();
                    mUIStatueButton.onClick.AddListener(() => { StatueUseButton(); });
                    mStatueSpendText.text = "-" + SpendGold + "G";
                    mUIStatueButton.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isTutorial == true)
            {
                mTutorialUIStatueButton.gameObject.SetActive(false);
            }
            else
            {
                mUIStatueButton.gameObject.SetActive(false);
            }
        }
    }
}
