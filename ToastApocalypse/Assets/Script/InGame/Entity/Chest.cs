using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour
{
    public GameObject mItem;
    public SpriteRenderer mRenderer;
    public Sprite[] mSprites;
    public MimicSpawner mMimicPos;
    public Room Currentroom;

    private bool ChestOpen;
    private eChestType Type;
    
    private void Awake()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                Type = eChestType.Wood;
                mMimicPos.Type = eChestType.Wood;
                Wood();
                break;
            case 1:
                Type = eChestType.Silver;
                mMimicPos.Type = eChestType.Silver;
                Silver();
                break;
            case 2:
                Type = eChestType.Gold;
                mMimicPos.Type = eChestType.Gold;
                Gold();
                break;
            default:
                Debug.LogError("Wrong ChestType");
                break;
        }
        ChestOpen = false;
    }

    private void Start()
    {
        while (true)
        {
            //TODO 상자의 등급에 따라 아이템 배열 다르게 설정
            int rand = Random.Range(0, 4);
            if (Player.Instance.NowPlayerWeapon.mID!=rand)
            {
                Weapon mWeapon = WeaponPool.Instance.GetFromPool(rand);
                mWeapon.transform.SetParent(mItem.transform);
                mWeapon.Currentroom = Currentroom;
                break;
            }
        }
    }

    private void Wood()
    {
        float rand;

        rand = Random.Range(0, 1f);
        if (rand > 0.4f)//상자
        {
            mRenderer.sprite = mSprites[0];
        }
        else//미믹
        {
            mMimicPos.gameObject.SetActive(true);
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
            mMimicPos.gameObject.SetActive(true);
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
            mMimicPos.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void Open()
    {
        ChestOpen = true;
        mItem.SetActive(true);
        int randx = Random.Range(-2, 2);
        int randy = Random.Range(-2, 2);
        mItem.transform.localPosition = gameObject.transform.position + new Vector3(randx, randy, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (ChestOpen == false)
            {
                Open();
                
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
            }

        }
    }
}
