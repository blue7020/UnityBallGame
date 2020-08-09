using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System.Runtime.CompilerServices;

public class SkillList : MonoBehaviour
{
    public static SkillList Instance;

    public GameObject[] SkillObj;

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
                    Cabbage_Boomerang();
                    break;
                case 2://돌진
                    Dash();
                    break;
                case 3://오븐의 힘
                    Power_of_Oven();
                    break;
                case 4://얼음 보호막
                    Frost_Shield(); 
                    break;

            }
        }
        else if (id>=5||id<10)
        {

        }
        
    }

    public void Tumble()//0
    {
        quaternion Backup = Player.Instance.transform.rotation;
        Player.Instance.mAnim.SetBool(AnimHash.Tumble, true);
        int DashSpeed = 15;
        Vector3 tumble = Player.Instance.mDirection.transform.up;
        Player.Instance.Dash(tumble,DashSpeed);
        Player.Instance.transform.rotation = Backup;
    }

    public void Cabbage_Boomerang()//1
    {
        Vector3 dir = Player.Instance.mDirection.transform.up;
        PlayerBullet bolt = PlayerBulletPool.Instance.GetFromPool(2);
        bolt.transform.SetParent(Player.Instance.transform);
        bolt.transform.position = Player.Instance.transform.position;
        bolt.mDamage = (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk) + GameController.Instance.Level;
        bolt.mRB2D.DOMove(dir * bolt.mSpeed, 0.8f).SetEase(Ease.Linear).OnComplete(() => { StartCoroutine(returnPlayer(bolt)); });
    }

    private IEnumerator returnPlayer(PlayerBullet bolt)
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        bolt.returnPlayer = true;
        while (true)
        {
            if (GameController.Instance.Level>5)
            {
                Destroy(bolt.gameObject);
                break;
            }
            else
            {
                if (bolt.returnCheck == true)
                {
                    bolt.returnCheck = false;
                    bolt.returnPlayer = false;
                    StopCoroutine(returnPlayer(bolt));
                    break;
                }
                else
                {
                    Vector3 Pos = Player.Instance.transform.position;
                    bolt.mRB2D.DOMove(Pos, 0.3f);
                    yield return delay;
                }
            }
        }
    }

    public void Dash()//2
    {
        //대쉬 이펙트
        //BuffController.Instance.RemoveNurf();
        int DashSpeed = 20;
        Vector3 dash = Player.Instance.mDirection.transform.up;
        Player.Instance.Dash(dash, DashSpeed);
        //부딪힌 대상 2초간 기절
    }

    public void Power_of_Oven()//3
    {
        Debug.Log("오븐의힘");
        StartCoroutine(Oven());
        StartCoroutine(Player.Instance.Atk(SkillController.Instance.mStatInfoArr[3].Atk, SkillController.Instance.mStatInfoArr[3].Duration));
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[3].AtkSpd, SkillController.Instance.mStatInfoArr[3].Duration));
    }
    private IEnumerator Oven()
    {
        WaitForSeconds dura = new WaitForSeconds(SkillController.Instance.mStatInfoArr[3].Duration);
        //버프이펙트 애니메이션
        //SkillObj[0].transform.SetParent(Player.Instance.transform);
        //SkillObj[0].transform.localPosition = Vector3.zero;
        //SkillObj[0].SetActive(true);
        yield return dura;
        //SkillObj[0].SetActive(false);
    }

    public void Frost_Shield()//4
    {
        Debug.Log("얼음보호막");
        StartCoroutine(Sheld());
        StartCoroutine(Player.Instance.Atk(SkillController.Instance.mStatInfoArr[4].Spd, SkillController.Instance.mStatInfoArr[3].Duration));
    }
    private IEnumerator Sheld()
    {
        WaitForSeconds dura = new WaitForSeconds(SkillController.Instance.mStatInfoArr[4].Duration);
        //버프이펙트 애니메이션
        //SkillObj[1].transform.SetParent(Player.Instance.transform);
        //SkillObj[1].transform.localPosition = Vector3.zero;
        //SkillObj[1].SetActive(true);
        yield return dura;
        //SkillObj[1].SetActive(false);
    } 
}
