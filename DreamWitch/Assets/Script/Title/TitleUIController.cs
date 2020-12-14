using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    public static TitleUIController Instance;
    public Image mTitle;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (TitleController.Instance.isShowTitle==true)
        {
            StartCoroutine(ShowTitle());
        }
    }

    public IEnumerator ShowTitle()
    {
        WaitForSeconds delay = new WaitForSeconds(5f);
        SoundController.Instance.mBGM.Stop();
        TitleController.Instance.isShowTitle = false;
        mTitle.gameObject.SetActive(true);
        SoundController.Instance.SESound(17);
        yield return delay;
        //SoundController.Instance.BGMChange(1);
        mTitle.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
        SoundController.Instance.BGMChange(0);
    }
}
