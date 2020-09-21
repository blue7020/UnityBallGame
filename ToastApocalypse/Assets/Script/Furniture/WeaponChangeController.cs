using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeController : MonoBehaviour
{
    public static WeaponChangeController Instance;

    public Text mTitleText, mTooltipText, mSelectText, mGuideText;
    public Image mWeaponChangeWindow;
    public WeaponSelectController mWeaponSelectController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (GameSetting.Instance.Language==0)//한국어
            {
                mTitleText.text = "무기 선택";
                mTooltipText.text = "사용할 무기를 드래그하여 현재 선택 슬롯에 넣어주세요";
                mSelectText.text = "현재 선택";
                mGuideText.text = "슬롯 터치 시 툴팁을 표시합니다";
            }
            else if (GameSetting.Instance.Language==1)//영어
            {
                mTitleText.text = "Weapon Select";
                mTooltipText.text = "Drag the weapon to selection slot";
                mSelectText.text = "Selection";
                mGuideText.text = "Touch the slot to view tooltips";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWeaponSelectController.RefreshInventory();
            mWeaponChangeWindow.gameObject.SetActive(true);
        }
    }
}
