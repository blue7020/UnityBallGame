using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System.Runtime.CompilerServices;

public class SkillList : MonoBehaviour
{
    public static SkillList Instance;

    public SkillEffect effect;

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

    //스킬의 buff코드
    //10=무적 11=공격력 12=방어력 13=공격속도 14=이동속도
    public void Tumble()//0
    {
        quaternion Backup = Player.Instance.transform.rotation;
        Player.Instance.mAnim.SetBool(AnimHash.Tumble, true);
        int DashSpeed = 15;
        Vector3 tumble = Player.Instance.mDirection.transform.up;
        Player.Instance.Dash(tumble,10,DashSpeed);
        Player.Instance.transform.rotation = Backup;
    }

    public void Cabbage_Boomerang()//1
    {
        Vector3 dir = Player.Instance.mDirection.transform.up;
        PlayerBullet bolt = PlayerBulletPool.Instance.GetFromPool(2);
        bolt.transform.SetParent(Player.Instance.transform);
        bolt.transform.position = Player.Instance.mDirection.transform.position;//플레이어는 이동 방향에 따라 오브젝트가 뒤집히므로 mDirection으로 설정
        bolt.mDamage = (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk);
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
                    Vector3 Pos = Player.Instance.mDirection.transform.position;//플레이어는 이동 방향에 따라 오브젝트가 뒤집히므로 mDirection으로 설정
                    bolt.mRB2D.DOMove(Pos, 0.3f);
                    yield return delay;
                }
            }
        }
    }

    public void Dash()//2
    {
        //대쉬 이펙트
        int DashSpeed = 20;
        Vector3 dash = Player.Instance.mDirection.transform.up;
        effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        effect.SetEffect(BuffEffectController.Instance.mSprite[3], SkillController.Instance.mStatInfoArr[2].Duration,0,Color.clear, (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk));
        BuffEffectController.Instance.EffectList.Add(effect);
        Player.Instance.Dash(dash, 10,DashSpeed);
    }

    public void Power_of_Oven()//3
    {
        SkillEffect effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        effect.SetEffect(BuffEffectController.Instance.mSprite[1], SkillController.Instance.mStatInfoArr[3].Duration,0,Color.clear);
        BuffEffectController.Instance.EffectList.Add(effect);
        StartCoroutine(Player.Instance.Atk(SkillController.Instance.mStatInfoArr[3].Atk,11, SkillController.Instance.mStatInfoArr[3].Duration));
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[3].AtkSpd,13, SkillController.Instance.mStatInfoArr[3].Duration));
    }

    public void Frost_Shield()//4
    {
        SkillEffect effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        effect.SetEffect(BuffEffectController.Instance.mSprite[2], SkillController.Instance.mStatInfoArr[3].Duration,1, Color.clear);
        BuffEffectController.Instance.EffectList.Add(effect);
        StartCoroutine(Player.Instance.Speed(SkillController.Instance.mStatInfoArr[4].Spd,14, SkillController.Instance.mStatInfoArr[3].Duration));
    }
}
