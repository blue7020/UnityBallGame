using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;//이게 없으면 오디오 믹서를 사용하지 못함.

public class SoundController : MonoBehaviour
{
    private const string MIXER_MASTER = "Master";
    private const string MIXER_BG = "BGM";
    private const string MIXER_FX = "Effect";
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mBGMArr, mEffectArr;
    [SerializeField]
    private AudioMixer mMixer;
    
    public float MasterVolume
    {
        get{
            float vol;
            mMixer.GetFloat(MIXER_MASTER, out vol);
            return vol;
        }
        set{
            float vol = 20f * Mathf.Log10(value);
            mMixer.SetFloat(MIXER_MASTER, vol);
        }
    }

    public float BGMVoluem
    {
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_BG, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            mMixer.SetFloat(MIXER_BG, vol);
        }
    }

    public float EffectVoluem
    {
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_FX, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            mMixer.SetFloat(MIXER_FX, vol);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mEffect.PlayOneShot(mEffectArr[0]);
        }
    }
}
