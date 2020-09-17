using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public static VirtualJoyStick Instance;

    public Image BG, Stick;
    public Vector2 inputVector;

    private void Awake()
    {
        if (GameSetting.Instance.Ingame==true)
        {
            PlayerList.Instance.stick = this;
        }
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

            inputVector = new Vector2(pos.x * 2 , pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ?inputVector.normalized : inputVector;

            //Move Joystick
            Stick.rectTransform.anchoredPosition
                = new Vector2(inputVector.x * (BG.rectTransform.sizeDelta.x / 2)/2,
                              inputVector.y * (BG.rectTransform.sizeDelta.y / 2)/2);

            if (Player.Instance!=null)
            {
                //플레이어 방향 벡터
                float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
                Player.Instance.mDirection.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                if (GameController.Instance.IsTutorial==false)
                {
                    UIController.Instance.mPlayerLookPoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }
                else
                {
                    TutorialUIController.Instance.mPlayerLookPoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        Stick.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x !=0)
        {
            return inputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vectical()
    {
        if (inputVector.y != 0)
        {
            return inputVector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
