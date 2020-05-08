using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : MonoBehaviour
{
    private List<UIElement> mElementList;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

    // Start is called before the first frame update
    void Start()
    {
        //save data load
        mElementList = new List<UIElement>();
        UIElement elem = Instantiate(mElementPrefab, mElementArea);
        elem.Init(0, null, "test1", "1", "power up", "2", LevelUP);
        mElementList.Add(elem);
        
    }

    public void LevelUP(int id,int amount)
    {
        GameController.Instance.GoldCallback = LevelUPCallback;
        GameController.Instance.Gold -= 2;

    }

    public void LevelUPCallback()
    {
        //골드가 충분히 있을때 작동
        GameController.Instance.TouchPower++;
        UIElement elem = Instantiate(mElementPrefab, mElementArea);
        elem.Init(0, null, "test1", "1", "power up", "2", LevelUP);
        mElementList.Add(elem);
    }
}
