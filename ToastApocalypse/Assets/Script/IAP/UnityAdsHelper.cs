using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsHelper : MonoBehaviour
{
    public static UnityAdsHelper Instance;

    private const string android_game_id = "4272433";
    private const string ios_game_id = "4272432";

    private const string Interstitial = "interstitial";

    private Coroutine mCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Advertisement.Initialize(android_game_id);
        //Initialize();
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
#endif
    }

    private void OnDestroy()
    {
        if (mCoroutine != null)
        {
            StopCoroutine(mCoroutine);
        }
    }



    public void Show()
    {
        if (mCoroutine != null)
        {
            StopCoroutine(mCoroutine);
        }
        mCoroutine = StartCoroutine(ShowInterstitialInitialized());
    }

    public IEnumerator ShowInterstitialInitialized()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.5f);
        while (!Advertisement.isInitialized || !Advertisement.IsReady())
        {
            yield return wait;
        }
        Advertisement.Show();
    }
}
