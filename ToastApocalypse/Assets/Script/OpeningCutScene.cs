using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpeningCutScene : MonoBehaviour,IPointerClickHandler
{
    public Text mCutSceneText;
    public List<DialogText> mDialogList;
    public Button mSkipButton;
    public Image mCutSceneImage;
    public int TextID;
    public bool SceneCheck,TouchDelay;

    private void Start()
    {
        OpeningSetting();
        ShowOpening();
        StartCoroutine(Delay());
    }

    public void OpeningSetting()
    {
        mDialogList = new List<DialogText>();
        for (int i = 54; i < 63; i++)
        {
            mDialogList.Add(GameSetting.Instance.mDialogArr[i]);
        }
        TextID = 0;
    }

    public void ShowOpening()
    {
        if (TextID>= mDialogList.Count)
        {
            if (SceneCheck==false)
            {
                MainScreenUIController.Instance.OpeningSkip();
            }
            else
            {
                MainLobbyUIController.Instance.OpeningSkip();
            }
        }
        else
        {
            mCutSceneImage.sprite = GameSetting.Instance.Illust[TextID];
            if (GameSetting.Instance.Language == 0)
            {
                mCutSceneText.text = mDialogList[TextID].ContensFormat;
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mCutSceneText.text = mDialogList[TextID].EngContensFormat;
            }
        }
    }

    private IEnumerator Delay()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        TouchDelay = true;
        yield return delay;
        TouchDelay = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TouchDelay==false)
        {
            TextID++;
            ShowOpening();
        }
        StartCoroutine(Delay());
    }
}
