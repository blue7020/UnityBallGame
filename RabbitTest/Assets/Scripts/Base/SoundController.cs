using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    private const string MIXER_MASTER = "Master";
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

    private void Start()
    {
        BGMChange(0);
    }

    public void Mute()
    {
        if (SaveDataController.Instance.mUser.Mute == true)
        {
            mBGM.mute = false;
            mSE.mute = false;
            SaveDataController.Instance.mUser.Mute = false;
        }
        else
        {
            mBGM.mute = true;
            mSE.mute = true;
            SaveDataController.Instance.mUser.Mute = true;
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

}
