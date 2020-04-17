using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSFXType
{
    ExpAst,
    ExpEnemy,
    ExpPlayer,
    FireEnemy,
    FirePlayer
}

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mBGMClip, mEffectClip;

    // Start is called before the first frame update
    void Start()
    {
        //AudioClip 로드
        //Audio Setting 로드
    }
    public void SetBGMVolume(float value)
    {
        mBGM.volume = value;
        
    }

    public void SetEffectVolume(float value)
    {
        //볼륨값은 0~1 사이의 값(== 퍼센트)
        mEffect.volume = value;
    }

    public void ChangeBGM(int index)
    {
        mBGM.Stop();
        mBGM.clip = mBGMClip[index];
        mBGM.Play();
        //혹시 모르니 현재 bgm을 정지하고 바꾼 후, 재생하는 것이 낫다.
    }

    public void PlayEffectSound(int index)
    {
        //이펙트를 재생할 때는 이렇게 하는 것이 좋다.
        mEffect.PlayOneShot(mEffectClip[index]);
        //AudioSource.PlayClipAtPoint(mEffectClip[index], Vector3.zero); //효과는 같지만 Garbage가 쌓인다. 하이어라키에 오브젝트가 나타나고 Destroy되기 때문에 비추천
    }
}
