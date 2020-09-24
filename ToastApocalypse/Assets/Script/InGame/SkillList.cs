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
    public PlayerBullet[] Skillbullet;
    public int DestroyCheckLevel;
    public bool isTutorial;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (isTutorial!=true)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
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
        DestroyCheckLevel = GameController.Instance.Level;
        PlayerBullet bolt = Instantiate(Skillbullet[0],Player.Instance.CurrentRoom.transform);
        bolt.transform.SetParent(Player.Instance.CurrentRoom.transform);
        bolt.transform.position = Player.Instance.transform.position;
        bolt.mDamage = (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk);
        Vector3 dir = Player.Instance.mDirection.transform.up*bolt.mSpeed;
        bolt.mRB2D.AddForce(dir);
        StartCoroutine(ShotBoomerang(bolt));
    }

    private IEnumerator ShotBoomerang(PlayerBullet bolt)
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        yield return delay;
        if (GameController.Instance.Level != DestroyCheckLevel)
        {
            Destroy(bolt);
        }
        else
        {
            bolt.mRB2D.velocity = Vector3.zero;
            yield return delay;
            bolt.returnPlayer = true;
            StartCoroutine(ReturnBoomerang(bolt));
        }
    }
    private IEnumerator ReturnBoomerang(PlayerBullet bolt)
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            if (GameController.Instance.Level != DestroyCheckLevel)
            {
                Destroy(bolt);
            }
            if (bolt!=null)
            {
                bolt.mRB2D.DOMove(Player.Instance.transform.position, 0.3f);
            }
            yield return delay;
        }
    }

    public void Dash()//2
    {
        int DashSpeed = 20;
        Vector3 dash = Player.Instance.mDirection.transform.up;
        effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        effect.SetEffect(BuffEffectController.Instance.mSprite[3], 1f,0,Color.clear, (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk),eSkilltype.DamageCollider);
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
        effect.SetEffect(BuffEffectController.Instance.mSprite[2], SkillController.Instance.mStatInfoArr[3].Duration,1, Color.clear, 0, eSkilltype.Barrier);
        BuffEffectController.Instance.EffectList.Add(effect);
        StartCoroutine(Player.Instance.Speed(SkillController.Instance.mStatInfoArr[4].Spd,14, SkillController.Instance.mStatInfoArr[3].Duration));
    }
}
