using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffSelectController : MonoBehaviour
{
    public static BuffSelectController Instance;

    public Image mWindow;
    public Text mTitle, mTooltip;
    public Text[] mBuffTextArr;
    public BuffSelect[] mBuffImageArr;
    public List<int> mBuffList;

    public Sprite[] mBuffIconArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            mBuffList = new List<int>();
            for (int i=0; i< mBuffIconArr.Length;i++)
            {
                mBuffList.Add(i);
            }
            if (GameSetting.Instance.Language==0)
            {
                mTitle.text = "버프 선택";
                mTooltip.text = "터치하여 받을 효과를 선택할 수 있습니다";
            }
            else
            {
                mTitle.text = "Buff Select";
                mTooltip.text = "Touch to select an effect";
            }
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

    public void SetBuff()
    {
        int[] idSaver = new int[3];
        for (int i=0; i< mBuffImageArr.Length; i++)
        {
            idSaver[i] = -1;
        }
        for (int i=0; i< mBuffImageArr.Length;i++)
        {
            int rand = Random.Range(0,mBuffList.Count);
            switch (i)
            {
                case 0:
                    idSaver[0] = rand;
                    mBuffImageArr[i].mID = mBuffList[rand];
                    mBuffImageArr[i].SetBuff();
                    break;
                case 1:
                    if (rand!=idSaver[0] && rand != idSaver[2])
                    {
                        idSaver[1] = rand;
                        mBuffImageArr[i].mID = mBuffList[rand];
                        mBuffImageArr[i].SetBuff();
                    }
                    else
                    {
                        continue;
                    }
                    break;
                case 2:
                    if (rand != idSaver[0]&& rand != idSaver[1])
                    {
                        idSaver[2] = rand;
                        mBuffImageArr[i].mID = mBuffList[rand];
                        mBuffImageArr[i].SetBuff();
                    }
                    else
                    {
                        continue;
                    }
                    break;
            }
        }
    }
}
