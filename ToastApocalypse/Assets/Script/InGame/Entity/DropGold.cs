using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public SpriteRenderer mRenderer;
    public Sprite[] mSprites;

    public bool DropEnd;
    public float mGold;

    public void GoldDrop(DropGold dropGold, float Gold)
    {
        mGold = Gold;
        if (mGold >= 20 && mGold < 40)
        {
            dropGold.mRenderer.sprite = dropGold.mSprites[1];
        }
        else if (mGold >= 40)
        {
            dropGold.mRenderer.sprite = dropGold.mSprites[2];
        }
        else
        {
            mRenderer.sprite = mSprites[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //골드가 겹치면 골드 스택 증가
        //TODO 골드 스택이 증가하면 쌓인 골드 둘을 비교한 후 더 작은 쪽이 큰 쪽한테 골드를 넘겨주고 gameobject 비활성화
        if (other.gameObject.GetComponent<Player>())
        {
            Player.Instance.mStats.Gold += mGold;
            UIController.Instance.ShowGold();
            gameObject.SetActive(false);
        }
        
    }
}
