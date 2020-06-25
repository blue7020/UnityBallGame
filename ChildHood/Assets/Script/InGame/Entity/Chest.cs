using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    public GameObject mItem;
    [SerializeField]
    public SpriteRenderer mRenderer;
    [SerializeField]
    private Sprite[] mSprites;
    [SerializeField]
    private GameObject mMimicPos;

    private bool ChestOpen;
    private eChestType Type;
    
    private void Start()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                Type = eChestType.Wood;
                Wood();
                break;
            case 1:
                Type = eChestType.Silver;
                Silver();
                break;
            case 2:
                Type = eChestType.Gold;
                Gold();
                break;
            default:
                Debug.LogError("Wrong ChestType");
                break;
        }
        ChestOpen = false;
    }

    private void Wood()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.5f)//상자
        {
            mRenderer.sprite = mSprites[0];
        }
        else//미믹
        {
            Enemy mEnemy = mEnemyPool.GetFromPool(0);
            mEnemy.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }

    private void Silver()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.3f)//상자
        {
            mRenderer.sprite = mSprites[2];
        }
        else//미믹
        {  
            Enemy mEnemy = mEnemyPool.GetFromPool(1);
            mEnemy.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }

    private void Gold()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.1f)//상자
        {
            mRenderer.sprite = mSprites[4];
        }
        else//미믹
        {
            Enemy mEnemy = mEnemyPool.GetFromPool(2);
            mEnemy.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ChestOpen == false)
            {
                ChestOpen = true;
                switch (Type)
                {
                    case eChestType.Wood:
                        mRenderer.sprite = mSprites[1];
                        break;
                    case eChestType.Silver:
                        mRenderer.sprite = mSprites[3];
                        break;
                    case eChestType.Gold:
                        mRenderer.sprite = mSprites[5];
                        break;
                    default:
                        Debug.LogError("Wrong Chest Sprite");
                        break;
                }
                //상자의 등급에 따라 아이템 배열 다르게
            }

        }
    }
}
