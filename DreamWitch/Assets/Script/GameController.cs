using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform mStartPoint, mCanvas,mHeartFrameCanvas;
    public Image mHeart,mHeartFrame;
    public List<Image> mPlayerHP;
    public List<Image> mHPFrame;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mPlayerHP = new List<Image>();
            mHPFrame = new List<Image>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetHP(Player.Instance.mCurrentHP);
    }

    public void SetHP(int count)
    {
        for (int i = 0; i < count; i++)
        {
            mHPFrame.Add(Instantiate(mHeartFrame, mHeartFrameCanvas));
            mPlayerHP.Add(Instantiate(mHeart, mCanvas));
        }
    }

    public void Heal(int count)
    {
        for (int i = 0; i < count; i++)
        {
            mPlayerHP.Add(Instantiate(mHeart, mCanvas));
        }
        Player.Instance.mCurrentHP += count;
    }

    public void Damege()
    {
        Debug.Log(mPlayerHP.Count);
        if (mPlayerHP.Count==1)
        {
            Debug.Log("사망");
        }
        else
        {
            Destroy(mPlayerHP[mPlayerHP.Count - 1].gameObject);
            mPlayerHP.RemoveAt(mPlayerHP.Count - 1);
        }
    }
}
