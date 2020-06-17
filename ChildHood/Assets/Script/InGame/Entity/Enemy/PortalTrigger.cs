using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public static PortalTrigger Instance;

    [SerializeField]
    private Portal portal;
    [SerializeField]
    private Enemy[] BossArr;
    [SerializeField]
    private Enemy[] StageBossArr;
    private Enemy NowBoss;

    public bool BossDeath;

    private int rand;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rand = Random.Range(0, BossArr.Length);
    }
    private void Start()
    {
        if (Player.Instance.Level >= 5)
        {
            NowBoss = StageBossArr[0];
            //TODO 스테이지 정보에 따라 다르게
        }
        else if (Player.Instance.Level < 5)
        {
            NowBoss = BossArr[rand];
        }
        
        BossDeath = false;
        Instantiate(NowBoss,transform.position,Quaternion.identity);
    }

    private void Update()
    {
        if (BossDeath == true)
        {
            if (portal.Type == ePortalType.Stage)
            {
                if (Player.Instance.Level == 5)
                {
                    Player.Instance.Level = 1;
                }
            }
            portal.ShowPortal();
            BossDeath = false;
        }
    }
}
