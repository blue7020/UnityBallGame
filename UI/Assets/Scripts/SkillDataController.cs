using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataController : CSVLoader
{
    public static SkillDataController Instance;
    private SkillData[] mInfoArr;
    private Sprite[] mSpriteArr;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            LoadCSV(out mInfoArr, "CSVFiles/SkillTable");
            mSpriteArr = Resources.LoadAll<Sprite>("Skill");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public SkillData GetSkillData(int id)
    {
        return mInfoArr[id].GetClone();
    }

    public Sprite GetSprite(int id)
    {
        return mSpriteArr[id];
    }
}
