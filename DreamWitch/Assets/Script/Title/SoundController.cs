using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    private const string MIXER_MASTER = "Master";
    private const string MIXER_BG = "BGM";
    private const string MIXER_FX = "SE";

    public AudioSource mBGM, mSE;
    public AudioClip[] mBGMArr, mSEArr;
    public AudioMixer mMixer;

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

    public void BGMChange(int id)
    {
        mBGM.clip = mBGMArr[id];
        mBGM.Play();
    }

    public void SESound(int id)
    {
        mSE.PlayOneShot(mSEArr[id]);
    }

    public void BGMFadeOut(float time)
    {
        StartCoroutine(FadeOut(time));
    }
    private IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = mBGM.volume;

        while (mBGM.volume > 0)
        {
            mBGM.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        mBGM.Stop();
        mBGM.volume = startVolume;
    }
}
