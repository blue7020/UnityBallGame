using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public static WeaponController Instance;

    public int mWeaponSkillCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mWeaponSkillCount = 0;
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
        switch (WeaponID)//원거리 무기는 Playerbullet에서 설정
        {
            case 0://이쑤시개
                break;
            case 1://케찹
                break;
            case 2://머스타드
                break;
            case 3://바게트
                KnockBack(Target);
                break;
            case 4://포크
                break;
            case 5://뒤집개
                Stun_Crit(Target, Checker);
                break;
            case 6://아이스크림 스쿱
                break;
            case 7://생크림 레이저
                break;
            case 8://피자 커터
                break;
            case 9://시리얼 디스펜서
                StartCoroutine(TripleShot());
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
                StartCoroutine(DoubleShot());
                break;
            case 19://피쉬본
                break;
            case 20://본블레이드
                break;
            case 21://스노우플레이크
                break;
            case 22://샌드위치 소드
                break;
            case 23://어묵 꼬치
                FishCakeBar(Target);
                break;
            case 24://콘도그
                CornDog(Target,Checker);
                break;
            case 25://시럽주사기
                break;
            case 26://수확자
                TheReaper(Target,Checker);
                break;
            case 27://캔디 배럴
                break;
            case 28://캔디샷
                break;
            case 29://펌킨 블레이드
                PumpkinBlade(Target);
                break;
            case 30://캔디케인
                CandyCane(Target);
                break;
            case 31://산타의굴뚝
                StartCoroutine(TripleShot());
                break;
            case 32://아이스캔디바
                Stun_Crit(Target,Checker);
                break;
            case 33://기프트런처
                break;

        }
        Target = null;
    }

    //WeaponSkillData
    public void KnockBack(Enemy Target)
    {
        if (Target != null&&Target.mStats.Resistance<0.5f)
        {
            Vector3 Pos = Player.Instance.transform.position - Target.transform.position;
            Target.mRB2D.velocity = Vector3.zero;
            Target.mRB2D.AddForce(-Pos * 20, ForceMode2D.Impulse);
        }
    }

    public void Stun_Crit(Enemy Target, bool isCrit)
    {
        if (isCrit == true&&Target!=null)
        {
            StartCoroutine(Target.Stuned(1f));
        }
    }

    public IEnumerator TripleShot()
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

    public IEnumerator DoubleShot()
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

    public void FishCakeBar(Enemy Target)
    {
        if (Target != null)
        {
            if (mWeaponSkillCount >= 4)
            {
                Target.Hit((Player.Instance.mStats.Atk + Player.Instance.buffIncrease[0]) * 0.3f, 0, false);
                mWeaponSkillCount = 0;
            }
            else
            {
                mWeaponSkillCount++;
            }
        }
    }

    public void CornDog(Enemy Target, bool isCrit)
    {
        if (isCrit == true && Target != null)
        {
            Player.Instance.StartCoroutine(Player.Instance.Speed(0.2f,4,1f));
        }
        KnockBack(Target);
    }

    public void TheReaper(Enemy Target,bool isCrit)
    {
        if (isCrit == true && Target != null)
        {
            Player.Instance.Heal(0.5f);
        }
    }

    public void PumpkinBlade(Enemy Target)
    {
        if (Target != null)
        {
            if (mWeaponSkillCount >= 4)
            {
                Player.Instance.NowPlayerWeapon.mStats.Crit = 1f;
                mWeaponSkillCount = 0;
            }
            else
            {
                Player.Instance.NowPlayerWeapon.mStats.Crit = 0;
                mWeaponSkillCount++;
            }
        }
    }

    public void CandyCane(Enemy Target)
    {
        if (Target != null)
        {
            Player.Instance.StartCoroutine(Player.Instance.Speed(0.2f, 4, 0.5f));
            KnockBack(Target);
        }
    }
}
