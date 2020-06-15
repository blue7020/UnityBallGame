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
        if (Player.Instance.Level<5)
        {
            rand = Random.Range(0, BossArr.Length);
            NowBoss = BossArr[rand];
        }
        else if(Player.Instance.Level ==5)
        {
            NowBoss = StageBossArr[0];
            //TODO 스테이지 정보에 따라 다르게
        }
        
    }
    private void Start()
    {
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
