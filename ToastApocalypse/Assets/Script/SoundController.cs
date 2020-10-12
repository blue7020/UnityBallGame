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
    public AudioSource mBGM, mSE,mBGSE;
    public AudioClip[] mBGMArr, mSEArr, mUISEArr;
    public AudioMixer mMixer;

    public float BGMVolume;
    public float SEVolume;
    public int UIBGMVol;
    public int UISEVol;

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

    private void Start()
    {
        BGMChange(0);
        if (BGMVolume == 1)
        {
            UIBGMVol = 10;
        }
        else
        {
            UIBGMVol = (int)(10 * BGMVolume);
        }
        if (BGMVolume == 1)
        {
            UISEVol = 10;
        }
        else
        {
            UISEVol = (int)(10 * SEVolume);
        }
        mBGM.volume = BGMVolume;
        mSE.volume = SEVolume;
        mBGSE.volume = SEVolume;
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
    public void SESoundLong(int id)
    {
        mBGSE.clip = mSEArr[id];
        mBGSE.Play();
    }

    public void SESoundUI(int id)
    {
        mSE.PlayOneShot(mUISEArr[id]);
    }

    public void PlusBGM()
    {
        if (UIBGMVol < 10)
        {
            UIBGMVol += 1;
            BGMVolume += 0.1f;
            mBGM.volume = BGMVolume;
        }
    }
    public void MinusBGM()
    {
        if (UIBGMVol > 0)
        {
            UIBGMVol -= 1;
            BGMVolume -= 0.1f;
            mBGM.volume = BGMVolume;
        }
    }

    public void PlusSE()
    {
        if (UISEVol < 10)
        {
            UISEVol += 1;
            SEVolume += 0.1f;
            mSE.volume = SEVolume;
            mBGSE.volume = SEVolume;
        }
    }
    public void MinusSE()
    {
        if (UISEVol > 0)
        {
            UISEVol -= 1;
            SEVolume -= 0.1f;
            mSE.volume = SEVolume;
            mBGSE.volume = SEVolume;
        }
    }
}
