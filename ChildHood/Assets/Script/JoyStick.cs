using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private RectTransform MovementPad;
    [SerializeField]
    private RectTransform Stick;

    private bool IsTouch;
    private Vector3 movePos;

    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        IsTouch = false;
        radius = MovementPad.rect.width * 0.5f;
    }
    private void Update()
    {
        if (IsTouch)
        {

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)MovementPad.position;

        value = Vector2.ClampMagnitude(value, radius);
        Stick.localPosition = value;

        float distance = Vector2.Distance(MovementPad.position, Stick.position) /radius;
        value = value.normalized;
        movePos = new Vector3(value.x * Player.Instance.mInfoArr[Player.Instance.mID].Spd * Time.deltaTime, value.y *Player.Instance.mInfoArr[Player.Instance.mID].Spd * Time.deltaTime,0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsTouch = false;
        Stick.localPosition = Vector3.zero;
    }
}
