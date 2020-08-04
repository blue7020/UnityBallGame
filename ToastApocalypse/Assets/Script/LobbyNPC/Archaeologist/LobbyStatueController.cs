using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStatueController : InformationLoader
{
    public static LobbyStatueController Instance;

    public StatueStat[] mStatInfoArr;
    public StatueText[] mTextInfoArr;

    public Sprite[] mSprites;

    public StatueStat[] GetStatInfoArr()
    {
        return mStatInfoArr;
    }
    public StatueText[] GetTextInfoArr()
    {
        return mTextInfoArr;
    }

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mStatInfoArr, Path.STATUE_STAT);
            LoadJson(out mTextInfoArr, Path.STATUE_TEXT);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
