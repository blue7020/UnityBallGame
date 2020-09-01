using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour
{
    public GameObject mItem;
    private Rigidbody2D mRB2D;
    public SpriteRenderer mRenderer;
    public Sprite[] mSprites;
    public MimicSpawner mMimicPos;
    public Room Currentroom;

    public UsingItem DefaultItem;

    private bool ChestOpen;
    private eChestType Type;
    private Weapon weapon;
    private Artifacts artifact;
    private int index;

    private float[] rate=new float[3];//0=무기 확률 1=패시브 유물 확률 2=액티브 유물 확률

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

    private void Wood()
    {
        rate[0] = 0.45f;
        rate[1] = 0.40f;
        rate[2] = 0.15f;
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
        rate[0] = 0.15f;
        rate[1] = 0.5f;
        rate[2] = 0.35f;
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
        rate[0] = 0.1f;
        rate[1] = 0.35f;
        rate[2] = 0.55f;
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
        ChestOpen = true;
        float rand = Random.Range(0, 1f);
        if (WeaponController.Instance.mWeapons.Count>= 0&&rand >= rate[0] && rand < rate[1])//무기
        {
            StartCoroutine(WeaponSearch());
        }
        else if (ArtifactController.Instance.mPassiveArtifact.Count >= 0&&rand >= rate[1] && rand <= rate[2])//패시브유물
        {
            StartCoroutine(PassiveArtifactSearch());
        }
        else if (ArtifactController.Instance.mActiveArtifact.Count>=0&&rand >= rate[2])//액티브 유물
        {
            StartCoroutine(ActiveArtifactSearch());
        }
        else
        {
            UsingItem item = Instantiate(DefaultItem, mItem.transform);
            weapon.transform.position = Player.Instance.transform.position - new Vector3(0, -2, 0);
        }
        mItem.SetActive(true);
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
