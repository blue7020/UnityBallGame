using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chest : MonoBehaviour
{
    public GameObject mItem;
    private Rigidbody2D mRB2D;
    public SpriteRenderer mRenderer;
    public Sprite[] mSprites;
    public MimicSpawner mMimicPos;
    public Room Currentroom;

    public List<UsingItem> mItemList;

    private bool ChestOpen;
    private eChestType Type;
    private Weapon weapon;
    private Artifacts artifact;
    private UsingItem item;
    private int index;
    private Button mUIChestButton;

    private float[] mRate = new float[3];//0=무기 확률 1=패시브 유물 확률 2=액티브 유물 확률;

    private void Awake()
    {
        for (int i=0; i<GameSetting.Instance.mItemArr.Length;i++)
        {
            if (SaveDataController.Instance.mUser.ItemOpen[i]==true)
            {
                mItemList.Add(GameSetting.Instance.mItemArr[i]);
            }
        }
        mUIChestButton = UIController.Instance.mUIChestButton;
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

    private void Wood()
    {
        mRate[0] = 0.45f;
        mRate[1] = 0.4f;
        mRate[2] = 0.15f;
        float rand = Random.Range(0, 1f);
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
        mRate[0] = 0.2f;
        mRate[1] = 0.45f;
        mRate[2] = 0.35f;
        float rand = Random.Range(0, 1f);
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
        mRate[0] = 0.15f;
        mRate[1] = 0.3f;
        mRate[2] = 0.55f;
        float rand = Random.Range(0, 1f);
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
        mUIChestButton.gameObject.SetActive(false);
        ChestOpen = true;
        SoundController.Instance.SESound(18);
        float rand = Random.Range(0, 1f);
        if (rand >= mRate[0] && rand < mRate[1])//무기
        {
            if (GameController.Instance.mWeaponList.Count > 0)
            {
                WeaponSearch();
            }
            else
            {
                RandomItem();
            }
        }
        else if (rand >= mRate[1] && rand < mRate[2])//패시브유물
        {
            if (GameController.Instance.mPassiveArtifactList.Count > 0)
            {
                PassiveArtifactSearch();
            }
            else
            {
                RandomItem();
            }
        }
        else//액티브 유물
        {
            if (GameController.Instance.mActiveArtifactList.Count>0)
            {
                ActiveArtifactSearch();
            }
            else
            {
                RandomItem();
            }
        }
        mItem.SetActive(true);
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

    private void RandomItem()
    {
        int rand = Random.Range(0, mItemList.Count);
        item = Instantiate(mItemList[rand], mItem.transform);
        item.Currentroom = Currentroom;
        item.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);

    }

    private void WeaponSearch()
    {

        int rand = Random.Range(0, GameController.Instance.mWeaponList.Count);
        rand = Random.Range(0, GameController.Instance.mWeaponList.Count);
        weapon = Instantiate(GameController.Instance.mWeaponList[rand], mItem.transform);
        weapon.Currentroom = Currentroom;
        GameController.Instance.mWeaponList.RemoveAt(rand);
        weapon.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);
    }

    private void PassiveArtifactSearch()
    {
        int rand = Random.Range(0, GameController.Instance.mPassiveArtifactList.Count);
        rand = Random.Range(0, GameController.Instance.mPassiveArtifactList.Count);
        artifact = Instantiate(GameController.Instance.mPassiveArtifactList[rand], mItem.transform);
        artifact.Currentroom = Currentroom;
        artifact.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);
        GameController.Instance.mPassiveArtifactList.Remove(artifact);
    }
    private void ActiveArtifactSearch()
    {
        int rand = Random.Range(0, GameController.Instance.mActiveArtifactList.Count);
        rand = Random.Range(0, GameController.Instance.mActiveArtifactList.Count);
        artifact = Instantiate(GameController.Instance.mActiveArtifactList[rand], mItem.transform);
        artifact.Currentroom = Currentroom;
        artifact.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);
        GameController.Instance.mActiveArtifactList.Remove(artifact);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ChestOpen == false)
            {
                mUIChestButton.onClick.RemoveAllListeners();
                mUIChestButton.onClick.AddListener(() => { Open(); });
                mUIChestButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mUIChestButton.gameObject.SetActive(false);
        }
    }
}
