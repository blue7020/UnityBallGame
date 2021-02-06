using System.Collections;
using UnityEngine;
using DG.Tweening;
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
            case 1:
                UIController.Instance.ShowTutorial();
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
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
                UIController.Instance.mDialogueImage.transform.localPosition = new Vector3(-20, -249, 0);
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
                StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
                SoundController.Instance.SESound(22);
                mObj[1].gameObject.SetActive(true);
                Player.Instance.GetItem(mObj[0].GetComponent<HoldingItem>());
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 33:
                StartCoroutine(CutScene12());
                break;
            case 34:
                Player.Instance.isNoDamage = true;
                Player.Instance.ShowAction(1);
                mObj[0].gameObject.SetActive(true);
                Player.Instance.mNowItem.gameObject.SetActive(true);
                Player.Instance.mNowItem.transform.SetParent(null);
                Sequence seq = DOTween.Sequence();
                seq.Append(Player.Instance.mRB2D.DOMove(Player.Instance.transform.position + new Vector3(-0.7f, 0, 0), 0.5f));
                Player.Instance.mNowItem.transform.position = Player.Instance.transform.position+new Vector3(-0.2f,0,0);
                seq.Append(Player.Instance.mNowItem.mRB2D.DOMove(new Vector3(Player.Instance.transform.position.x + -1.5f, -0.75f, 0), 0.4f));
                Player.Instance.ShowAction(1);
                seq.Play();
                StartCoroutine(CutScene12_1());
                break;
            case 36:
                StartCoroutine(CutScene12_2());
                break;
            case 37:
                Darkness.Instance.transform.position = new Vector3(515, -146, 0);
                UIController.Instance.mDialogueImage.gameObject.SetActive(true);
                Player.Instance.ShowAction(0);
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 43:
                StartCoroutine(CutScene14());
                break;
            case 45:
                StartCoroutine(CutScene14_1());
                break;
            case 46:
                StartCoroutine(CutScene14_2());
                break;
            case 47:
                StartCoroutine(CutScene15());
                break;
            case 48:
                SoundController.Instance.SESound(22);
                SoundController.Instance.SESound(6);
                Darkness.Instance.MoveCutScene(new Vector3(600f, -80, 0), 5f);
                StartCoroutine(DialogueSystem.Instance.ChatDelay2());
                break;
            case 54:
                StartCoroutine(CutScene15_1());
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

    public IEnumerator CutScene11()//스테이지 1
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Player.Instance.isCutScene = true;
        UIController.Instance.mScreenEffect.gameObject.SetActive(true);
        mObj[0].transform.position = new Vector3(-25.5f, 18.26f, 0);
        mObj[0].GetComponent<HoldingItem>().mItemKeyObj.SetActive(false);
        yield return delay;
        UIController.Instance.mScreenEffect.gameObject.SetActive(false);
        DialogueSystem.Instance.DialogueSetting(25, 32);
    }

    public IEnumerator CutScene12()
    {
        float time = 2.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        UIController.Instance.mDialogueImage.transform.localPosition = new Vector3(-20, -249, 0);
        Player.Instance.mSpeed -= 3f;
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        yield return delay;
        mMoveTrigger = false;
        Player.Instance.mSpeed += 3f;
        StartCoroutine(DialogueSystem.Instance.ChatDelay2());
    }
    public IEnumerator CutScene12_1()
    {
        float time = 2.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        yield return delay;
        Darkness.Instance.transform.localPosition = new Vector3(162, 0, 0);
        Darkness.Instance.Show();
        Darkness.Instance.MoveCutScene(Player.Instance.mNowItem.transform.position, 0.5f);
        SoundController.Instance.SESound(22);
        time = 0.5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        HoldingItem item = Player.Instance.mNowItem;
        Player.Instance.mNowItem.Drop();
        item.mItemKeyObj.gameObject.SetActive(false);
        item.transform.SetParent(Darkness.Instance.transform);
        item.transform.localPosition = new Vector3(0, 0.5f, 0);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        Darkness.Instance.MoveCutScene(new Vector3(185, 1, 0), 2f);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        StartCoroutine(DialogueSystem.Instance.ChatDelay());
    }
    public IEnumerator CutScene12_2()
    {
        float time = 3f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Darkness.Instance.MoveCutScene(new Vector3(199,1,0),3f);
        CameraMovement.Instance.CameraMove(new Vector3(199, 1, 0), 0.2f);
        Player.Instance.mSpeed -= 3f;
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        yield return delay;
        UIController.Instance.mDialogueImage.transform.localPosition = new Vector3(-20, -249, 0);
        time = 2f;
        delay = new WaitForSeconds(time);
        mMoveTrigger = false;
        Player.Instance.mSpeed += 3f;
        yield return delay;
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        SoundController.Instance.SESound(22);
        Darkness.Instance.MoveCutScene(new Vector3(199, -50, 0), 2f);
        time = 1;
        delay = new WaitForSeconds(time);
        yield return delay;
        Player.Instance.mNowItem = null;
        Player.Instance.mNowItemID = -1;
        UIController.Instance.ItemImageChange();
        CameraMovement.Instance.mFollowing = true;
        Player.Instance.isNoDamage = false;
        StartCoroutine(DialogueSystem.Instance.ChatDelay2());
    }

    public IEnumerator CutScene13()
    {
        float time = 2f;
        WaitForSeconds delay = new WaitForSeconds(time);
        UIController.Instance.mDialogueImage.transform.localPosition = new Vector3(-20, 231, 0);
        Quaternion.Euler(new Vector2(0, 0));
        Vector3 pos = new Vector3(160, -32, -10);
        CameraMovement.Instance.CameraMove(pos, 2.5f);
        yield return delay;
        DialogueSystem.Instance.DialogueSetting(39, 39);
        time = 4f;
        delay = new WaitForSeconds(time);
        yield return delay;
        CameraMovement.Instance.mFollowing = true;
    }

    public IEnumerator CutScene14()
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        yield return delay;
        time = 0.5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        UIController.Instance.mDialogueImage.gameObject.SetActive(false);
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        SoundController.Instance.SESound(22);
        yield return delay;
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        SoundController.Instance.SESound(22);
        Darkness.Instance.MoveCutScene(new Vector3(525, -141, 0), 1.5f);
        time = 1.5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        Darkness.Instance.transform.DOScale(new Vector3(2, 2, 1), 1f);
        time = 1f;
        delay = new WaitForSeconds(time);
        yield return delay;
        GameController.Instance.mMapMaterialController.mBoss.gameObject.SetActive(true);
        GameController.Instance.mMapMaterialController.mBoss.transform.position = new Vector3(525.1f, -141.25f, 0);
        GameController.Instance.mMapMaterialController.mBoss.GetComponent<Rigidbody2D>().DOMoveY(-144.4f, 3.5f);
        time = 5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        Darkness.Instance.transform.DOScale(new Vector3(1, 1, 1), 1f);
        StartCoroutine(DialogueSystem.Instance.ChatDelay());
        UIController.Instance.mDialogueImage.gameObject.SetActive(true);
    }
    public IEnumerator CutScene14_1()
    {
        float time = 2f;
        WaitForSeconds delay = new WaitForSeconds(time);
        StartCoroutine(CameraMovement.Instance.Shake(1.5f, 0.15f));
        SoundController.Instance.SESound(22);
        Darkness.Instance.MoveCutScene(new Vector3(575.5f, -144,0),5f);
        yield return delay;
        mObj[0].SetActive(true);
        StartCoroutine(DialogueSystem.Instance.ChatDelay());
    }
    public IEnumerator CutScene14_2()
    {
        float time = 1f;
        WaitForSeconds delay = new WaitForSeconds(time);
        SoundController.Instance.BGMChange(2);
        Player.Instance.CheckPointPos = transform.position;
        GameController.Instance.isBoss = true;
        yield return delay;
        StartCoroutine(DialogueSystem.Instance.ChatDelay());
    }

    public IEnumerator CutScene15()
    {
        float time = 2f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Player.Instance.mSpeed -= 3f;
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        yield return delay;
        mMoveTrigger = false;
        Player.Instance.mSpeed += 3f;
        StartCoroutine(DialogueSystem.Instance.ChatDelay());
    }
    public IEnumerator CutScene15_1()
    {
        float time = 2f;
        WaitForSeconds delay = new WaitForSeconds(time);
        Player.Instance.mSpeed -= 3f;
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        yield return delay;
        mMoveTrigger = false;
        Player.Instance.mSpeed += 3f;
        time = 1f;
        delay = new WaitForSeconds(time);
        UIController.Instance.mNextDialogueText.gameObject.SetActive(false);
        SoundController.Instance.BGMFadeOut(3f);
        yield return delay;
        CutSceneController.Instance.FadeOut();
        time = 2f;
        delay = new WaitForSeconds(time);
        yield return delay;
        UIController.Instance.mDialogueImage.gameObject.SetActive(false);
        TitleController.Instance.isShowTitle = true;
        SaveDataController.Instance.mUser.StageClear[1] = true;
        SaveDataController.Instance.mUser.StageShow[2] = true;
        time = 1f;
        delay = new WaitForSeconds(time);
        yield return delay;
        SceneManager.LoadScene(0);
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
                        DialogueSystem.Instance.DialogueSetting(33, 38);
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
                        DialogueSystem.Instance.DialogueSetting(47, 54);
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
