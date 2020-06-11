using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField]
    private Portal portal;
    [SerializeField]
    private Enemy Boss;

    private void Update()
    {
        if (Boss.BossDeath == true)
        {
            Player.Instance.Level = 1;
            portal.ShowPortal();
            Boss.BossDeath = false;
        }
    }
}
