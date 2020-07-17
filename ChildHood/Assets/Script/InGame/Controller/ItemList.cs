using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : InformationLoader
{
    public static ItemList Instance;

    [SerializeField]
    public ItemStat[] mInfoArr;

#pragma warning disable 0649
    [SerializeField]
    private List<GameObject> ItemCommon;
    [SerializeField]
    private List<GameObject> ItemRare;
    [SerializeField]
    private List<GameObject> ItemEpic;
#pragma warning restore 0649

    public ItemStat[] GetInfoArr()
    {
        return mInfoArr;
    }


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
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        LoadJson(out mInfoArr, Path.ITEM_STAT);
    }

    public void ItemSpawn(eChestType Type)//,Item 아이템
    {
        int rand;
        switch (Type)
        {
            case eChestType.Wood:
                rand = Random.Range(0, ItemCommon.Count);
                //rand 번째에 해당하는 아이템 값을 넘겨주면된다.
                break;
            case eChestType.Silver:
                rand = Random.Range(0, ItemRare.Count);
                break;
            case eChestType.Gold:
                rand = Random.Range(0, ItemEpic.Count);
                break;
            default:
                Debug.LogError("Wrong ChestType");
                break;
        }
        //아이템을 chest에 넘겨주고 플레이어가 현재 소유한 유물은 아이템 리스트에서 제외해주면된다.
    }
}
