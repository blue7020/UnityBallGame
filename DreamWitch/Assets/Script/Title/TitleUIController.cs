using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    public static TitleUIController Instance;
    public Image mTitle,mNoticeImage,mTitleLogoImage;
    public Sprite[] mTitleLanguageSpriteArr;
    public Text mKeyText;
    public float mAlphaAnimPeriod = 2;
    public SpriteRenderer mTitleObj;


    private void OnEnable()
    {
        if (TitleController.Instance.isShowNotice == true)
        {
            mNoticeImage.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (TitleController.Instance.mLanguage==0)
            {
                mTitleObj.sprite = mTitleLanguageSpriteArr[1];
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                mTitleObj.sprite = mTitleLanguageSpriteArr[0];
            }
            mKeyText.text = "Press SpaceBar";
            StartCoroutine(AlphaAnim());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (TitleController.Instance.isShowNotice == false)
        {
            StartCoroutine(ShowNotice());
        }
        if (TitleController.Instance.isShowTitle==true)
        {
            StartCoroutine(ShowTitle());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!TitleController.Instance.isShowTitle && TitleController.Instance.isShowNotice)
            {
                GameStart();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            SaveDataController.Instance.Save(false);
            Application.Quit();
        }
    }

    int ClickCount = 0;
    void DoubleClick()
    {
        ClickCount = 0;
    }

    public IEnumerator ShowNotice()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(4f);
        yield return delay;
        mNoticeImage.gameObject.SetActive(false);
        TitleController.Instance.isShowNotice = true;
        SoundController.Instance.BGMChange(1);
    }

    public IEnumerator ShowTitle()
    {
        WaitForSeconds delay = new WaitForSeconds(5f);
        SoundController.Instance.mBGM.Stop();
        switch (TitleController.Instance.mLanguage)
        {
            case 0:
                mTitleLogoImage.sprite = mTitleLanguageSpriteArr[1];
                break;
            default:
                mTitleLogoImage.sprite = mTitleLanguageSpriteArr[0];
                break;
        }
        mTitle.gameObject.SetActive(true);
        SoundController.Instance.SESound(17);
        yield return delay;
        SoundController.Instance.BGMChange(1);
        mTitle.gameObject.SetActive(false);
        TitleController.Instance.isShowTitle = false;
        SaveDataController.Instance.Save();
    }

    public void GameStart()
    {
        if (SaveDataController.Instance.mUser.StageClear[0]==true)
        {
            Loading.Instance.StartLoading(2);
        }
        else
        {
            Loading.Instance.StartLoading(1);
            SoundController.Instance.BGMChange(0);
        }
    }

    public IEnumerator AlphaAnim()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        bool Ascending = true;
        float halfTime = mAlphaAnimPeriod / 2;
        Color color = new Color(0, 0, 0, 1 / halfTime * Time.fixedDeltaTime);
        while (true)
        {
            yield return delay;
            if (Ascending)
            {
                mKeyText.color += color;
                if (mKeyText.color.a>=1)
                {
                    Ascending = false;
                }
            }
            else
            {
                mKeyText.color -= color;
                if (mKeyText.color.a<=0)
                {
                    Ascending = true;
                }
            }
        }
    }
}
