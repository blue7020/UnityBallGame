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
        if (GameController.Instance.Level >= 5)
        {
            NowBoss = StageBossArr[0];
            //TODO 스테이지 정보에 따라 다르게
        }
        else if (GameController.Instance.Level < 5)
        {
            NowBoss = BossArr[rand];
        }
        Instantiate(NowBoss,transform.position,Quaternion.identity);
    }

    public void BossDeath()
    {

        if (portal.Type == ePortalType.Stage)
        {
            if (GameController.Instance.Level == 5)
            {
                GameController.Instance.Level = 1;
            }
        }
        portal.ShowPortal();
    }
}
