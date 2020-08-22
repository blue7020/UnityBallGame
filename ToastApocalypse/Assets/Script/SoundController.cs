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
        BGMChange(0);
        UIBGMVol = 3;
        UISEVol = 3;
        BGMVolume = 0.3f;
        SEVolume = 0.3f;
        mBGM.volume = BGMVolume;
        mSE.volume = SEVolume;
    }

    public void BGMChange(int id)
    {
        mBGM.clip = mBGMArr[id];
        mBGM.Play();
    }

    public void PlusBGM()
    {
        if (BGMVolume < 1f)
        {
            UIBGMVol += 1;
            BGMVolume += 0.1f;
            mBGM.volume = BGMVolume;
        }
        else
        {
            UIBGMVol += 10;
            BGMVolume = 1f;
            mBGM.volume = 1f;
        }
    }
    public void MinusBGM()
    {
        if (BGMVolume > 0)
        {
            UIBGMVol -= 1;
            BGMVolume -= 0.1f;
            mBGM.volume = BGMVolume;
        }
        else
        {
            UIBGMVol = 0;
            BGMVolume = 0f;
            mBGM.volume = 0f;
        }
    }

    public void PlusSE()
    {
        if (SEVolume < 1f)
        {
            UISEVol += 1;
            SEVolume += 0.1f;
            mSE.volume = SEVolume;
        }
        else
        {
            UISEVol = 10;
            SEVolume = 1f;
            mSE.volume = 1f;
        }
    }
    public void MinusSE()
    {
        if (SEVolume > 0)
        {
            UISEVol -= 1;
            SEVolume -= 0.1f;
            mSE.volume = SEVolume;
        }
        else
        {
            UISEVol = 0;
            SEVolume += 0f;
            mSE.volume = 0f;
        }
    }
}
