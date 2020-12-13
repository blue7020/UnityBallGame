using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image mPlayCountSceen,mItemImage,mItemBoxImage;
    public Sprite mNull;
    public Text mPlayCountText;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(ShowPlayCountScreen());
    }

    public IEnumerator ShowPlayCountScreen()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(2f);
        GameController.Instance.GamePause();
        mPlayCountText.text = "x"+TitleController.Instance.PlayCount;
        mPlayCountSceen.gameObject.SetActive(true);
        yield return delay;
        mPlayCountSceen.gameObject.SetActive(false);
        GameController.Instance.GamePause();
    }

    public void ItemImageChange(Sprite spt=null)
    {
        mItemImage.sprite = spt;
    }
}
