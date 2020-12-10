using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePoint : MonoBehaviour
{
    public bool mTrigger;
    public int mID;

    public IEnumerator CutScene0()
    {
        float time = 2.5f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        Player.Instance.isCutScene = true;
        Player.Instance.ShowAction(0);
        yield return delay;
        SoundController.Instance.mBGM.mute = true;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        SoundController.Instance.SESound(6);
        Player.Instance.ShowAction(1);
        time = 1f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        CutSceneController.Instance.CutSceneCamera();
        time = 5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.isCutScene = false;
        SoundController.Instance.mBGM.mute=false;
        CutSceneController.Instance.ChangeMainCamera();
    }

    public IEnumerator CutScene1()
    {
        float time = 2.5f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        Player.Instance.isCutScene = true;
        yield return delay;
        Player.Instance.isCutScene = false;
    }

    public void PlayCutScene()
    {
        switch (mID)
        {
            case 0:
                StartCoroutine(CutScene0());
                break;
            case 1:
                StartCoroutine(CutScene1());
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& !mTrigger)
        {
            mTrigger = true;
            PlayCutScene();
        }
    }
}
