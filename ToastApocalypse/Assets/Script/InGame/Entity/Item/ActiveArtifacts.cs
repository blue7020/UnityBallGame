using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveArtifacts : MonoBehaviour
{
    //액티브 유물의 기능을 여기서 담당
    public static ActiveArtifacts Instance;
    public GameObject[] mSkillobj;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void ArtifactsFuntion(int id)
    {
        switch (id)
        {
            case 3:
                StartCoroutine(SFC());
                break;
            case 7:
                StartCoroutine(HonnyPot());
                break;
            case 11:
                PetCookie();
                break;
            case 15:
                StartCoroutine(FreshMilk());
                break;
            case 19:
                UnbrandedCan();
                break;
            default:
                Debug.LogError("Wrong Active Artifacts Id");
                break;
        }
    }

    public IEnumerator SFC()
    {
        WaitForSeconds dura = new WaitForSeconds(5f);
        BuffController.Instance.SetBuff(7, 7, eBuffType.Buff, 5f);
        Player.Instance.Nodamage = true;
        yield return dura;
        Player.Instance.Nodamage = false;
    }

    public IEnumerator HonnyPot()
    {
        WaitForSeconds dura = new WaitForSeconds(4f);
        GameObject honny = Instantiate(mSkillobj[0],Player.Instance.CurrentRoom.transform);
        honny.transform.position = Player.Instance.transform.position;
        yield return dura;
        honny.SetActive(false);
    }

    public void PetCookie()
    {
        Player.Instance.Heal(1);
    }

    public IEnumerator FreshMilk()
    {
        WaitForSeconds dura = new WaitForSeconds(5f);
        BuffController.Instance.SetBuff(3, 3, eBuffType.Buff, 5f);
        BuffController.Instance.SetBuff(4, 4, eBuffType.Buff, 5f);
        Player.Instance.buffIncrease[3] += 0.4f;
        Player.Instance.buffIncrease[4] += 0.4f;
        yield return dura;
        Player.Instance.buffIncrease[3] -= 0.4f;
        Player.Instance.buffIncrease[4] -= 0.4f;
    }

    public void UnbrandedCan()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                Player.Instance.Speed(0.4f,3,7);
                break;
            case 1:
                Player.Instance.Heal((Player.Instance.mMaxHP*0.3f));
                break;
            case 2:
                Player.Instance.CCreduce(1,103,7);
                break;
        }
    }
}
