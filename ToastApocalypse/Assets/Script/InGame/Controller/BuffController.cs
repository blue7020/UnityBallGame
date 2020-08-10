using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public static BuffController Instance;

    public const int BUFF_COUNT = 7;

    public Sprite[] mSprite;
    public Buff mBuff;
    public Transform mParents;
    public Buff[] mBuffArr;

    public int buffIndex;

    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
            buffIndex = 0;
            mBuffArr = new Buff[BUFF_COUNT];
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetBuff(int id,int code,eBuffType bufftype,float dura)
    {
        if (buffIndex > 6)
        {
            buffIndex = 0;
            for (int i = 0; i < BUFF_COUNT - 1; i++)
            {
                if (mBuffArr[i+1]!=null)
                {
                    mBuffArr[i] = mBuffArr[i + 1];
                    buffIndex++;
                }
            }
            mBuffArr[buffIndex].Delete();
            mBuffArr[buffIndex] = Instantiate(mBuff, mParents);
            mBuffArr[buffIndex].SetData(buffIndex, code, mSprite[id], bufftype, dura);
            buffIndex++;
        }
        else
        {
            if (buffIndex < 0)
            {
                buffIndex = 0;
            }
            mBuffArr[buffIndex] = Instantiate(mBuff, mParents);
            mBuffArr[buffIndex].SetData(buffIndex, code, mSprite[id], bufftype, dura);
            buffIndex++;
        }
    }

    public void RemoveNurf()
    {
        for (int i = 0; i < mBuffArr.Length; i++)
        {
            if (mBuffArr[i].eType == eBuffType.Nurf)
            {
                mBuffArr[i].Delete();
                buffIndex--;
            }
        }
    }
}
