using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static AttackPad Instance;
    [SerializeField]
    private Image BG, Stick, CoolWheel;
    public Vector2 inputVector;
    private bool AttackSwitch;
    float AttackCurrentTime;

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
        if (AttackCurrentTime <= 0)
        {
            StartCoroutine(CooltimeRoutine());
            AttackSwitch = true;
        }
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

            //무기 방향 돌리기
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            Player.Instance.NowPlayerWeapon.transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
            if (AttackSwitch == true)
            {
                if (Player.Instance.NowPlayerWeapon.eType==eWeaponType.Melee)
                {
                    Player.Instance.NowPlayerWeapon.MeleeAttack();
                }
                if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Range)
                {
                    Player.Instance.NowPlayerWeapon.RangeAttack();
                }
                    StartCoroutine(AttackCooltime());
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
        WaitForSeconds Cool = new WaitForSeconds(Player.Instance.Stats.AtkSpd);
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

    private IEnumerator CooltimeRoutine()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float maxTime = Player.Instance.Stats.AtkSpd;
        AttackCurrentTime = maxTime;
        while (AttackCurrentTime >= 0)
        {
            yield return frame;
            AttackCurrentTime -= Time.fixedDeltaTime;
            ShowCooltime(maxTime, AttackCurrentTime);
        }
    }


}
