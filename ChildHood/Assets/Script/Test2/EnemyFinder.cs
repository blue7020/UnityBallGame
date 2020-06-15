using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    [SerializeField]
    private Room room;
    [SerializeField]
    private GameObject mDoor;

    public int mMonsterCount;

    private void Start()
    {
        mMonsterCount = 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Player.Instance.NowEnemyCount == 0)
            {
                mDoor.gameObject.SetActive(false);
            }
        }
    }
}
