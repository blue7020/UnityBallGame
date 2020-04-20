using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    Life, Homing, Multi
}

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private Player mplayer;

    public void ItemFunction(eItemType id)
    {
        switch (id)
        {
            //지금은 코드(기능)가 단순해서 괜찮지만, 조건이 복잡해지면 따로 메서드로 빼주는 게 좋다. == 유지보수가 쉽기 때문.
            case eItemType.Life:
                AddLife();
                break;
            case eItemType.Homing:
                SetHoming();
                break;
            case eItemType.Multi:
                AddMoreBolt();
                break;
        }
    }

    private void AddLife()
    {
        Debug.Log("Life");
    }

    private void SetHoming()
    {
        mplayer.StartHoming(3);//3초
    }

    private void AddMoreBolt()
    {
        mplayer.mCurrentBoltCount++;
    }
}
