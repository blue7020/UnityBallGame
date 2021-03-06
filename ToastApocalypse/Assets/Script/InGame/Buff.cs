﻿using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public int mID;
    public int mBuffCode;
    public Image mIcon;
    public eBuffType eType;
    public float mDuration;
    public Animator mAnim;

    public void SetData(int id, int code, Sprite spt,eBuffType bufftype,float dura,int Index)
    {
        mID = id;
        mBuffCode = code;
        mIcon.sprite = spt;
        eType = bufftype;
        mDuration = dura;
        StartCoroutine(Dura(Index));
    }

    private IEnumerator Dura(int index)
    {
        int count=0;
        WaitForSeconds dura = new WaitForSeconds(1f);
        while (true)
        {
            if (count >= mDuration*0.6f)
            {
                if (count >= mDuration)
                {
                    break;
                }
                mAnim.SetBool(AnimHash.Fading, true);
            }
            yield return dura;
            count++;
        }
        BuffController.Instance.buffIndex--;
        BuffController.Instance.mBuffArr[index] = null;
        Delete();
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
