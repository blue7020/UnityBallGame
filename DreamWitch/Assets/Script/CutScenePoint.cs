using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScenePoint : MonoBehaviour
{
    public int mNowStage;
    public bool mTrigger, IsSkip, IsPreStart;
    public int mID;
    private bool mMoveTrigger;

    public GameObject[] mObj;

    public void PreStartAction()
    {
        if (mNowStage == 0)
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
                    if (Player.Instance.mNowItemID == 0)
                    {
                        mTrigger = true;
                        StartCoroutine(CutScene9());
                    }
                    else
                    {
                        DialogueSystem.Instance.DialogueSetting(20, 20);
                    }
                    break;
                default:
                    break;
            }
        }
        else if (mNowStage == 1)
        {
            switch (mID)
            {
                case 0:
                    mTrigger = true;
                    StartCoroutine(CutScene11());
                    break;
                default:
                    break;
            }
        }


    }

    public void SetAction(int index)
    {
        switch (index)//대사 번호로만 판단
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
            case 21:
                SoundController.Instance.mBGM.mute = false;
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 23:
                CutSceneController.Instance.CloseCutSceneImage();
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 24:
                mID = 10;
                StartCoroutine(CutScene10());
                break;
            case 25://여기부터 스테이지 1
                Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
                Player.Instance.ShowAction(0);
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 26:
                Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 31:
                Player.Instance.ShowAction(1);
                Darkness.Instance.Show();
                SoundController.Instance.SESound(6);
                Darkness.Instance.MoveCutScene(new Vector3(-22.5f, 22.5f,0),2f);
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 32:
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                StartCoroutine(CutScene11_1());
                break;
            case 33:
                StartCoroutine(CutScene12());
                break;
            case 34:
                StartCoroutine(CutScene12_1());
                break;
            case 36:
                StartCoroutine(CutScene12_2());
                break;
            case 37:
                Player.Instance.ShowAction(0);
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 38:
                DialogueSystem.Instance.EndCutScene();
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
            Vector3 move = new Vector3(1, 0f, 0f);
            Player.Instance.transform.position += move * Time.deltaTime * Player.Instance.mSpeed;
            Player.Instance.mAnim.SetFloat("xVelocity", 1);
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
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        Player.Instance.mNowItem.gameObject.SetActive(true);
        Player.Instance.mNowItem.transform.SetParent(null);
        Player.Instance.mNowItem.transform.position = new Vector3(Player.Instance.transform.position.x + 0.5f, 5.26f, 0);
        Player.Instance.mNowItem.mRenderer.sortingOrder = 6;
        CutSceneController.Instance.FadeIn();
        time = 2f;
        delay = new WaitForSeconds(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        Player.Instance.ShowAction(1);
        time = 2f;
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
        DialogueSystem.Instance.DialogueSetting(21, 24);
    }

    public IEnumerator CutScene10()
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        UIController.Instance.mNextDialogueText.gameObject.SetActive(false);
        SoundController.Instance.BGMFadeOut(3f);
        TitleController.Instance.isShowTitle = true;
        yield return delay;
        CutSceneController.Instance.FadeOut();
        time = 2f;
        delay = new WaitForSeconds(time);
        yield return delay;
        UIController.Instance.mDialogueImage.gameObject.SetActive(false);
        SaveDataController.Instance.mUser.StageClear[0] = true;
        SaveDataController.Instance.mUser.StageShowEvent[0] = true;
        SaveDataController.Instance.mUser.StageShow[0] = true;
        SaveDataController.Instance.mUser.StageShow[1] = true;
        time = 1f;
        delay = new WaitForSeconds(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowStageClear());
    }

    public IEnumerator CutScene11()
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Player.Instance.isCutScene = true;
        UIController.Instance.mScreenEffect.gameObject.SetActive(true);
        mObj[0].transform.position = new Vector3(-25.5f, 18.26f, 0);
        yield return delay;
        UIController.Instance.mScreenEffect.gameObject.SetActive(false);
        DialogueSystem.Instance.DialogueSetting(25, 32);
    }
    public IEnumerator CutScene11_1()
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        SoundController.Instance.SESound(6);
        mObj[1].gameObject.SetActive(true);
        Player.Instance.GetItem(mObj[0].GetComponent<HoldingItem>());
        yield return delay;
        DialogueSystem.Instance.EndCutScene();
        Darkness.Instance.Moving();
        UIController.Instance.mDialogueImage.transform.position = new Vector3(-20, 231, 0);
    }

    public IEnumerator CutScene12()
    {
        float time = 2.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Player.Instance.mSpeed -= 3f;
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        yield return delay;
        mMoveTrigger = false;
        Player.Instance.mSpeed += 3f;
        DialogueSystem.Instance.isChatDelay = false;
    }
    public IEnumerator CutScene12_1()
    {
        float time = 2.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Player.Instance.isNoDamage = true;
        Player.Instance.ShowAction(1);
        mObj[0].gameObject.SetActive(true);
        //프레스톤 떨굼
        yield return delay;
        time = 0.5f;
        delay = new WaitForSeconds(time);
        Darkness.Instance.Show();
        //먹구름이 뒤에서 나타나 프레스톤 위치로 이동
        yield return delay;
        //프레스톤이 먹구름 하위로 들어간 후 먹구름 이동해서 플레이어 앞에섬
        //두트윈으로 먹구름 이동 설정하기
        time = 1f;
        delay = new WaitForSeconds(time);
        yield return delay;
        Player.Instance.isNoDamage = false;
        DialogueSystem.Instance.DialogueSetting(37, 38);
    }
    public IEnumerator CutScene12_2()
    {
        float time = 2.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        //먹구름을 따라감, 먹구름도 이동하며 동굴 속으로 들어감
        yield return delay;
        DialogueSystem.Instance.isChatDelay = false;
        Darkness.Instance.transform.position = new Vector3(515,-146,0);
    }

    public IEnumerator CutScene13()
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Quaternion.Euler(new Vector2(0, 180f));
        DialogueSystem.Instance.DialogueSetting(39, 39);
        yield return delay;
        time = 4f;
        delay = new WaitForSeconds(time);
        Vector3 pos = new Vector3(160,-32, -10);
        CameraMovement.Instance.CameraMove(pos, 2.5f);
        yield return delay;
        CameraMovement.Instance.mFollowing = true;
        DialogueSystem.Instance.isChatDelay = false;
    }

    public IEnumerator CutScene14()
    {
        float time = 2f;
        WaitForSeconds delay = new WaitForSeconds(time);
        yield return delay;
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
            if (mNowStage == 0)
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
                        else
                        {
                            DialogueSystem.Instance.DialogueSetting(20, 20);
                        }
                        break;
                    default:
                        mTrigger = true;
                        break;
                }
            }
            else if (mNowStage == 1)
            {
                switch (mID)
                {
                    case 0:
                        mTrigger = true;
                        break;
                    case 1:
                        UIController.Instance.mDialogueImage.transform.position = new Vector3(-20, -249, 0);
                        DialogueSystem.Instance.DialogueSetting(33, 36);
                        mTrigger = true;
                        break;
                    case 2:
                        mTrigger = true;
                        StartCoroutine(CutScene13());
                        break;
                    case 3:
                        mTrigger = true;
                        DialogueSystem.Instance.DialogueSetting(40, 40);
                        break;
                    case 4:
                        mTrigger = true;
                        DialogueSystem.Instance.DialogueSetting(41, 41);
                        break;
                    case 5:
                        mTrigger = true;
                        DialogueSystem.Instance.DialogueSetting(42, 46);
                        break;
                    case 6:
                        mTrigger = true;
                        DialogueSystem.Instance.DialogueSetting(47, 53);
                        break;
                    default:
                        mTrigger = true;
                        break;
                }
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
        if (other.gameObject.CompareTag("Player") && !mTrigger)
        {
            PlayCutScene();
        }
    }
}
