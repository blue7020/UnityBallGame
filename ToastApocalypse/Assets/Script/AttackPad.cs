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
    private bool AttackSwitch,IsReload,AttackEnd;
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
        if (Player.Instance.NowPlayerWeapon!=null)
        {
            AttackEnd = false;
            mCycle=StartCoroutine(AttackCycle());
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

        }
    }

    public IEnumerator AttackCycle(bool check=true)
    {
        WaitForSeconds time = new WaitForSeconds(0.1f);
        float currentTime = 0;
        Attack();
        while (check)
        {
            float Maxtime = Player.Instance.mStats.AtkSpd;
            if (AttackEnd==false)
            {
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
    }

    private void Attack()
    {
        if (Player.Instance.NowPlayerWeapon!=null&&AttackCurrentTime <= 0)
        {
            if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Melee || Player.Instance.NowPlayerWeapon.nowBullet >= 1)
            {
                CoolMaxtime = Player.Instance.mStats.AtkSpd;
            }
            AttackSwitch = true;
            StartCoroutine(CooltimeRoutine(CoolMaxtime));

            if (AttackSwitch == true && Player.Instance.Stun == false)
            {
                if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Melee)
                {
                    Player.Instance.NowPlayerWeapon.MeleeAttack();
                    StartCoroutine(AttackCooltime());
                }
                if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Range)
                {
                    if (Player.Instance.NowPlayerWeapon.nowBullet > 0)
                    {
                        Player.Instance.NowPlayerWeapon.RangeAttack();
                        StartCoroutine(AttackCooltime());
                    }
                    else
                    {
                        AttackSwitch = false;
                        float reloadCool = Player.Instance.NowPlayerWeapon.mStats.ReloadCool;
                        CoolMaxtime = reloadCool * (1 - PassiveArtifacts.Instance.ReloadCooltimeReduce);
                        IsReload = true;
                        StartCoroutine(CooltimeRoutine(CoolMaxtime));
                        Player.Instance.NowPlayerWeapon.nowBullet = Player.Instance.NowPlayerWeapon.MaxBullet;
                    }
                }
                if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Fire)
                {
                    if (Player.Instance.NowPlayerWeapon.nowBullet > 0)
                    {
                        Player.Instance.NowPlayerWeapon.FireAttack();
                    }
                    else
                    {
                        Player.Instance.NowPlayerWeapon.mAttackArea.FireStarter.Stop();
                        AttackSwitch = false;
                        float reloadCool = Player.Instance.NowPlayerWeapon.mStats.ReloadCool;
                        IsReload = true;
                        CoolMaxtime = reloadCool * (1 - PassiveArtifacts.Instance.ReloadCooltimeReduce);
                        StartCoroutine(CooltimeRoutine(CoolMaxtime));
                        Player.Instance.NowPlayerWeapon.nowBullet = Player.Instance.NowPlayerWeapon.MaxBullet;
                        UIController.Instance.ShowNowBulletText();
                    }
                }
            }
        }
    }

    //public virtual void OnPointerDown(PointerEventData ped)
    //{
    //    if (Player.Instance.NowPlayerWeapon != null && AttackSwitch == false)
    //    {
    //        if (Player.Instance.NowPlayerWeapon.Attackon == false)
    //        {
    //            OnDrag(ped);
    //        }
    //    }

    //}

    //public virtual void OnPointerUp(PointerEventData ped)
    //{
    //    StopCoroutine(AttackCycle());
    //    mCycle = null;
    //    if (Player.Instance.NowPlayerWeapon != null)
    //    {
    //        Stick.rectTransform.anchoredPosition = Vector3.zero;
    //        if (Player.Instance.NowPlayerWeapon.eType == eWeaponType.Fire)
    //        {
    //            Player.Instance.NowPlayerWeapon.mAttackArea.FireStarter.Stop();
    //            SoundController.Instance.mBGSE.Stop();
    //        }
    //    }
    //}

    private IEnumerator AttackCooltime()
    {
        WaitForSeconds Cool = new WaitForSeconds(Player.Instance.mStats.AtkSpd/ (1+ Player.Instance.AttackSpeedStat +Player.Instance.buffIncrease[2]));
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
        if (IsReload == true)
        {
            SoundController.Instance.SESound(7);
            IsReload = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AttackEnd = true;
        StopCoroutine(AttackCycle());
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
            if (Player.Instance.NowPlayerWeapon.Attackon == false)
            {
                OnDrag(ped);
            }
        }
    }

    public void WeaponChangeReset()
    {
        mCycle=StartCoroutine(AttackCycle(false));
        StopCoroutine(AttackCycle(false));
        mCycle = null;
        StopCoroutine(CooltimeRoutine(CoolMaxtime));
        StopCoroutine(AttackCooltime());

        StartCoroutine(CooltimeRoutine(CoolMaxtime));
        StartCoroutine(AttackCooltime());
        //CoolWheel.gameObject.SetActive(false);
    }
}
