using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionCheckManager : MonoBehaviour
{
    private static VersionCheckManager Instance;


    public Transform mPopupTransform;
    public GameObject mPopupCheckerObj;        //팝업 : 버전체크 업데이트 프리팹
    public Text mVersionText,mTitleText,mTooltipText,mButtonText;
    public string url;              //데이터를 가져올 URL

    //유니티 자체에서 bundleIdentifier를 읽을수도 있지만, 이렇게 읽을 수 도 있다.
    public string _bundleIdentifier { get { return url.Substring(url.IndexOf("details"), url.LastIndexOf("details") + 1); } }


    [HideInInspector]
    public bool isSamePlayStoreVersion = false;

    //public bool isTestMode = false;        //테스트 모드 여부

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


    private void Start()
    {
#if UNITY_EDITOR
        isSamePlayStoreVersion = true;
#elif UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
                    StartCoroutine(PlayStoreVersionCheck());
#endif

        mVersionText.text = "Version: " + Application.version;
        if (SaveDataController.Instance.mLanguage == 1)//Korean
        {
            mTitleText.text = "새 소식";
            mTooltipText.text = "<b><color=red>새 버전이 있습니다!</color></b>\n업데이트 내역은\n스토어의 설명을 참고해 주세요!";
            mButtonText.text = "업데이트 하기";
        }
        else
        {
            mTitleText.text = "NEWS";
            mTooltipText.text = "<b><color=red>There's a new version!</color></b>\nPlease refer to the store's\nexplanation for the update!";
            mButtonText.text = "Update";
        }
    }

    /// <summary>
    /// 버전체크를 하여, 강제업데이트를 체크한다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayStoreVersionCheck()
    {
        WWW www = new WWW(url);
        yield return www;

        //인터넷 연결 에러가 없다면, 
        if (www.error == null)
        {
            int index = www.text.IndexOf("softwareVersion");
            string versionText = www.text.Substring(index, 30);

            //플레이스토어에 올라간 APK의 버전을 가져온다.
            int softwareVersion = versionText.IndexOf(">");
            string playStoreVersion = versionText.Substring(softwareVersion + 1, Application.version.Length + 1);

            //버전이 같다면,
            if (playStoreVersion.Trim().Equals(Application.version))
            {
                //게임 씬으로 넘어간다.
                Debug.LogWarning("true : " + playStoreVersion + " : " + Application.version);

                //버전이 같다면, 앱을 넘어가도록 한다.
                isSamePlayStoreVersion = true;
            }
            else
            {
                //버전이 다르므로, 마켓으로 보낸다.
                Debug.LogWarning("false : " + playStoreVersion + " : " + Application.version);

                //업데이트 팝업을 연결한다.
                Instantiate(mPopupCheckerObj, mPopupTransform, false);
            }
        }
        else
        {
            //인터넷 연결 에러시
            Debug.LogWarning(www.error);
            //PopupCreateMgr.GetInstance().Create_ConfirmPopup("인터넷 연결 안내", "인터넷 연결이 되지 않았습니요.\n인터넷 연결을 확인하니요.","다시 연결하기", mPopupTransform, "OnClick_ReConnectionVersionChecker");
        }
    }

    /// <summary>
    /// 업데이트 팝업에서 업데이트 여부를 체크한다.
    /// </summary>
    public void Call_PlayStoreVersionCheck()
    {
        StartCoroutine(PlayStoreVersionCheck());
    }
}
