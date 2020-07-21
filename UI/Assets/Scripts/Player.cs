using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private string mName = "";
    private string[] mNameArr = { "핫산", "촬스", "콥슨"};
    private string[] mTitleArr = { "<허름한> ", "<무명의> ", "<풋내기> ", "<은둔 고수> "};
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
    public int Level { get { return mLevel; } }
    public int Atk { get { return mATK; } }
    public int Def { get { return mDEF; } }
    public float MaxHP { get { return mMaxHP; } }
    public float CurrentHP { get { return mCurrentHP; } }
    public float MaxMP { get { return mMaxMP; } }
    public float CurrentMP { get { return mCurrentMP; } }
    public float MaxEXP { get { return mMaxExp; } }

    public float CurrentEXP { get { return mCurrentExp; } }

    public int EquipSlotCount { get { return EQUIP_SLOT_COUNT; } }
    public int SkillSlotCount { get { return SKILL_COUNT; } }
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
        UIController.Instance.ShowPlayerName();
        UIController.Instance.ShowPlayerExp();
        UIController.Instance.ShowPlayerStatusText();
        //-----------------------------------------
    }

    public void CastSkill(int id)
    {
        if(!UseMana(mSkillDataArr[id].MP)) { return; }//옵션

        StartCoroutine(Skill(id));//옵션
        StartCoroutine(SkillCooltime(id));//옵션
    }
    private IEnumerator Skill(int id)//옵션
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
        UIController.Instance.ShowPlayerStatusText();
        //Debug.LogFormat("HP: {0}/{1} \nAtk{2} \nDef: {3}", mCurrentHP.ToString("N1"), mMaxHP.ToString("N1"), mATK, mDEF);
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
        UIController.Instance.ShowPlayerStatusText();
        //Debug.LogFormat("HP: {0}/{1} \nAtk{2} \nDef: {3}", mCurrentHP.ToString("N1"), mMaxHP.ToString("N1"), mATK, mDEF);
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
        mCurrentHP = Mathf.Max(mCurrentHP - amount, 0);
        //-----------------작성공간-----------------
        StartCoroutine(UIController.Instance.PlayerHit());
        UIController.Instance.ShowPlayerStatusText();
        //Debug.LogFormat("HP: {0}/{1}", mCurrentHP.ToString("N1"), mMaxHP.ToString("N1"));
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
        UIController.Instance.ShowPlayerStatusText();
        //Debug.LogFormat("MP: {0}/{1}", mCurrentMP.ToString("N1"), mMaxMP.ToString("N1"));
        //-----------------------------------------
    }

    public bool UseMana(float amount)
    {
        if (mCurrentMP >= amount)
        {
            mCurrentMP = Mathf.Max(mCurrentMP - amount, 0);
            //-----------------작성공간-----------------
            UIController.Instance.ShowPlayerStatusText();
            //Debug.LogFormat("MP: {0}/{1}", mCurrentMP.ToString("N1"), mMaxMP.ToString("N1"));
            //-----------------------------------------
            return true;
        }
        else
        {
            //-----------------작성공간-----------------
            string text = "마나가 부족합니다!";
            UIController.Instance.ShowPopup(text);
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
        UIController.Instance.ShowPlayerExp();
        //Debug.LogFormat("Current Level: {0} \nEXP: {1}/{2}", mLevel, mCurrentExp.ToString("N1"), mMaxExp.ToString("N1"));
        //-----------------------------------------
    }

    public void Equip(ItemData data)
    {
        Unequip(data.ItemType);
        //-----------------작성공간-----------------
        for (int i = 0; i < mEquipDataArr.Length; i++)
        {
            if (mEquipDataArr[i] == null)
            {
                
                mEquipDataArr[i] = data;
                mATK += mEquipDataArr[i].ATK;
                mDEF += mEquipDataArr[i].DEF;
                mMaxHP += mEquipDataArr[i].HP;
                mMaxMP += mEquipDataArr[i].MP;
            }
            else
            {
                if (mEquipDataArr[i].ItemType == data.ItemType)
                {
                    mEquipDataArr[i] = data;
                    mATK += mEquipDataArr[i].ATK;
                    mDEF += mEquipDataArr[i].DEF;
                    mMaxHP += mEquipDataArr[i].HP;
                    mMaxMP += mEquipDataArr[i].MP;
                }
            }
        }
        UIController.Instance.ShowPlayerStatusText();
        //Debug.LogFormat("Type: {0} \nMinLevel: {1} \nATK: {2} \nHP: {3} \nMP: {4}", data.ItemType, data.Level, data.ATK, data.DEF, data.HP, data.MP);
        //-----------------------------------------
    }
    public void Unequip(eItemType itemType)
    {
        //-----------------작성공간-----------------
        for (int i=0; i<mEquipDataArr.Length;i++)
        {
            if (mEquipDataArr[i] != null)
            {
                if (mEquipDataArr[i].ItemType == itemType)
                {
                    mATK -= mEquipDataArr[i].ATK;
                    mDEF -= mEquipDataArr[i].DEF;
                    mMaxHP -= mEquipDataArr[i].HP;
                    if (mMaxHP <= mCurrentHP)
                    {
                        mCurrentHP = mMaxHP;
                    }
                    mMaxMP -= mEquipDataArr[i].MP;
                    if (mMaxMP <= mCurrentMP)
                    {
                        mCurrentMP = mMaxMP;
                    }
                }
            }
            
        }
        //Debug.Log(mEquipDataArr[(int)itemType]);
        //-----------------------------------------
    }
}
