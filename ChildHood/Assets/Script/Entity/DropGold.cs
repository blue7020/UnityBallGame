using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public float Gold;
    private int GoldStack;//골드 중첩

    public bool DropEnd;

    // Start is called before the first frame update
    void Start()
    {
        Gold = Enemy.Instance.mInfoArr[Enemy.Instance.mID].Gold;
    }

    private void FixedUpdate()
    {
        //몬스터 사망 시 드롭되게 해야함
        if (DropEnd == true)
        {
            GoldDrop();
            
        }
    }

    private void GoldDrop()
    {
        Debug.Log("GoldDrop");
        gameObject.SetActive(true);
        transform.position = Enemy.Instance.mGoldDropPos;
        if (GoldStack == 1)
        {
            //스프라이트 변경
        }
        else if (GoldStack == 3)
        {
            //스프라이트 변경
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Player.Instance.mInfoArr[Player.Instance.mID].Gold += Gold;
            gameObject.SetActive(false);
        }
        if (other.gameObject.GetComponent<DropGold>())
        {
            GoldStack++;
            //TODO other가 골드라면 other와 이것의 골드를 비교한 후 더 작은 쪽이 큰 쪽한테 골드를 넘겨주고 gameobject 비활성화
        }
    }
}
