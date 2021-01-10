using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IslandSelect : MonoBehaviour,IPointerClickHandler
{
    public int mID;
    public Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
    }

    public IEnumerator SelectDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        StageSelectController.Instance.isSelectDelay = true;
        StageSelectController.Instance.ShowStageSelectUI(mID);
        yield return delay;
        StageSelectController.Instance.isSelectDelay = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!StageSelectController.Instance.isSelectDelay)
        {
            StartCoroutine(SelectDelay());
        }
    }
}
