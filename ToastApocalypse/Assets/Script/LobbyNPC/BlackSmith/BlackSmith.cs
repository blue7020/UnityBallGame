using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith : MonoBehaviour
{
    public static BlackSmith Instance;

    public Animator mAnim;
    public Text mTitleText, mGuideText, mMaterialText;
    public Image mBlacksmithWindow;
    public bool IsShop;

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
        if (GameSetting.Instance.NPCOpen[2]==true)
        {
            mAnim.SetBool(AnimHash.Furniture, true);
        }
        if (GameSetting.Instance.Language==0)//한국어
        {
            mTitleText.text = "오븐";
            mGuideText.text = "슬롯 터치 시 툴팁을 표시합니다";
            mMaterialText.text = "조합 재료";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mTitleText.text = "Oven";
            mGuideText.text = "Touch the slot to view tooltips";
            mMaterialText.text = "Recipe";
        }
    }

    public void ShopExit()
    {
        IsShop = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mBlacksmithWindow.gameObject.SetActive(true);
        }
        IsShop = true;
    }
}
