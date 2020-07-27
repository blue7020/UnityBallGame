using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : InformationLoader
{
    public static SkillController Instance;


    public SkillStat[] mStatInfoArr;
    public SkillText[] mTextInfoArr;

    public Sprite[] SkillIcon;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Delete();
        }
        LoadJson(out mStatInfoArr, Path.SKILL_STAT);
        LoadJson(out mTextInfoArr, Path.SKILL_TEXT_STAT);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
