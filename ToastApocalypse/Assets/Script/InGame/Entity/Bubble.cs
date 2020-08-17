using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public bool TrapTrigger;
    public bool IsUSe;

    public IEnumerator BubbleTile()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        while (true)
        {
            if (TrapTrigger == true)
            {
                if (Player.Instance.mCurrentAir + 20 >= Player.MAX_AIR)
                {
                    Player.Instance.mCurrentAir = Player.MAX_AIR;
                }
                else
                {
                    Player.Instance.mCurrentAir += 20;
                }
            }
            UIController.Instance.ShowAirGaugeBar();
            yield return delay;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TrapTrigger = true;
            if (TrapTrigger == true && !IsUSe)
            {
                IsUSe = true;
                Player.Instance.OnAir = true;
                StartCoroutine(BubbleTile());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.OnAir = false;
            TrapTrigger = false;
            IsUSe = false;
            StopCoroutine(BubbleTile());
        }
    }
}
