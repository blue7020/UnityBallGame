using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyNPC : MonoBehaviour
{
    public Image mDialogWindow;
    public Text mText;
    public int mID;

    private void Start()
    {
        if (GameSetting.Instance.Language == 0)
        {
            switch (mID)
            {
                case 6:
                    mText.text = "";
                    break;
                case 7:
                    mText.text = "";
                    break;
                case 8:
                    mText.text = "우리도 저주 때문에 언제 유통기한이\n생길지 몰라, 항상 조심하게.";
                    break;
            }
        }
        else if (GameSetting.Instance.Language == 1)
        {
            switch (mID)
            {
                case 6:
                    mText.text = "";
                    break;
                case 7:
                    mText.text = "";
                    break;
                case 8:
                    mText.text = "";
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mDialogWindow.gameObject.SetActive(true);
        }
    }
}
