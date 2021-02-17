using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelecter : MonoBehaviour
{
    public int mID;
    public Text mText;

    public void ButtonSetting(int id)
    {
        mID = id;
        LanguageSetting();
    }

    public void LanguageSetting()
    {
        if (TitleController.Instance.mLanguage == 0)
        {
            mText.text = "챕터 " + (mID + 1);
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            mText.text = "Ch " + (mID + 1);
        }
    }

    public void ChapterSelect()
    {
        TitleController.Instance.NowChapterCode = mID;
        SaveDataController.Instance.Save(false);
        Loading.Instance.StartLoading(1, false);
    }
}
