using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static AttackPad Instance;
    public Image BG, Stick, CoolWheel;
    public Vector2 inputVector;
    private bool AttackSwitch;
    float AttackCurrentTime;
    float CoolMaxtime;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        AttackSwitch = false;
        AttackCurrentTime = 0;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BG.rectTransform,
                                                                    ped.position,
                                                                    ped.pressEventCamera,
                                                                    out pos))
        {
            pos.x = (pos.x / BG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / BG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Joystick
            Stick.rectTransform.anchoredPosition
                = new Vector2(inputVector.x * (BG.rectTransform.sizeDelta.x / 2),
                              inputVector.y * (BG.rectTransform.sizeDelta.y / 2));

            if (AttackCurrentTime <= 0)
            {
                if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Melee|| Player.Instance.NowPlayerWeapon.nowBullet>=1)
                {
                    CoolMaxtime = Player.Instance.mStats.AtkSpd;
                }
                else if(Player.Instance.NowPlayerWeapon.nowBullet<= 0)
                {
                    CoolMaxtime = Player.Instance.NowPlayerWeapon.mStats.ReloadCool;
                }
                StartCoroutine(CooltimeRoutine(CoolMaxtime));
                AttackSwitch = true;
            }
            //무기 방향 돌리기
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            Player.Instance.NowPlayerWeapon.transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
            if (AttackSwitch == true)
            {
                if (Player.Instance.NowPlayerWeapon.eType==eWeaponType.Melee)
                {
                    Player.Instance.NowPlayerWeapon.MeleeAttack();
                    StartCoroutine(AttackCooltime());
                }
                if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Range)
                {
                    if (Player.Instance.NowPlayerWeapon.nowBullet >= 1)
                    {
                        Player.Instance.NowPlayerWeapon.RangeAttack();
                        StartCoroutine(AttackCooltime());
                    }
                    else
                    {

                        Player.Instance.NowPlayerWeapon.nowBullet = Player.Instance.NowPlayerWeapon.MaxBullet;
                    }
                }
                    
            }
        }

    }



    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (AttackSwitch==false)
        {
            if (Player.Instance.NowPlayerWeapon.Attackon == false)
            {
                OnDrag(ped);
            }
        }
        
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        Stick.rectTransform.anchoredPosition = Vector3.zero;
    }

    private IEnumerator AttackCooltime()
    {
        WaitForSeconds Cool = new WaitForSeconds(Player.Instance.mStats.AtkSpd);
        yield return Cool;
        AttackSwitch = false;

    }

    public void ShowCooltime(float maxTime, float currentTime)
    {
        if (currentTime > 0)
        {
            CoolWheel.gameObject.SetActive(true);
            CoolWheel.fillAmount = currentTime / maxTime;
        }
        else
        {
            CoolWheel.gameObject.SetActive(false);
        }
    }

    private IEnumerator CooltimeRoutine(float maxTime)
    {
        float CoolTime = maxTime;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        AttackCurrentTime = CoolTime;
        while (AttackCurrentTime >= 0)
        {
            yield return frame;
            AttackCurrentTime -= Time.fixedDeltaTime;
            ShowCooltime(CoolTime, AttackCurrentTime);
        }
    }


}
