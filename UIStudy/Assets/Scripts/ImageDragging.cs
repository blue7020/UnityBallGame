using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ImageDragging : MonoBehaviour,IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    //EventSystems는 IDragHandler 같은 인터페이스를 사용할 수 있고
    //Envents는 UnityEvent Obj.Invoke()를 사용할 수 있다.
    //[SerializeField]
    //private UnityEvent PointEnter;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 Pos = Camera.main.ScreenToWorldPoint(eventData.position);
        Pos.z = 0;

        transform.position = Pos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //PointEnter.Invoke();//UnityEvent에 등록된 메서드를 바로 실행시키라는 명령이며, 평소에 쓰는 Invoke와는 다르다
        Debug.Log("Mouse Enter " + eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit! " + eventData.position);
    }

}
