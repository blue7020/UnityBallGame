using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mRenderer;
    [SerializeField]
    private Sprite[] mSprites;

    private float mGold;
    public bool DropEnd;
    private int GoldStack =0;//골드 중첩

    private void Awake()
    {
        gameObject.SetActive(false);
        mRenderer.sprite = mSprites[0];
    }

    public void GoldDrop(Vector2 Pos, float Gold)
    {
        transform.position = Pos;
        mGold = Gold;
        if (GoldStack == 1)
        {
            mRenderer.sprite = mSprites[1];
        }
        else if (GoldStack == 3)
        {
            mRenderer.sprite = mSprites[2];
        }
        Enemy.Instance.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DropGold>())
        {
            GoldStack++;
            //TODO other가 골드라면 other와 이것의 골드를 비교한 후 더 작은 쪽이 큰 쪽한테 골드를 넘겨주고 gameobject 비활성화
        }
        if (other.gameObject.GetComponent<Player>())
        {
            Debug.Log("Goldeat " + mGold);
            Player.Instance.mInfoArr[Player.Instance.mID].Gold += mGold;
            gameObject.SetActive(false);
        }
        
    }
}
