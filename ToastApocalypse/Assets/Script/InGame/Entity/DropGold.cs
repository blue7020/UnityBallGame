using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public SpriteRenderer mRenderer;
    public Sprite[] mSprites;

    public bool DropEnd;
    public float mGold;

    public void GoldDrop(float Gold)
    {
        int rand = Random.Range(-1, 2);
        mGold = Gold+rand;
        if (mGold >= 20 && mGold < 40)
        {
            mRenderer.sprite = mSprites[1];
        }
        else if (mGold >= 40)
        {
            mRenderer.sprite = mSprites[2];
        }
        else
        {
            mRenderer.sprite = mSprites[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Player.Instance.mStats.Gold += mGold;
            UIController.Instance.ShowGold();
            gameObject.SetActive(false);
        }
        
    }
}
