using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public static Loading Instance;

    public GameObject mSavingImage;
    public Canvas mCanvas;
    public Image mBlackScreen;
    public Text mProgressText;

    public Slider mSlider;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartSaving()
    {
        mSavingImage.SetActive(true);
    }

    public void StartLoading(int sceneIndex)
    {
        StartCoroutine(ShowLoading(sceneIndex));
    }

    public IEnumerator ShowLoading(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        mBlackScreen.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress/0.9f);
            mSlider.value = progess;
            mProgressText.text = (progess * 100f) + "%";
            yield return null;
        }
        mBlackScreen.gameObject.SetActive(false);
        SaveDataController.Instance.Save(true);
    }
}
