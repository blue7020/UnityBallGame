using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private string mName = "";
    private string[] mNameArr = { "드럭만", "님폰없", "콥슨"};
    private string[] mTitleArr = { "<허름한> ", "<추레한> ", "<지저분한> ", "<보잘것 없는> "};
    public string Name 
    { 
        get 
        {
            if(string.IsNullOrEmpty(mName))
            {
                mName = mTitleArr[Random.Range(0, mTitleArr.Length)] + 
                        mNameArr[Random.Range(0, mNameArr.Length)];
            }
            return mName;
        } 
    }


    private int mLevel, mATK, mDEF;
    private float mMaxHP, mCurrentHP;
    private float mMaxMP, mCurrentMP;
    private float mMaxExp, mCurrentExp;

    private const int EQUIP_SLOT_COUNT = 3;
    private ItemData[] mEquipDataArr;
    private const int SKILL_COUNT = 3;
    private SkillData[] mSkillDataArr;
    //-----------------작성공간-----------------

    public void GetStats(int level, int atk, int def, float maxhp, float currenthp, float maxmp, float currentmp, float maxexp,float currentexp)
    {
        level = mLevel;
        atk = mATK;
        def = mDEF;
        maxhp = mMaxHP;
        maxmp = mMaxMP;
        maxexp = mMaxExp;
        currenthp = mCurrentHP;
        currentmp = mCurrentMP;
        currentexp = mCurrentExp;
    }
    //-----------------------------------------

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mLevel = 0;
            mATK = 1;
            mDEF = 0;
            mCurrentHP = mMaxHP = 50;
            mCurrentMP = mMaxMP = 10;
            mEquipDataArr = new ItemData[EQUIP_SLOT_COUNT];
            mSkillDataArr = new SkillData[SKILL_COUNT];
            mMaxExp = 100;
            mCurrentExp = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(ManaRegen());
        for(int i = 0; i < SKILL_COUNT; i++)
        {
            mSkillDataArr[i] = SkillDataController.Instance.GetSkillData(i);
        }
        //-----------------작성공간-----------------
        UIController.Instance.ShowPlayerStat();
        UIController.Instance.ShowPlayerGaugeText();
        //-----------------------------------------
    }

    public void CastSkill(int id)
    {
        if(!UseMana(mSkillDataArr[id].MP)) { return; }//옵션

        StartCoroutine(Skill(id));//옵션
        StartCoroutine(SkillCooltime(id));//옵션
    }
    private IEnumerator Skill(int id)
    {
        float time = mSkillDataArr[id].Duration;

        switch (mSkillDataArr[id].SkillType)
        {
            case eSkillType.Heal:
                mCurrentHP = Mathf.Min(mCurrentHP + mSkillDataArr[id].Amount, mMaxHP);
                break;
            case eSkillType.Berserk:
                mCurrentHP = Mathf.Max(mCurrentHP - mMaxHP / 10, 0);
                mATK += mSkillDataArr[id].Amount;
                break;
            case eSkillType.DefenseAura:
                mDEF += mSkillDataArr[id].Amount;
                break;
        }
        //-----------------작성공간-----------------
        Debug.LogFormat("HP: {0}/{1} \nAtk{2} \nDef: {3}", mCurrentHP.ToString("N1"), mMaxHP.ToString("N1"), mATK, mDEF);
        //-----------------------------------------
        WaitForSeconds tick = new WaitForSeconds(time);
        yield return tick;

        switch (mSkillDataArr[id].SkillType)
        {
            case eSkillType.Berserk:
                mATK -= mSkillDataArr[id].Amount;
                break;
            case eSkillType.DefenseAura:
                mDEF -= mSkillDataArr[id].Amount;
                break;
        }

        //-----------------작성공간-----------------
        Debug.LogFormat("HP: {0}/{1} \nAtk{2} \nDef: {3}", mCurrentHP.ToString("N1"), mMaxHP.ToString("N1"), mATK, mDEF);
        //-----------------------------------------
    }

    private IEnumerator SkillCooltime(int id) // 옵션
    {
        float time = mSkillDataArr[id].Cooltime; // 옵션
        //-----------------작성공간-----------------
        WaitForSeconds tick = new WaitForSeconds(time);
        while (time > 0)
        {
            yield return tick;
            time -= time;
        }
        //-----------------------------------------
    }

    public void Hit(float amount)
    {
        mCurrentHP -= amount;
        //-----------------작성공간-----------------
        Debug.LogFormat("HP: {0}/{1}", mCurrentHP.ToString("N1"), mMaxHP.ToString("N1"));
        //-----------------------------------------
    }

    private IEnumerator ManaRegen()
    {
        WaitForSeconds tick = new WaitForSeconds(1);
        while (true)
        {
            yield return tick;
            Manaregen(1);
        }
    }

    public void Manaregen(float amount)
    {
        mCurrentMP = Mathf.Min(mCurrentMP + amount, mMaxMP);
        //-----------------작성공간-----------------
        UIController.Instance.ShowPlayerGaugeText();
        Debug.LogFormat("MP: {0}/{1}", mCurrentMP.ToString("N1"), mMaxMP.ToString("N1"));
        //-----------------------------------------
    }

    public bool UseMana(float amount)
    {
        if (mCurrentMP >= amount)
        {
            mCurrentMP = Mathf.Max(mCurrentMP - amount, 0);
            //-----------------작성공간-----------------
            UIController.Instance.ShowPlayerGaugeText();
            Debug.LogFormat("MP: {0}/{1}", mCurrentMP.ToString("N1"), mMaxMP.ToString("N1"));
            //-----------------------------------------
            return true;
        }
        else
        {
            //-----------------작성공간-----------------
            Debug.Log("Not enough Mana");
            //-----------------------------------------
            return false;
        }
    }

    public void AddExp(float amount)
    {
        mCurrentExp += amount;
        while (mCurrentExp > mMaxExp)
        {
            mLevel++;
            mCurrentExp -= mMaxExp;
            mMaxExp = 100 * mLevel;
        }
        //-----------------작성공간-----------------
        Debug.LogFormat("Current Level: {0} \nEXP: {1}/{2}", mLevel, mCurrentExp.ToString("N1"), mMaxExp.ToString("N1"));
        //-----------------------------------------
    }

    public void Equip(ItemData data)
    {
        Unequip(data.ItemType);
        //-----------------작성공간-----------------
        Debug.LogFormat("Type: {0} \nMinLevel: {1} \nATK: {2} \nHP: {3} \nMP: {4}", data.ItemType, data.Level, data.ATK, data.DEF, data.HP, data.MP);
        //-----------------------------------------
    }
    public void Unequip(eItemType itemType)
    {
        //-----------------작성공간-----------------
        Debug.Log(mEquipDataArr[(int)itemType]);
        //-----------------------------------------
    }
}
