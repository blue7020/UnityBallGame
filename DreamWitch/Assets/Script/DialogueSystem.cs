using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DialogueSystem : InformationLoader
{
    public static DialogueSystem Instance;


    public DialogueText[] mDialogueTextArr;
    public GameObject mNextPoint; 
    public List<string> mTextList;
    public int NowIndex,EndIndex,BackupNowIndex,
        mNowCutSceneIndex;//<-텍스트 번호가 아닌 현재 컷씬의 번호
    public bool isChatDelay,isDialogue,EndDialogue,isTextBoxShow,isSkip;

    public CutScenePoint mCutScenePoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            NowIndex = -1;
            EndIndex = -1;
            BackupNowIndex = -1;
            LoadJson(out mDialogueTextArr, Paths.DIALOGUE_TEXT);
            mTextList = new List<string>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator ChatDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        if (EndDialogue)
        {
            DialogueCheck();
        }
        else
        {
            mNextPoint.SetActive(false);
            isChatDelay = true;
            ShowDialogue(NowIndex);
            BackupNowIndex = NowIndex;
            mCutScenePoint.SetAction(NowIndex);
            if (NowIndex < EndIndex)
            {
                NowIndex++;
            }
            else
            {
                EndDialogue = true;
            }
        }
        yield return delay;
    }
    public IEnumerator ChatDelay2(float time =1f)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        yield return delay;
        mNextPoint.SetActive(true);
        isChatDelay = false;
    }


    public void DialogueCheck()
    {
        if (EndDialogue)
        {
            NowIndex = -1;
            mCutScenePoint.SetAction(NowIndex);
        }
    }

    public void EndCutScene()
    {
        NowIndex = -1;
        EndIndex = -1;
        BackupNowIndex = -1;
        EndDialogue = false;
        isDialogue = false;
        isTextBoxShow = false;
        mCutScenePoint.EndCutScene();
    }

    public void DialogueSetting(int now,int end)
    {
        NowIndex = now;
        EndIndex = end;
        BackupNowIndex = NowIndex;
        isDialogue = true;
        StartCoroutine(ChatDelay());
    }

    public void ShowDialogue(int id)
    {
        if (id>-1)
        {
            isTextBoxShow = true;
            UIController.Instance.mDialogueFaceImage.sprite = UIController.Instance.mFaceSprite[mDialogueTextArr[id].FaceCode];
            UIController.Instance.mDialogue.text = "";
            string text = "";
            if (TitleController.Instance.mLanguage == 0)
            {
                text = mDialogueTextArr[id].text_kor;
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                text = mDialogueTextArr[id].text_eng;
            }
            UIController.Instance.mDialogue.DOText(text, 0.8f);
            UIController.Instance.mDialogueImage.gameObject.SetActive(true);
        }
    }

    public void Skip()
    {
        switch (mNowCutSceneIndex)
        {
            case 0:
                mCutScenePoint.StopAllCoroutines();
                Player.Instance.mSpeed = 7;
                mCutScenePoint.mMoveTrigger = false;
                UIController.Instance.ShowTutorial();
                Player.Instance.transform.position = new Vector3(-20.90166f, -5.245378f, 0);
                mNowCutSceneIndex = 1;
                EndCutScene();
                break;
            case 5:
                mCutScenePoint.StopAllCoroutines();
                UIController.Instance.HideTutorial();
                SoundController.Instance.mBGM.mute = false;
                Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
                CutSceneController.Instance.ChangeMainCamera();
                mNowCutSceneIndex = 6;
                EndCutScene();
                break;
            case 9:
                mCutScenePoint.StopAllCoroutines();
                mCutScenePoint.mID = 10;
              CutSceneController.Instance.mCutSceneImage.gameObject.SetActive(false);
                StartCoroutine(mCutScenePoint.CutScene10());
                break;
            case 11://1stage
                mCutScenePoint.StopAllCoroutines();
                mCutScenePoint.mObj[0].transform.position = new Vector3(-25.5f, 18.26f, 0);
                mCutScenePoint.mObj[0].GetComponent<HoldingItem>().mItemKeyObj.SetActive(false);
                UIController.Instance.mScreenEffect.gameObject.SetActive(false);
                SoundController.Instance.BGMChange(3);
                Darkness.Instance.Show();
                Darkness.Instance.transform.position =new Vector3(-22.5f, 22.5f, 0);
                Player.Instance.GetItem(mCutScenePoint.mObj[0].GetComponent<HoldingItem>());
                mCutScenePoint.mObj[1].gameObject.SetActive(true);
                mCutScenePoint.mObj[2].gameObject.SetActive(true);
                EndCutScene();
                break;
            case 12:
                mCutScenePoint.StopAllCoroutines();
                mCutScenePoint.mMoveTrigger = false;
                HoldingItem item = Player.Instance.mNowItem;
                Darkness.Instance.transform.position = new Vector3(515, -146, 0);
                Darkness.Instance.mRenderer.color = new Color(1, 1, 1, 1);
                Player.Instance.mSpeed = 7;
                Player.Instance.mNowItem.Drop();
                item.mItemKeyObj.gameObject.SetActive(false);
                item.transform.SetParent(Darkness.Instance.transform);
                item.transform.localPosition = new Vector3(0, 0.5f, 0);
                Player.Instance.mNowItem = null;
                Player.Instance.mNowItemID = -1;
                UIController.Instance.ItemImageChange();
                Player.Instance.isNoDamage = false;
                Player.Instance.transform.position = new Vector3(190.5934f, -0.241164f, 0);
                EndCutScene();
                break;
            case 13:
                mCutScenePoint.StopAllCoroutines();
                mNowCutSceneIndex = -1;
                HoldingItem tem = mCutScenePoint.mObj[0].GetComponent<HoldingItem>();
                tem.mItemKeyObj.gameObject.SetActive(false);
                tem.transform.SetParent(Darkness.Instance.transform);
                tem.transform.localPosition = new Vector3(0, 0.5f, 0);
                CameraMovement.Instance.mFollowing = true;
                Darkness.Instance.transform.position = new Vector3(515, -146, 0);
                Darkness.Instance.Show();
                EndCutScene();
                break;
            case 14:
                mCutScenePoint.StopAllCoroutines();
                Player.Instance.CheckPointPos = transform.position; GameController.Instance.mMapMaterialController.mBoss.gameObject.SetActive(true);      GameController.Instance.mMapMaterialController.mBoss.transform.position =  new Vector3(525.5f,- 144.4f,0);
                Darkness.Instance.gameObject.SetActive(false);
                Darkness.Instance.transform.localScale = new Vector3(1, 1, 1);
                mCutScenePoint.mObj[0].SetActive(true);
                SoundController.Instance.BGMChange(2);
                GameController.Instance.isBoss = true;
                EndCutScene();
                break;
            case 15:
                mCutScenePoint.StopAllCoroutines();
                StartCoroutine(mCutScenePoint.CutSecne15_2());
                break;
            default:
                EndCutScene();
                break;
        }
    }
    private IEnumerator DarknessMove()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
        Darkness.Instance.transform.position = new Vector3(575.5f, -144, 0);
        yield return delay;
        Darkness.Instance.mRB2D.DOMove(new Vector3(575.5f, -144, 0), 0.01f);
    }


    private void Update()
    {
        if (isDialogue)
        {
            if (!isChatDelay)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine(ChatDelay());
                }
            }
        }
        if (isSkip && mCutScenePoint != null && mCutScenePoint.IsSkip)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Skip();
            }
        }
    }
}
