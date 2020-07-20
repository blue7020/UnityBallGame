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
    }
    private void Start()
    {
        if (GameController.Instance.Level >= 5)
        {
            NowBoss = StageBossArr[Player.Instance.mNowStage-1];
        }
        else if (GameController.Instance.Level < 5)
        {
            int rand = Random.Range(0, BossArr.Length);
            NowBoss = BossArr[rand];
        }
        Instantiate(NowBoss,transform.position,Quaternion.identity);
    }

    public void BossDeath()
    {
        portal.ShowPortal();
    }
}
