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
    private string bufftext;
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
                FreshMilk();
                break;
            case 19:
                UnbrandedCan();
                break;
            case 22:
                JackOLantern();
                break;
            case 23:
                PieBoom();
                break;
            case 27:
                GhostPackgage();
                break;
            case 30:
                SurpriseGift();
                break;
            case 35:
                ChristmasCookie();
                break;
            default:
                //Debug.LogError("Wrong Active Artifacts Id");
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
        effect = null;
        yield return dura;
        Player.Instance.Nodamage = false;
    }

    public IEnumerator HonnyPot()
    {
        WaitForSeconds dura = new WaitForSeconds(4.5f);
        GameObject honny = Instantiate(mSkillobj[0],Player.Instance.CurrentRoom.transform);
        honny.transform.position = Player.Instance.transform.position;
        yield return dura;
        if (honny!=null)
        {
            honny.SetActive(false);
        }
    }

    public void PetCookie()
    {
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "체력 회복!";
        }
        else
        {
            bufftext = "Restore HP!";
        }
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
        effect = null;
        Player.Instance.Heal(1);
    }

    public void FreshMilk()
    {
        BuffController.Instance.RemoveNurf();
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.yellow);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.cyan);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "이동 속도, 공격 속도 증가!";
        }
        else
        {
            bufftext = "Movement speed, Attack speed increase!";
        }
        Player.Instance.DoEffect(4, 7f, 4, 0.4f);
        Player.Instance.DoEffect(3, 7f, 3, 0.4f);
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
        effect = null;
    }

    public void UnbrandedCan()
    {
        float rand = Random.Range(0, 3f);
        if (rand <= 1f)
        {
            if (GameSetting.Instance.Language == 0)
            {
                bufftext = "공격력, 공격 속도 증가!";
            }
            else
            {
                bufftext = "Attack, Attack speed increase!";
            }
            Player.Instance.DoEffect(1, 7f, 1, 0.3f);
            buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.red);
            BuffEffectController.Instance.EffectList.Add(buffEffect);
            Player.Instance.DoEffect(3, 7f, 3, 0.3f);
            buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.yellow);
            BuffEffectController.Instance.EffectList.Add(buffEffect);
            effect = TextEffectPool.Instance.GetFromPool(0);
            effect.SetText(bufftext);
            effect = null;
        }
        else if (rand > 1f && rand <= 2f)
        {
            float amount = Player.Instance.mMaxHP * 0.3f;
            Player.Instance.Heal(amount);
            if (GameSetting.Instance.Language == 0)
            {
                bufftext = "체력 회복!";
            }
            else
            {
                bufftext = "Restore HP!";
            }
            effect = TextEffectPool.Instance.GetFromPool(0);
            effect.SetText(bufftext);
            effect = null;
        }
        else
        {
            CCReduce();
        }
    }

    private void CCReduce()
    {
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "상태 이상 면역!";
        }
        else
        {
            bufftext = "Debuff Resistance!";
        }
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 7, 0, Color.magenta);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        Player.Instance.DoEffect(5, 7f, 7, 1f);
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
        effect = null;
    }

    public void PieBoom()
    {
        PlayerBullet bolt = Instantiate(SkillList.Instance.Skillbullet[2], Player.Instance.transform);
        bolt.mDamage = (0.8f * Player.Instance.mStats.Atk);
        bolt.mRB2D.AddForce(Player.Instance.mDirection.transform.up * bolt.mSpeed, ForceMode2D.Impulse);
    }

    public void JackOLantern()
    {
        Turret turret = Instantiate(SkillList.Instance.SkillTurret[1], Player.Instance.transform);
        turret.transform.position = Player.Instance.transform.position+new Vector3(0,1.5f,0);
    }

    public void GhostPackgage()
    {
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 7, 0, Color.magenta);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.yellow);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        buffEffect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
        buffEffect.SetEffect(BuffEffectController.Instance.mSprite[0], 5, 0, Color.cyan);
        BuffEffectController.Instance.EffectList.Add(buffEffect);
        if (GameSetting.Instance.Language == 0)
        {
            bufftext = "상태 이상 면역, 이동 속도, 공격 속도 증가!";
        }
        else
        {
            bufftext = "Debuff Resistance, Movement speed, Attack speed increase!";
        }
        Player.Instance.DoEffect(5, 7f, 7, 1f);
        Player.Instance.DoEffect(4, 7f, 4, 0.4f);
        Player.Instance.DoEffect(3, 7f, 3, 0.4f);
        effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(bufftext);
        effect = null;
    }

    public void SurpriseGift()
    {
        Turret turret = Instantiate(SkillList.Instance.SkillTurret[2], Player.Instance.transform);
        turret.transform.position = Player.Instance.transform.position + new Vector3(0, 1.5f, 0);
    }

    public void ChristmasCookie()
    {
        Player.Instance.Heal(Player.Instance.mMaxHP*0.2f);
    }
}
