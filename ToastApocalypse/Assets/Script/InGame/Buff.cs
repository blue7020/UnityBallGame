using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{

    public int mID;
    public Image mIcon;
    public eBuffType eType;
    public float mDuration;

    public void SetData(int id,Sprite spt,eBuffType bufftype,float dura)
    {
        mID = id;
        mIcon.sprite = spt;
        eType = bufftype;
        mDuration = dura;
        StartCoroutine(Dura());
    }

    private IEnumerator Dura()
    {
        WaitForSeconds duration = new WaitForSeconds(mDuration);
        yield return duration;
        Delete();
    }

    public void Delete()
    {
        BuffController.Instance.buffIndex--;
        Destroy(gameObject);
    }
}
