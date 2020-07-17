using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectController : MonoBehaviour
{
    public static PlayerSelectController Instance;

    [SerializeField]
    private PlayerSelect[] mCharacterList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i=1; i<mCharacterList.Length;i++)//기본 캐릭터는 식빵이기에 id 0번은 처음에 활성화하지않는다.
        {
            if (GameSetting.Instance.CharacterOpen[i]==true)
            {
                mCharacterList[i].gameObject.SetActive(true);
            }
        }
    }

    public void CharaChange(int id)
    {
        mCharacterList[GameSetting.Instance.PlayerID].gameObject.SetActive(true);
        GameSetting.Instance.PlayerID = id;
        //TODO 잠시 화면이 암전됬다가 플레이어 캐릭터 교체 후 초기위치로 이동
    }
}
