using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public static WeaponController Instance;

    public List<Weapon> mWeapons;

    public int mWeaponSkillCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mWeaponSkillCount = 0;
            mWeapons = new List<Weapon>();
            for (int i = 0; i < GameSetting.Instance.mWeapons.Length; i++)
            {
                mWeapons.Add(GameSetting.Instance.mWeapons[i]);
            }
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void WeaponSkill(int WeaponID, Enemy Target, bool Checker)
    {
        switch (WeaponID)
        {
            case 0://이쑤시개
                break;
            case 1://케찹
                break;
            case 2://머스타드
                Mustard(Target);
                break;
            case 3://바게트
                KnockBack(Target);
                break;
            case 4://포크
                break;
            case 5://뒤집개
                Fliper(Target, Checker);
                break;
            case 6://아이스크림 스쿱
                break;
            case 7://생크림 레이저
                break;
            case 8://피자 커터
                break;
            case 9://시리얼 디스펜서
                StartCoroutine(Cereal());
                break;
            case 10://나루토마키
                break;
            case 11://냉동 고등어
                KnockBack(Target);
                break;
            case 12://이쑤시개 나이프
                break;
            case 13://팝콘 런처
                break;
            case 14://크럼브샷
                break;
            case 15://머핀 해머
                KnockBack(Target);
                break;
            case 16://화염방사기
                break;
            case 17://잼블레이드
                JamBlade(Target);
                break;
            case 18://페퍼로니 건
                StartCoroutine(Pepperoni());
                break;
            case 19://피쉬본
                break;
            case 20://본블레이드
                break;
            case 21://스노우플레이크
                break;
            case 22://샌드위치 소드
                break;

        }
        Target = null;
    }

    //WeaponSkillData
    private void Mustard(Enemy Target)
    {
        if (Target != null)
        {
            Target.StartCoroutine(Target.SpeedNurf(1.5f, 0.5f));
        }
    }

    public void KnockBack(Enemy Target)
    {
        if (Target != null&&Target.mStats.Resistance<0.5f)
        {
            Vector3 Pos = Player.Instance.transform.position - Target.transform.position;
            Target.mRB2D.velocity = Vector3.zero;
            Target.mRB2D.AddForce(-Pos * 20, ForceMode2D.Impulse);
        }
    }

    public void Fliper(Enemy Target, bool isCrit)
    {
        if (isCrit == true&&Target!=null)
        {
            StartCoroutine(Target.Stuned(1f));
        }
    }

    public IEnumerator Cereal()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        yield return delay;
        Player.Instance.NowPlayerWeapon.mAttackArea.ResetDir();
        yield return delay;
        Player.Instance.NowPlayerWeapon.mAttackArea.ResetDir();
    }

    public void JamBlade(Enemy Target)
    {
        if (mWeaponSkillCount>=2)
        {
            PlayerBullet bolt = PlayerBulletPool.Instance.GetFromPool(9);
            bolt.mWeaponID = 17;
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand <= Player.Instance.mStats.Crit+ Player.Instance.buffIncrease[5] / 100)
            {
                bolt.mDamage = (Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage);

            }
            else
            {
                bolt.mDamage = Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]);
            }
            bolt.transform.localPosition = Player.Instance.NowPlayerWeapon.transform.position;
            bolt.transform.localScale = bolt.mboltscale * (1 + PassiveArtifacts.Instance.AdditionalBulletSize);
            float angle = Mathf.Atan2(AttackPad.Instance.inputVector.y, AttackPad.Instance.inputVector.x) * Mathf.Rad2Deg;
            bolt.transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);
            bolt.mRB2D.AddForce(Player.Instance.NowPlayerWeapon.mAttackArea.BulletStarter.up * bolt.mSpeed, ForceMode2D.Impulse);
            mWeaponSkillCount = 0;
        }
        else
        {
            mWeaponSkillCount++;
        }
        KnockBack(Target);
    }

    public IEnumerator Pepperoni()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        yield return delay;
        Player.Instance.NowPlayerWeapon.mAttackArea.ResetDir();
    }

    public void SandwichSword()
    {
        if (Player.Instance.mTotalKillCount%3==0)
        {
            Player.Instance.Heal(1);
        }
    }
}
