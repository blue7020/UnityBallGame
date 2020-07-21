using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataController : CSVLoader
{
    public static ItemDataController Instance;
    private ItemData[] mInfoArr;
    private Sprite[] mSpriteArr;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            LoadCSV(out mInfoArr, "CSVFiles/ItemTable");
            mSpriteArr = Resources.LoadAll<Sprite>("Item");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ItemData GetItemData(int id)
    {
        return mInfoArr[id].GetClone();
    }

    public Sprite GetItemSprite(int id)
    {
        return mSpriteArr[id];
    }
}
