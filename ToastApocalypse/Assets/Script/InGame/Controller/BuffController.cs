using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public static BuffController Instance;

    public const int BUFF_COUNT = 14;

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

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetBuff(int id,int code,eBuffType bufftype,float dura)
    {
        bool buffChecker = false;
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
            mBuffArr[buffIndex].SetData(buffIndex, code, mSprite[id], bufftype, dura, buffIndex);
            buffIndex++;
        }
        else
        {

            if (buffIndex < 0)
            {
                buffIndex = 0;
            }
            for (int i = 0; i < mBuffArr.Length; i++)
            {
                if (mBuffArr[i]!=null&&mBuffArr[i].mBuffCode==code)
                {
                    mBuffArr[i].Delete();
                    mBuffArr[i] = Instantiate(mBuff, mParents);
                    mBuffArr[i].SetData(i, code, mSprite[id], bufftype, dura, i);
                    buffChecker = true;
                    break;
                }
            }
            if (buffChecker==false)
            {
                mBuffArr[buffIndex] = Instantiate(mBuff, mParents);
                mBuffArr[buffIndex].SetData(buffIndex, code, mSprite[id], bufftype, dura, buffIndex);
                buffIndex++;
            }
        }
    }

    public void RemoveNurf()
    {
        for (int i = 0; i < mBuffArr.Length; i++)
        {
            if (mBuffArr[i]!=null&& mBuffArr[i].eType == eBuffType.Debuff)
            {
                mBuffArr[i].Delete();
                buffIndex--;
            }
        }
    }

    public void RemoveAll()
    {
        for (int i = 0; i < mBuffArr.Length; i++)
        {
            if (mBuffArr[i] != null)
            {
                if (mBuffArr[i].mBuffCode==14)
                {
                    Player.Instance.InfiniteAmmo = false;
                }
                if (mBuffArr[i].mBuffCode == 15)
                {
                    Player.Instance.PlusBoltCount -= 1;
                }
                mBuffArr[i].Delete();
                buffIndex--;
            }
        }
    }
}
