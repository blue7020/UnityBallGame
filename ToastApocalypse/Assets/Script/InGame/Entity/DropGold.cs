using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public SpriteRenderer mRenderer;

    public bool DropEnd;
    public float mGold;

    public void GoldDrop(float Gold)
    {
        int rand = Random.Range(-1, 3);
        mGold = Gold+rand;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            SoundController.Instance.SESoundUI(2);
            Player.Instance.mStats.Gold += mGold;
            UIController.Instance.ShowGold();
            gameObject.SetActive(false);
        }
        
    }
}
