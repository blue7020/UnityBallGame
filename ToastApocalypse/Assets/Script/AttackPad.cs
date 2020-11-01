using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackPad : MonoBehaviour, IDragHandler, IEndDragHandler,IBeginDragHandler//,IPointerUpHandler, IPointerDownHandler
{
    public static AttackPad Instance;
    public Image BG, Stick, CoolWheel;
    public Vector2 inputVector;
    private bool AttackSwitch,IsReload,AttackEnd, check;
    float AttackCurrentTime;
    float CoolMaxtime;
    private Coroutine mCycle;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            AttackSwitch = false;
            AttackCurrentTime = 0;
            IsReload = false;
            AttackEnd = false;
            mCycle = null;
        }
        else
        {
            Delete();
        }
    }
    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        if (Player.Instance.NowPlayerWeapon != null)
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
                    = new Vector2(inputVector.x * (BG.rectTransform.sizeDelta.x / 2 / 2),
                                  inputVector.y * (BG.rectTransform.sizeDelta.y / 2) / 2);
                //무기 방향 돌리기
                float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
                Player.Instance.NowPlayerWeapon.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                if (Player.Instance.NowPlayerWeapon.IsSpin == true)
                {
                    Quaternion q1 = Player.Instance.NowPlayerWeapon.transform.rotation;
                    Quaternion q2 = Quaternion.Euler(0, -180, Player.Instance.NowPlayerWeapon.transform.rotation.z);
                    float dot = Quaternion.Dot(q1, q2);
                    if (dot > 0 && dot < 4.1e-8)
                    {
                        Player.Instance.NowPlayerWeapon.mRenderer.flipY = true;
                    }
                    else
                    {
                        Player.Instance.NowPlayerWeapon.mRenderer.flipY = false;
                    }
                }
            }
            AttackEnd = false;

        }
    }

    public IEnumerator AttackCycle()
    {
        WaitForSeconds time = new WaitForSeconds(0.1f);
        float currentTime = 0;
        while (check)
        {
            float Maxtime = Player.Instance.mStats.AtkSpd * (1 - (Player.Instance.AttackSpeedStat + Player.Instance.buffIncrease[2]));
            if (AttackEnd==false)
            {
                if (currentTime==0)
                {
                    Attack();
                }
                if (currentTime >= Maxtime)
                {
                    currentTime = 0;
                    Attack();
                }
            }
            else
            {
                break;
            }
            currentTime += 0.1f;
            yield return time;
        }
        StopCoroutine(AttackCycle());
        mCycle = null;
    }

    private void Attack()
    {
        if (Player.Instance.NowPlayerWeapon!=null&&AttackCurrentTime <= 0)
        {
            if (Player.Instance.Stun == false)
            {
                if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Melee || Player.Instance.NowPlayerWeapon.nowBullet > 0)
                {
                    CoolMaxtime = Player.Instance.mStats.AtkSpd * (1 - (Player.Instance.AttackSpeedStat + Player.Instance.buffIncrease[2]));
                    StartCoroutine(CooltimeRoutine(CoolMaxtime));
                    if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Melee)
                    {
                        Player.Instance.NowPlayerWeapon.MeleeAttack();
                    }
                    else
                    {
                        if (Player.Instance.NowPlayerWeapon.nowBullet > 0)
                        {
                            if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Range)
                            {
                                Player.Instance.NowPlayerWeapon.RangeAttack();
                            }
                            else if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Fire)
                            {
                                Player.Instance.NowPlayerWeapon.FireAttack();
                            }
                        }
                    }
                }
                else if(Player.Instance.NowPlayerWeapon.eType != eWeaponType.Melee&& Player.Instance.NowPlayerWeapon.nowBullet <= 0)
                {
                    if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Fire)
                    {
                        Player.Instance.NowPlayerWeapon.mAttackArea.FireStarter.Stop();
                    }
                    float reloadCool = Player.Instance.NowPlayerWeapon.mStats.ReloadCool;
                    CoolMaxtime = reloadCool * (1 - PassiveArtifacts.Instance.ReloadCooltimeReduce);
                    IsReload = true;
                    StartCoroutine(CooltimeRoutine(CoolMaxtime));
                }
            }
        }
    }

    public void ShowCooltime(float maxTime, float currentTime)
    {
        CoolWheel.gameObject.SetActive(true);
        if (currentTime > 0)
        {
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
            AttackSwitch = true;
            yield return frame;
            AttackCurrentTime -= Time.fixedDeltaTime;
            ShowCooltime(CoolTime, AttackCurrentTime);
        }
        if (IsReload == true)
        {
            SoundController.Instance.SESound(7);
            Player.Instance.NowPlayerWeapon.nowBullet = Player.Instance.NowPlayerWeapon.MaxBullet;
            UIController.Instance.ShowNowBulletText();
            IsReload = false;
        }
        AttackSwitch = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AttackEnd = true;
        check = false;
        mCycle = null;
        if (Player.Instance.NowPlayerWeapon != null)
        {
            Stick.rectTransform.anchoredPosition = Vector3.zero;
            if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Fire)
            {
                Player.Instance.NowPlayerWeapon.mAttackArea.FireStarter.Stop();
                SoundController.Instance.mBGSE.Stop();
            }
        }
    }
    public void OnBeginDrag(PointerEventData ped)
    {
        if (Player.Instance.NowPlayerWeapon != null && AttackSwitch == false)
        {
            OnDrag(ped);
            check = true;
            mCycle = StartCoroutine(AttackCycle());
        }
    }

    public void WeaponChangeReset()
    {
        AttackEnd = true;
        check = false;
        mCycle = null;
        StopCoroutine(CooltimeRoutine(CoolMaxtime));
        StartCoroutine(CooltimeRoutine(CoolMaxtime));
    }
}
