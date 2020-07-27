using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public static SkillList Instance;


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
        DontDestroyOnLoad(gameObject);
    }

    public void SkillSetting(int id)//스킬 5개 단위로 끊어서 작성
    {
        if (id<5)
        {
            switch (id)
            {
                case 0://구르기
                    Tumble();
                    break;
                case 1://양상추부메랑
                    break;
                case 2://돌진
                    break;
                case 3://오븐의 힘
                    break;
                case 4://얼음 보호막
                    break;

            }
        }
        else if (id>=5||id<10)
        {

        }
        
    }

    public void Tumble()//0
    {
        Player.Instance.mAnim.SetBool(AnimHash.Tumble, true);
        int DashSpeed = 20;
        Vector3 tumble = Player.Instance.mDirection.transform.up;
        Player.Instance.Dash(tumble,DashSpeed);
    }

    public void Cabbage_Boomerang()//1
    {
        Debug.Log("양배추부메랑");
    }

    public void Dash()//2
    {
        Debug.Log("돌진");
    }

    public void Power_of_Oven()//3
    {
        Debug.Log("오븐의 힘");
    }

    public void Ice_Barrier()//4
    {
        Debug.Log("얼음보호막");
    }
}
