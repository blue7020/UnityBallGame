using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;//아이템 내려오는 속도
    [SerializeField]
    private eItemType mType;//이 아이템이 어떤 아이템인지 할당해주기 위해서.
    private ItemController mController;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.back * mSpeed;
    }

    public void SetController(ItemController controller)
    {
        mController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mController.ItemFunction(mType);
            gameObject.SetActive(false);//오브젝트 비활성화
        }
    }
}
