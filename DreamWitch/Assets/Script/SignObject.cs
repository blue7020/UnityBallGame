using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignObject : MonoBehaviour
{
    public string[] TextArr;//0=kor,1=eng
    public string text;
    public bool isCollide;

    public void SetText()
    {
        switch (TitleController.Instance.mLanguage)
        {
            case 0:
                text = TextArr[0];
                break;
            case 1:
                text = TextArr[1];
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetText();
            Player.Instance.mFuntion = (() => { UIController.Instance.ShowTextBox(text); });
        }   
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.mFuntion = null;
        }
    }
}
