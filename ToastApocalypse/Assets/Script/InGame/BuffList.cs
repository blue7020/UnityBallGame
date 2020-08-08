using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffList : MonoBehaviour
{
    public static BuffList Instance;

    public eBuffType eType;

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

    public void BuffSetting(int id)//버프 5개 단위로 끊어서 작성
    {
        if (id < 5)
        {
            switch (id)
            {
                case 0://공격력 중가
                    break;
                case 1://방어력 증가
                    break;
                case 2://이동 속도 증가
                    break;
                case 3://공격 속도 증가
                    break;
                case 4://기절
                    break;
                case 5://둔화
                    break;
            }
        }
    }

    private void Atk()
    {

    }

}
