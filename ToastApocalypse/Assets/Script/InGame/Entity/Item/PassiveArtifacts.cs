using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveArtifacts : MonoBehaviour
{
    //패시브유물의 기능을 여기서 담당
    public static PassiveArtifacts Instance;

    public int AdditionalBullet;
    public float AdditionalMeleeRangeSize,AdditionalBulletSize, AdditionalCooltimeReduce, ReloadCooltimeReduce, ReviveCount;

    public GameObject[] mSkillobj;//혹시 모를 펫에 대비해서

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AdditionalMeleeRangeSize = 1;
            AdditionalBullet = 0;
            AdditionalBulletSize = 0;
            AdditionalCooltimeReduce = 0;
            ReloadCooltimeReduce = 0;
            ReviveCount = 0;
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ArtifactsFuntion(int id)
    {
        switch (id)
        {
            case 2://금단의 레시피
                AdditionalBulletSize += 0.5f;
                break;
            case 6://누틸라
                Player.Instance.MapSeeker = true;
                RoomControllers.Instance.Seeker();
                break;
            case 8://쿠키틀
                Player.Instance.TrapResistance = true;
                break;
            case 14://달걀과 베이컨
                ReloadCooltimeReduce += 0.3f;
                break;
            case 24://익숙한 버섯
                AdditionalMeleeRangeSize += 0.3f;
                Player.Instance.NowPlayerWeapon.WeaponRangeSize();
                break;
            case 25://마녀의 항아리
                Player.Instance.BonusHeal +=0.3f;
                break;
            case 26://망령의 로브
                Player.Instance.TrapResistance = true;
                break;
            case 31://굴뚝
                Player.Instance.BonusHeal += 0.3f;
                break;
            case 34://홀리 장식
                ReviveCount += 1;
                break;
            default:
                //Debug.LogError("Wrong Active Artifacts Id" + id);
                break;
        }
    }
}
