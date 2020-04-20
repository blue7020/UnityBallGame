using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField]
    private Item[] mPrefab;
    private List<Item>[] mItem;
    [SerializeField]
    private ItemController mController;

    // Start is called before the first frame update
    void Awake()
    {
        mItem = new List<Item>[mPrefab.Length];
        for (int i=0; i<mItem.Length; i++)
        {
            mItem[i] = new List<Item>();
        }
    }

    public Item GetFromPool(int id = 0)
    {
        for (int i = 0; i < mItem[id].Count; i++)
        {
            if (!mItem[id][i].gameObject.activeInHierarchy)
            {
                mItem[id][i].gameObject.SetActive(true);
                return mItem[id][i];
            }
        }
        Item newObj = Instantiate(mPrefab[id]);
        mItem[id].Add(newObj);
        newObj.SetController(mController);
        return newObj;
    }
}
