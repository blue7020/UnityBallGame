using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScenePoint : MonoBehaviour
{
    public bool mTrigger,IsSkip,IsPreStart;
    public int mID,mStartIndex,mEndIndex;
    private bool mMoveTrigger;

    private void Start()
    {
        if (CutSceneController.Instance.mCutSceneList[mID] == true)
        {
            mTrigger = true;
        }
    }

    public void PreStartAction()
    {
        switch (mID)
        {
            case 0:
                mTrigger = true;
                StartCoroutine(CutScene0());
                break;
            case 5:
                mTrigger = true;
                StartCoroutine(CutScene5());
                break;
            case 7:
                mTrigger = true;
                Player.Instance.ShowAction(4);
                CameraMovement.Instance.CameraMove(new Vector3(195, 1.5f, 0), 1.5f);
                DialogueSystem.Instance.DialogueSetting(15, 16);
                break;
            case 9:
                mTrigger = true;
                StartCoroutine(CutScene9());
                break;
            default:
                break;
        }
    }

    public void SetAction(int index)
    {
        switch (index)
        {
            case -1:
                DialogueSystem.Instance.EndCutScene();
                break;
            case 2:
                Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 3:
                CameraMovement.Instance.CameraMove(Player.Instance.transform.position + new Vector3(0, 4f, 0), 2f);
                Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
                StartCoroutine(DialogueSystem.Instance.ChatDelay2(1f));
                break;
            case 4:
                CameraMovement.Instance.mFollowing = true;
                Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 7:
                CameraMovement.Instance.CameraMove(new Vector3(19, 1.25f, 0), 1f);
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 10:
                CameraMovement.Instance.CameraMove(new Vector3(111, 14.5f, 0), 1f);
                StartCoroutine(DialogueSystem.Instance.ChatDelay2(1f));
                break;
            case 11:
                CameraMovement.Instance.mFollowing = true;
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 13:
                StartCoroutine(CutScene5_2());
                break;
            case 20:
                SoundController.Instance.mBGM.mute = false;
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 21:
                CutSceneController.Instance.CloseCutSceneImage();
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 23:
                mID = 10;
                PlayCutScene();
                break;
            default:
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
        }
    }

    public IEnumerator CutScene0()
    {
        float time = 0.5f;
        mTrigger = true;
        WaitForSeconds delay = new WaitForSeconds(time);
        Player.Instance.mSpeed -= 1.5f;
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        DialogueSystem.Instance.DialogueSetting(0, 1);
        yield return delay;
        DialogueSystem.Instance.isChatDelay = false;
        time = 1.5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        mMoveTrigger = false;
        Player.Instance.mSpeed += 1.5f;
        yield return delay;
        UIController.Instance.ShowTutorial();
    }
    public IEnumerator MovePlayer()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        while (mMoveTrigger)
        {
            Player.Instance.Moving(1);
            if (!mMoveTrigger)
            {
                break;
            }
            yield return delay;
        }
        Player.Instance.mRB2D.velocity = new Vector2(Player.Instance.mRB2D.velocity.x, Player.Instance.mRB2D.velocity.y);
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
    }

    public IEnumerator CutScene5()
    {
        float time = 2f;
        WaitForSeconds delay = new WaitForSeconds(time);
        UIController.Instance.HideTutorial();
        Player.Instance.ShowAction(0);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        Player.Instance.ShowAction(1);
        DialogueSystem.Instance.DialogueSetting(12, 13);
    }
    public IEnumerator CutScene5_2()
    {
        float time = 4f;
        WaitForSeconds delay = new WaitForSeconds(time);
        SoundController.Instance.mBGM.mute = true;
        SoundController.Instance.SESound(6);
        CutSceneController.Instance.CutSceneCamera();
        yield return delay;
        CutSceneController.Instance.ChangeMainCamera();
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        time = 0.75f;
        delay = new WaitForSeconds(time);
        yield return delay;
        SoundController.Instance.mBGM.mute = false;
        StartCoroutine(DialogueSystem.Instance.ChatDelay2());
    }

    public IEnumerator CutScene9()
    {
        float time = 1.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        CutSceneController.Instance.FadeOut();
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        Player.Instance.mNowItem.gameObject.SetActive(true);
        Player.Instance.mNowItem.transform.SetParent(null);
        Player.Instance.mNowItem.transform.position = new Vector3(Player.Instance.transform.position.x+0.5f, 5.26f,0);
        Player.Instance.mNowItem.mRenderer.sortingOrder = 6;
        CutSceneController.Instance.FadeIn();
        time = 2f;
        delay = new WaitForSeconds(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        Player.Instance.ShowAction(1);
        time = 1.5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        CutSceneController.Instance.ShowCutSceneImage(0);
        time = 1f;
        delay = new WaitForSeconds(time);
        yield return delay;
        SoundController.Instance.mBGM.mute = true;
        SoundController.Instance.SESound(6);
        StartCoroutine(CutSceneController.Instance.FadeinCutSceneImage(1));
        time = 3f;
        delay = new WaitForSeconds(time);
        yield return delay;
        DialogueSystem.Instance.DialogueSetting(20, 23);
    }

    public IEnumerator CutScene10()
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        SoundController.Instance.BGMFadeOut(3f);
        TitleController.Instance.isShowTitle = true;
        yield return delay;
        CutSceneController.Instance.FadeOut();
        TitleController.Instance.TutorialClear = true;
        time = 4f;
        delay = new WaitForSeconds(time);
        yield return delay;
        GameController.Instance.GotoStageSelect(0);
    }

    public void PlayCutScene()
    {
        CutSceneController.Instance.mCutSceneList[mID] = true;
        DialogueSystem.Instance.mCutScenePoint = this;
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        if (IsSkip)
        {
            UIController.Instance.mSkipText.gameObject.SetActive(true);
        }
        if (IsPreStart)
        {
            PreStartAction();
        }
        else
        {
            switch (mID)
            {
                case 0:
                    mTrigger = true;
                    break;
                case 1:
                    mTrigger = true;
                    DialogueSystem.Instance.DialogueSetting(2, 4);
                    break;
                case 2:
                    mTrigger = true;
                    DialogueSystem.Instance.DialogueSetting(5, 6);
                    break;
                case 3:
                    mTrigger = true;
                    DialogueSystem.Instance.DialogueSetting(7, 9);
                    break;
                case 4:
                    mTrigger = true;
                    DialogueSystem.Instance.DialogueSetting(10, 11);
                    break;
                case 5:
                    mTrigger = true;
                    break;
                case 6:
                    mTrigger = true;
                    DialogueSystem.Instance.DialogueSetting(14, 14);
                    break;
                case 7:
                    mTrigger = true;
                    break;
                case 8:
                    mTrigger = true;
                    DialogueSystem.Instance.DialogueSetting(17, 19);
                    break;
                case 9:
                    if (Player.Instance.mNowItemID == 0)
                    {
                        mTrigger = true;
                    }
                    break;
                default:
                    mTrigger = true;
                    break;
            }
        }
    }

    public void EndCutScene()
    {
        Player.Instance.isCutScene = false;
        CameraMovement.Instance.mFollowing = true;
        DialogueSystem.Instance.mCutScenePoint = null;
        UIController.Instance.mSkipText.gameObject.SetActive(false);
        UIController.Instance.mDialogueImage.gameObject.SetActive(false);
        GameController.Instance.ShowUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& !mTrigger)
        {
            PlayCutScene();
        }
    }
}
