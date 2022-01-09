using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class VersionCheckManager : MonoBehaviour
{
    private static VersionCheckManager Instance;


    public GameObject mPopUpWindow;
    public string url,VersionUrl;              //데이터를 가져올 URL

    public string CurVersion; // 현재 빌드버전
    string latsetVersion; // 최신버전
    public bool isTestmode;

    public Text mTitle, mButtonText, mToolTipText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SaveDataController.Instance.mLanguage == 1)//Korean
        {
            mTitle.text = "새 소식";
            mToolTipText.text = "<b><color=red>새 버전이 있습니다!</color></b>\n\n업데이트 내용은\n스토어의 설명을\n참고해 주세요!";
            mButtonText.text = "스토어로 이동";
        }
        else
        {
            mTitle.text = "NEWS";
            mToolTipText.text = "<b><color=red>New version availabe!</color></b>\n\nPlease refer to the\nstore's explanation\nfor the update!";
            mButtonText.text = "Go to store";
        }
        CurVersion = Application.version;
        if (isTestmode)
        {
            latsetVersion = "0.1";
            VersionCheck();
        }
        else
        {
            StartCoroutine(LoadTxtData(VersionUrl));
        }
    }

    public void VersionCheck()
    {
        if (CurVersion != latsetVersion)
        {
            mPopUpWindow.SetActive(true);
        }
        else
        {
            mPopUpWindow.SetActive(false);
        }
        Debug.Log("Cur: "+ CurVersion + "Lastet: "+ latsetVersion);
    }


    IEnumerator LoadTxtData(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest(); // 페이지 요청

        if (www.isNetworkError)
        {
            Debug.Log("error get page");
        }
        else
        {
            latsetVersion = www.downloadHandler.text; // 웹에 입력된 최신버전
            //웹페이지 소스코드 전부를 긁어오므로 주의
            VersionCheck();
        }
    }

    public void OpenURL() // 스토어 열기
    {
        Application.OpenURL(url);
    }
}
