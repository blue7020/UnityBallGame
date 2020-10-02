using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNPC : MonoBehaviour
{
    public GameObject mJail;
    public int mID;
    public bool mRescue;
    public TextEffect effect;
    public string mtext;
    public SpriteRenderer mRenderer;
    private void Awake()
    {
        mRescue = false;
    }

    public IEnumerator Rescue()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        mRescue = true;
        mJail.SetActive(false);
        SoundController.Instance.SESoundUI(6);
        if (GameSetting.Instance.Language==0)
        {
            mtext = "시민을 구출하였습니다!";
        }
        else if(GameSetting.Instance.Language ==1)
        {
            mtext = "You rescued the citizen";
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(mtext);
        GameController.Instance.RescueNPCList.Add(mID);
        yield return delay;
        gameObject.SetActive(false);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (mRescue==false)
            {
                StartCoroutine(Rescue());
            }
        }
    }
}
