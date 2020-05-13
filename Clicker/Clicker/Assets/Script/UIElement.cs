using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private TextMeshProUGUI mTitleText, mLevelText, mContentsText, mCostText, mTenCostText;
    [SerializeField]
    private Button mButton, mTenUPbutton;

    private Coroutine mButtonPopRoutine;
    private int mClickCount;

    private int mID;
    private Delegates.TwoIntInVoidCallback mCallback;


    public void Init(int id,
        Sprite Icon,
        string title,
        string level,
        string contents,
        string cost,
        string tenCost,
        Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = Icon;
        mTitleText.text = title;
        Refresh(level, contents, cost, tenCost);

        mButton.onClick.AddListener(()=>
        {
            if(mButtonPopRoutine == null)
            {
                mClickCount = 0;
                mButtonPopRoutine = StartCoroutine(ButtonPop());
            }
            mClickCount++;
            callback(mID, 1);
        });
        mTenUPbutton.onClick.AddListener(()=>
        {
            mClickCount++;
            callback(mID, 10);
        });
    }

    public void Refresh(string level, string contents, string cost, string tenCost)
    {
        mLevelText.text = level;
        mContentsText.text = contents;
        mCostText.text = cost;
        mTenCostText.text = tenCost;
    }

    public void SetbuttonActive(bool IsActive)
    {
        mButton.interactable = IsActive;
    }
    
    public void SetTenButtonActive(bool IsActive)
    {
        mTenUPbutton.interactable = IsActive;
    }

    private IEnumerator ButtonPop()
    {
        WaitForSeconds pointOne = new WaitForSeconds(0.1f);
        float time = 3;
        bool activeButton = false;
        while (time > 0)
        {
            yield return pointOne;
            time -= 0.1f;
            if (mClickCount == 3)
            {
                activeButton = true && mTenUPbutton.interactable;
                mTenUPbutton.gameObject.SetActive(activeButton);
                break;
            }
        }

        if (activeButton)
        {
            mClickCount = 0;
            time = 3;
            while (time > 0)
            {
                if(mClickCount > 0)
                {
                    time = 3;
                    mClickCount = 0;
                }
                yield return pointOne;
                time -= 0.1f;
            }
        }

        mTenUPbutton.gameObject.SetActive(false);
        mButtonPopRoutine = null;
    }

}
