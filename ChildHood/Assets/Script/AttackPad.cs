using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static AttackPad Instance;

    [SerializeField]
    private Image BG, Stick;
    public Vector2 inputVector;

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
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BG.rectTransform,
                                                                    eventData.position,
                                                                    eventData.pressEventCamera,
                                                                    out pos))
        {
            pos.x = (pos.x / BG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / BG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2 + 1, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Joystick
            Stick.rectTransform.anchoredPosition
                = new Vector2(inputVector.x * (BG.rectTransform.sizeDelta.x / 2),
                              inputVector.y * (BG.rectTransform.sizeDelta.y / 2));

        }

        //무기 방향 돌리기
        float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
        Weapon.instance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Weapon.instance.Attack();
    }



    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        Weapon.instance.transform.rotation = Quaternion.identity;
        inputVector = Vector3.zero;
        Stick.rectTransform.anchoredPosition = Vector3.zero;
    }

}
