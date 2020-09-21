using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveArtifacts : MonoBehaviour
{
    //액티브 유물의 기능을 여기서 담당
    public static ActiveArtifacts Instance;

    public GameObject[] mSkillobj;
    public SkillEffect buffEffect;
    private string bufftext, bufftext2;
    private TextEffect effect;

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
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.white);
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "피해 면역!";
        }
        else
        {
            bufftext = "Damage Resistance!";
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
        yield return dura;
        Player.Instance.Nodamage = false;
    }

    public IEnumerator HonnyPot()
    {
        WaitForSeconds dura = new WaitForSeconds(4.5f);
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
        BuffController.Instance.RemoveNurf();
        BuffController.Instance.SetBuff(2, 2, eBuffType.Buff, 5f);
        BuffController.Instance.SetBuff(3, 3, eBuffType.Buff, 5f);
        Player.Instance.buffIncrease[2] += 0.4f;
        Player.Instance.buffIncrease[3] += 0.4f;
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.yellow);
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.cyan);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "이동 속도 증가!";
            bufftext2 = "공격 속도 증가!";
        }
        else
        {
            bufftext = "Movement speed increase!";
            bufftext2 = "Attack speed increase!";
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.transform.position += new Vector3(0, -0.6f, 0);
        effect.SetText(bufftext2);
        yield return dura;
        Player.Instance.buffIncrease[2] -= 0.4f;
        Player.Instance.buffIncrease[3] -= 0.4f;
    }

    public void UnbrandedCan()
    {
        float rand = Random.Range(0, 3f);
        if (rand<=1f)
        {
            if (GameSetting.Instance.Language == 0)
            {
                bufftext = "공격력 증가!";
                bufftext2 = "공격 속도 증가!";
            }
            else
            {
                bufftext = "Attack increase!";
                bufftext2 = "Attack speed increase!";
            }
            Player.Instance.DoEffect(1, 7f, 1, 0.3f);
            buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.red);
            BuffEffectController.Instance.EffectList.Add(buffEffect);
            effect = TextEffectPool.Instance.GetFromPool(0);
            effect.SetText(bufftext);
            Player.Instance.DoEffect(3, 7f, 3, 0.3f);
            buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.yellow);
            BuffEffectController.Instance.EffectList.Add(buffEffect);
            effect = TextEffectPool.Instance.GetFromPool(0);
            effect.transform.position += new Vector3(0, -0.6f, 0);
            effect.SetText(bufftext2);
        }
        else if (rand>1f&&rand<=2f)
        {
            float amount = Player.Instance.mMaxHP * 0.3f;
            Player.Instance.Heal(amount);
        }
        else
        {
            StartCoroutine(CCReduce());
        }
    }

    private IEnumerator CCReduce()
    {
        WaitForSeconds delay = new WaitForSeconds(7f);
        BuffController.Instance.SetBuff(8, 8, eBuffType.Buff, 7);
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 7, 0, Color.magenta);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "상태 이상 면역!";
        }
        else
        {
            bufftext = "Debuff Resistance!";
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
        Player.Instance.NoCC = true;
        yield return delay;
        Player.Instance.NoCC = false;
    }
}
