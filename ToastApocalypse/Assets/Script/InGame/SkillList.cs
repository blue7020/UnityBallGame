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
    public Turret[] SkillTurret;
    public int DestroyCheckLevel;
    public bool isTutorial;
    public GameObject DestroyObjZone;

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

    public void SkillSetting(int id)
    {
        effect = null;
        switch (id)
        {
            case 0://구르기
                Roll();
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
            case 5://오일 폭발
                StartCoroutine(Oil_Explision());
                break;
            case 6://로스팅
                Roasting();
                break;
            case 7://공중 제비
                Tumble();
                break;
            case 8://토핑 추가
                Topping_Plus();
                break;
            case 9://샴페인 폭발
                StartCoroutine(Champagne_Explosion());
                break;
            case 10://재포장
                StartCoroutine(Repackaging());
                break;
            case 11://소시지 터렛
                Sausage_Turret();
                break;
            case 12://바삭한 식감
                Crispy_Texture();
                break;
            case 13://배트-랑
                Bat_rang();
                break;
            case 14://크리스마스의 축복
                StartCoroutine(The_Blessing_Of_Christmas());
                break;
            case 15://절구찧기
                StartCoroutine(Mortar());
                break;
        }
        
    }

    //스킬의 buff코드
    //10=무적 11=공격력 12=방어력 13=공격속도 14=이동속도
    public void Roll()//0
    {
        int DashSpeed = 15;
        Player.Instance.mAnim.SetBool(AnimHash.Tumble, true);
        quaternion Backup = Player.Instance.transform.rotation;
        Vector3 tumble = Player.Instance.mDirection.transform.up;
        Player.Instance.Dash(tumble,10,DashSpeed);
        Player.Instance.transform.rotation = Backup;
    }

    public void Cabbage_Boomerang()//1
    {
        DestroyCheckLevel = GameController.Instance.StageLevel;
        PlayerBullet bolt = Instantiate(Skillbullet[0],Player.Instance.CurrentRoom.transform);
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
        if (GameController.Instance.StageLevel != DestroyCheckLevel)
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
            if (GameController.Instance.StageLevel != DestroyCheckLevel)
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
        int DashSpeed = 17;
        Vector3 dash = Player.Instance.mDirection.transform.up;
        Player.Instance.mAnim.SetBool(AnimHash.Tumble, true);
        effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        effect.SetEffect(BuffEffectController.Instance.mSprite[0], 1f,0,Color.clear, (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk),eSkilltype.DamageCollider);
        BuffEffectController.Instance.EffectList.Add(effect);
        Player.Instance.Dash(dash, 10,DashSpeed);
    }

    public void Power_of_Oven()//3
    {
        StartCoroutine(Player.Instance.Atk(SkillController.Instance.mStatInfoArr[3].Atk,11, SkillController.Instance.mStatInfoArr[3].Duration));
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[3].AtkSpd,13, SkillController.Instance.mStatInfoArr[3].Duration));
    }

    public void Frost_Shield()//4
    {
        SkillEffect effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        effect.SetEffect(BuffEffectController.Instance.mSprite[1], SkillController.Instance.mStatInfoArr[3].Duration,1, Color.clear, 0, eSkilltype.Barrier);
        BuffEffectController.Instance.EffectList.Add(effect);
        StartCoroutine(Player.Instance.Speed(SkillController.Instance.mStatInfoArr[4].Spd,14, SkillController.Instance.mStatInfoArr[3].Duration));
    }

    public IEnumerator Oil_Explision()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);
        GameObject destroyzone = Instantiate(DestroyObjZone, Player.Instance.transform);
        int count=0;
        int XPos = 2; int YPos = 2;
        while (true)
        {
            if (count >= 15)
            {
                break;
            }
            else if (count==10)
            {
                XPos += 2; YPos += 2;
            }
            else
            {
                SoundController.Instance.SESound(15);
                int Xpos = UnityEngine.Random.Range(-XPos, XPos);
                int Ypos = UnityEngine.Random.Range(-YPos, YPos);
                Vector3 Pos = new Vector3(Xpos, Ypos, 0);
                Vector3 StartPos = Player.Instance.transform.position;
                PlayerBullet oil = PlayerBulletPool.Instance.GetFromPool(12);
                oil.transform.SetParent(destroyzone.transform);
                oil.mDamage = SkillController.Instance.mStatInfoArr[5].Damage * Player.Instance.mStats.Atk;
                oil.transform.position = StartPos + Pos;
                count++;
            }
            yield return delay;
        }
    }

    public void Roasting()
    {
        StartCoroutine(Player.Instance.NoUseAmmo(16, SkillController.Instance.mStatInfoArr[6].Duration));
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[6].AtkSpd, 13, SkillController.Instance.mStatInfoArr[6].Duration));
    }

    public void Tumble()//7
    {
        quaternion Backup = Player.Instance.transform.rotation;
        Player.Instance.mAnim.SetBool(AnimHash.Tumble, true);
        int DashSpeed = 15;
        Vector3 tumble = Player.Instance.mDirection.transform.up;
        effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        effect.SetEffect(BuffEffectController.Instance.mSprite[0], 1f, 0, new Color(255 / 255f, 155 / 255f, 0 / 255f));
        BuffEffectController.Instance.EffectList.Add(effect);
        Player.Instance.Dash(tumble, 10, DashSpeed);
        Player.Instance.transform.rotation = Backup;
    }

    public void Topping_Plus()
    {
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[8].AtkSpd, 13, SkillController.Instance.mStatInfoArr[8].Duration));
        StartCoroutine(Player.Instance.PlusBolt(1,15, SkillController.Instance.mStatInfoArr[8].Duration));
    }

    public IEnumerator Champagne_Explosion()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        int count = 0;
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[9].AtkSpd, 13, SkillController.Instance.mStatInfoArr[9].Duration));
        StartCoroutine(Player.Instance.Def(SkillController.Instance.mStatInfoArr[9].Def, 12, SkillController.Instance.mStatInfoArr[9].Duration));
        GameObject destroyzone = Instantiate(DestroyObjZone,Player.Instance.transform);
        while (true)
        {
            if (count>=49)
            {
                break;
            }
            else
            {
                PlayerBullet bolt = Instantiate(Skillbullet[1], destroyzone.transform);
                bolt.mDamage = (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk);
                Vector3 dir = (Player.Instance.mDirection.transform.up*-1) * bolt.mSpeed*4;
                bolt.mRB2D.AddForce(dir);
                count++;
            }
            yield return delay;
        }
        Destroy(destroyzone);
    }


    public IEnumerator Repackaging()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        BuffController.Instance.RemoveNurf();
        int count = 0;
        float amount = Player.Instance.mMaxHP * 0.3f;
        while (true)
        {
            if (count >= 6)
            {
                break;
            }
            Player.Instance.Heal(amount / 6);
            count++;
            yield return delay;
        }
    }

    public void Sausage_Turret()
    {
        Turret turret = Instantiate(SkillTurret[0],Player.Instance.CurrentRoom.transform);
        turret.transform.position = Player.Instance.transform.position;
    }

    public void Crispy_Texture()
    {
        StartCoroutine(Player.Instance.Critical(SkillController.Instance.mStatInfoArr[12].Crit, 16, SkillController.Instance.mStatInfoArr[12].Duration));
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[12].AtkSpd, 13, SkillController.Instance.mStatInfoArr[12].Duration));
    }

    public void Bat_rang()
    {
        DestroyCheckLevel = GameController.Instance.StageLevel;
        PlayerBullet bolt = Instantiate(Skillbullet[3], Player.Instance.CurrentRoom.transform);
        bolt.transform.position = Player.Instance.transform.position;
        bolt.mDamage = (PlayerSkill.Insatnce.mStat.Damage * Player.Instance.mStats.Atk);
        Vector3 dir = Player.Instance.mDirection.transform.up * bolt.mSpeed;
        bolt.mRB2D.AddForce(dir);
        StartCoroutine(ShotBoomerang(bolt));
    }

    public IEnumerator The_Blessing_Of_Christmas()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        BuffController.Instance.RemoveNurf();
        Player.Instance.DoEffect(5, SkillController.Instance.mStatInfoArr[14].Duration, 17, 1f);
        int count = 0;
        float amount = Player.Instance.mMaxHP * 0.49f;
        while (true)
        {
            if (count >= 7)
            {
                break;
            }
            Player.Instance.Heal(amount / 7);
            count++;
            yield return delay;
        }
    }

    public IEnumerator Mortar()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        StartCoroutine(Player.Instance.Atk(SkillController.Instance.mStatInfoArr[15].Atk, 11, SkillController.Instance.mStatInfoArr[15].Duration));
        StartCoroutine(Player.Instance.AtkSpeed(SkillController.Instance.mStatInfoArr[15].AtkSpd, 13, SkillController.Instance.mStatInfoArr[15].Duration));
        int count = 0;
        float amount = Player.Instance.mMaxHP * 0.3f;
        while (true)
        {
            if (count >= 7)
            {
                break;
            }
            Player.Instance.Heal(amount / 7);
            count++;
            yield return delay;
        }
    }
}
