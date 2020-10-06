using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyNPC : MonoBehaviour
{
    public Image mDialogWindow;
    public Text mText;
    public int mID;

    private void Start()
    {
        if (GameSetting.Instance.Language == 0)
        {
            switch (mID)
            {
                case 6:
                    mText.text = "여기까지 무사히 와서 다행이야.\n하지만 아직도 던전 안에\n많은 사람들이 잡혀있겠지...?";
                    break;
                case 7:
                    mText.text = "책들은 마녀가 왜 봉인되었는지를 알려주질 않아.\n그냥 사악하다고만 적혀있던데...\n무슨 일이 있었던걸까?";
                    break;
                case 8:
                    mText.text = "저주 때문에 우리도 유통기한이 언제\n생길지 몰라. 항상 조심하게.";
                    break;
            }
        }
        else if (GameSetting.Instance.Language == 1)
        {
            switch (mID)
            {
                case 6:
                    mText.text = "I'm glad we got here safely.\nBut there's still a lot of people\nin the dungeon...";
                    break;
                case 7:
                    mText.text = "The books don't say why the witch was sealed.\nIt just says the witch is evil...\nWhat happened?";
                    break;
                case 8:
                    mText.text = "We don't know when the expiration date\nwill be due to the curse.\nAlways be careful.";
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mDialogWindow.gameObject.SetActive(true);
        }
    }
}
