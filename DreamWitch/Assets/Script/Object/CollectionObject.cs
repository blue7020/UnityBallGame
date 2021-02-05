using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObject : MonoBehaviour
{
    public int mID;
    public Animator mAnim;
    public Transform pos;
    public bool isGet,isMessage;
    public string[] textArr;//kor =0, eng =0
    public string text;

    private void Start()
    {
        pos = transform;
        Setting();
        GameController.Instance.mMapMaterialController.CollectionList.Add(this);
        if (isMessage)
        {
            if (TitleController.Instance.mLanguage == 0)
            {
                text = textArr[0];
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                text = textArr[1];
            }
        }
    }

    public void Refresh()
    {
        transform.position = pos.position;
        gameObject.SetActive(true);
        Setting();
    }

    public void Setting()
    {
        switch (TitleController.Instance.NowStage)
        {
            case 0:
                if (SaveDataController.Instance.mUser.Stage_0_CollectionCheck[mID] == true)
                {
                    isGet = true;
                    mAnim.SetBool(AnimHash.Get, true);
                }
                break;
            case 1:
                if (SaveDataController.Instance.mUser.Stage_1_CollectionCheck[mID] == true)
                {
                    isGet = true;
                    mAnim.SetBool(AnimHash.Get, true);
                }
                break;
            case 2:
                if (SaveDataController.Instance.mUser.Stage_2_CollectionCheck[mID] == true)
                {
                    isGet = true;
                    mAnim.SetBool(AnimHash.Get, true);
                }
                break;
            case 3:
                if (SaveDataController.Instance.mUser.Stage_3_CollectionCheck[mID] == true)
                {
                    isGet = true;
                    mAnim.SetBool(AnimHash.Get, true);
                }
                break;
            case 4:
                if (SaveDataController.Instance.mUser.Stage_4_CollectionCheck[mID] == true)
                {
                    isGet = true;
                    mAnim.SetBool(AnimHash.Get, true);
                }
                break;
            case 5:
                if (SaveDataController.Instance.mUser.Stage_5_CollectionCheck[mID] == true)
                {
                    isGet = true;
                    mAnim.SetBool(AnimHash.Get, true);
                }
                break;
            default:
                break;
        }
    }

    public void Check()
    {
        switch (TitleController.Instance.NowStage)
        {
            case 0:
                SaveDataController.Instance.mUser.Stage_0_CollectionCheck[mID] = true;
                break;
            case 1:
                SaveDataController.Instance.mUser.Stage_1_CollectionCheck[mID] = true;
                break;
            case 2:
                SaveDataController.Instance.mUser.Stage_2_CollectionCheck[mID] = true;
                break;
            case 3:
                SaveDataController.Instance.mUser.Stage_3_CollectionCheck[mID] = true;
                break;
            case 4:
                SaveDataController.Instance.mUser.Stage_4_CollectionCheck[mID] = true;
                break;
            case 5:
                SaveDataController.Instance.mUser.Stage_5_CollectionCheck[mID] = true;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isGet)
            {
                Check();
                UIController.Instance.StartCoroutine(UIController.Instance.CollectAnimation());
                if (isMessage)
                {
                    UIController.Instance.ShowDialogue(text);
                }
            }
            SoundController.Instance.SESound(19);
            gameObject.SetActive(false);
        }
    }
}
