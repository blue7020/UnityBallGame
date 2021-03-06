﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillEffect : MonoBehaviour
{
    public SpriteRenderer[] mObj;
    public SpriteRenderer mEffect;
    public Sprite mSpt;
    public float mDura;
    public Animator mAnim;
    public eSkilltype eType;
    public float mDamage;

    public void SetEffect(Sprite spt, float dura, int type, Color effectcolor, float damage = 0, eSkilltype etype=eSkilltype.None)
    {
        eType = etype;
        mDamage = damage;
        if (type == 0)//자신 기준 버프
        {
            mEffect.color = effectcolor;
            mObj[0].gameObject.SetActive(true);
            mAnim.SetBool(AnimHash.Aurora, true);
        }
        else if(type==1)//버프 공전
        {
            mSpt = spt;
            for (int i = 0; i < mObj.Length; i++)
            {
                mObj[i].gameObject.SetActive(true);
                mObj[i].sprite = mSpt;
            }
            mAnim.SetBool(AnimHash.Spining,true);
        }
        mDura = dura;
        StartCoroutine(ShowEffect());
    }

    private IEnumerator ShowEffect()
    {
        WaitForSeconds dura = new WaitForSeconds(mDura);
        yield return dura;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (eType==eSkilltype.Barrier)
        {
            if (other.GetComponent<Bullet>())
            {
                Bullet bolt = other.GetComponent<Bullet>();
                bolt.RemoveBullet();
            }
        }
        else if(eType == eSkilltype.DamageCollider)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                Enemy Target = other.gameObject.GetComponent<Enemy>();
                Target.Hit(mDamage,1,false);
                float rand = Random.Range(0,1f);
                if (Target.mStats.Resistance <= rand)
                {
                    Target.StartCoroutine(Target.Stuned(2f));
                    Target = null;
                }

            }
        }
    }
}
