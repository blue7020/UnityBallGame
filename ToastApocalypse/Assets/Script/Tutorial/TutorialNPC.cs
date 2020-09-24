using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNPC : MonoBehaviour
{
    public GameObject mJail;
    public bool mRescue;
    public string mMessage;

    private void Awake()
    {
        mRescue = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (mRescue == false)
            {
                mRescue = true;
                mJail.SetActive(false);
                TutorialDialog.Instance.ShowDialog();
            }
        }
    }
}
