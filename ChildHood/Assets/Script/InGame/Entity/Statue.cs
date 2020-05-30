using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public static Statue Instance;

    [SerializeField]
    private float CoolTime;
    private bool IsCoolTime;
    [SerializeField]
    private float mHealAmount;

    [SerializeField]
    public SpriteRenderer mRenderer;
    [SerializeField]
    public Sprite[] mSprites;

    [SerializeField]
    private eStatueType Type;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        IsCoolTime = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsCoolTime == false)
            {
                if (Type == eStatueType.Heal)
                {
                    Debug.Log("heal2");
                }
                StartCoroutine(Cooltime());
            }
            else
            {
                Debug.Log("Cooltime");
            }
        }
    }

    private IEnumerator Cooltime()
    {
        //밸런스 조정으로 쿨타임 기능은 사라질 수도 있음
        WaitForSeconds Cool = new WaitForSeconds(CoolTime);
        IsCoolTime = true;
        
        switch (Type)
        {
            case eStatueType.Heal:
                Heal();
                mRenderer.sprite = mSprites[1];
                break;
            case eStatueType.Speed:
                break;
            case eStatueType.Strength:
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
        
        yield return Cool;
        IsCoolTime = false;
        mRenderer.sprite = mSprites[0];
    }

    private void Heal()
    {
        Debug.Log("healing");
        if ((Player.Instance.mCurrentHP + mHealAmount) >= Player.Instance.mMaxHP)
        {
            Player.Instance.mCurrentHP = Player.Instance.mMaxHP;
        }
        else
        {
            Player.Instance.mCurrentHP += mHealAmount;
        }
        UIController.Instance.ShowHP();
    }
}
