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
        Debug.Log("실행");
        float DashSpeed = 5f;
        Player.Instance.mRB2D.velocity = Vector3.zero;
        Player.Instance.mRB2D.AddForce(new Vector2(1,1)*DashSpeed,ForceMode2D.Force);
    }

    public void Cabbage_Boomerang()//1
    {

    }

    public void Dash()//2
    {

    }

    public void Power_of_Oven()//3
    {

    }

    public void Ice_Barrier()//4
    {

    }
}
