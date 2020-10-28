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
            if (WeaponController.Instance.mWeapons.Count >= 0)
            {
                StartCoroutine(WeaponSearch());
            }
            else
            {
                RandomItem();
            }
        }
        else if (rand >= mRate[1] && rand < mRate[2])//패시브유물
        {
            if (ArtifactController.Instance.mPassiveArtifact.Count >= 0)
            {
                StartCoroutine(PassiveArtifactSearch());
            }
            else
            {
                RandomItem();
            }
        }
        else//액티브 유물
        {
            if (ArtifactController.Instance.mActiveArtifact.Count>=0)
            {
                StartCoroutine(ActiveArtifactSearch());
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

    private IEnumerator WeaponSearch()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int rand = Random.Range(0, WeaponController.Instance.mWeapons.Count);
            if (Player.Instance.NowPlayerWeapon != WeaponController.Instance.mWeapons[rand])
            {
                rand = Random.Range(0, WeaponController.Instance.mWeapons.Count);
                weapon = WeaponPool.Instance.GetFromPool(rand);
                weapon.transform.SetParent(mItem.transform);
                weapon.Currentroom = Currentroom;
                WeaponController.Instance.mWeapons.RemoveAt(rand);
                weapon.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);
                break;
            }
            yield return delay;
        }
    }
    private IEnumerator PassiveArtifactSearch()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        index = 0;
        while (true)
        {
            int rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
            if (InventoryController.Instance.mSlotArr[index] != ArtifactController.Instance.mPassiveArtifact[rand])
            {
                rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
                artifact = Instantiate(ArtifactController.Instance.mPassiveArtifact[rand], mItem.transform);
                artifact.Currentroom = Currentroom;
                artifact.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);
                ArtifactController.Instance.mPassiveArtifact.Remove(artifact);
                break;
            }
            index++;
            yield return delay;
        }
    }
    private IEnumerator ActiveArtifactSearch()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int rand = Random.Range(0, ArtifactController.Instance.mActiveArtifact.Count);
            if (Player.Instance.NowActiveArtifact != ArtifactController.Instance.mActiveArtifact[rand])
            {
                rand = Random.Range(0, ArtifactController.Instance.mActiveArtifact.Count);
                artifact = Instantiate(ArtifactController.Instance.mActiveArtifact[rand], mItem.transform);
                artifact.Currentroom = Currentroom;
                artifact.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);
                ArtifactController.Instance.mActiveArtifact.Remove(artifact);
                break;
            }
            yield return delay;
        }
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
