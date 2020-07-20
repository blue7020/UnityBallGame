using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public static PortalTrigger Instance;

#pragma warning disable 0649
    [SerializeField]
    private Portal portal;
    [SerializeField]
    private Enemy[] BossArr;
    [SerializeField]
    private Enemy[] StageBossArr;
    private Enemy NowBoss;
    private int rand;
#pragma warning restore 0649

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
            NowBoss = StageBossArr[0];//TODO 스테이지에 따라 보스 다르게!
        }
        else if (GameController.Instance.Level < 5)
        {
            NowBoss = BossArr[rand];
        }
        Instantiate(NowBoss,transform.position,Quaternion.identity);
    }

    public void BossDeath()
    {
        portal.ShowPortal();
    }
}
