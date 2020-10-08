using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public static Book Instance;
    public int DialogID;
    public Text mTitle,mTooltipTitle, mTooltip;
    public Image mWindow,mImage;
    public Button LeftButton, RightButton;

    public Sprite[] mSpt;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DialogID = 0;
            LeftButton.gameObject.SetActive(false);
            ShowTooltip();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LeftPageSelect()
    {
        DialogID -= 1;
        ShowTooltip();
        if (DialogID - 1 < 0)
        {
            LeftButton.gameObject.SetActive(false);
        }
        RightButton.gameObject.SetActive(true);
    }
    public void RightPageSelect()
    {
        DialogID += 1;
        ShowTooltip();
        if (DialogID + 1 >= 8)
        {
            RightButton.gameObject.SetActive(false);
        }
        LeftButton.gameObject.SetActive(true);
    }

    public void ShowTooltip()
    {
        mImage.sprite = mSpt[DialogID];
        if (GameSetting.Instance.Language == 0)
        {
            mTitle.text = "가이드북";
            switch (DialogID)
            {

                case 0:
                    mTooltipTitle.text = "<도움말>";
                    mTooltip.text = "페이지를 넘겨 도움말을 읽어보세요.";
                    break;
                case 1:
                    mTooltipTitle.text = "<유물>";
                    mTooltip.text = "<color=#1AF6FA>착용 유물</color>: 보유 시 능력치 증가\n<color=#FA4CB4>사용 유물</color>: 사용 시 일정 시간동안 효과를 받음\n\n던전 내의 상점, 상자 등에서 획득할 수 있습니다.";
                    break;
                case 2:
                    mTooltipTitle.text = "<아이템>";
                    mTooltip.text = "소모품입니다. 사용 시 일정 시간 동안 효과를 받습니다.\n\n던전 내의 상점에서 획득할 수 있습니다.";
                    break;
                case 3:
                    mTooltipTitle.text = "<무기>";
                    mTooltip.text = "<color=#FE2E2E>근접</color>: 범위 공격, 탄환 파괴 가능\n<color=#FE642E>중거리</color>: 범위 공격, 탄약 사용\n<color=#FFBF00>원거리</color>: 탄약 사용\n\n중거리와 원거리 무기는 탄약이 떨어지면\n재장전을 하며 잠시 동안 공격을 할 수 없습니다.\n[부엌]에서 무기를 교체할 수 있습니다.";
                    break;
                case 4:
                    mTooltipTitle.text = "<스킬>";
                    mTooltip.text = "사용 시 일정 시간 동안 효과를 받습니다.\n\n[마법사]를 통해 스킬을 교체할 수 있습니다.";
                    break;
                case 5:
                    mTooltipTitle.text = "<몬스터>";
                    mTooltip.text = "몬스터는 낮은 층으로 갈수록 강해지며, 층마다 일반 몬스터보다 강력한 보스 몬스터가 한 마리씩 존재합니다.\n\n몬스터는 죽으면 골드를 떨어뜨립니다.";
                    break;
                case 6:
                    mTooltipTitle.text = "<던전 상점>";
                    mTooltip.text = "던전의 매 층마다 상점이 있습니다.\n골드를 소모해 아이템과 유물들을 구입할 수 있습니다.";
                    break;
                case 7:
                    mTooltipTitle.text = "<슬롯 머신>";
                    mTooltip.text = "던전에서 발견할 수 있습니다.\n골드를 소모해 무기나 유물 중 하나를 확률적으로 획득할 수 있습니다.";
                    break;
            }
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTitle.text = "Guide book";
            switch (DialogID)
            {
                case 0:
                    mTooltipTitle.text = "<Help>";
                    mTooltip.text = "Turn the page and read the help.";
                    break;
                case 1:
                    mTooltipTitle.text = "<Artifact>";
                    mTooltip.text = "<color=#1AF6FA>Passive Artifact</color>: Increase status.\n<color=#FA4CB4>Active Artifact</color>: When use get effect for duration.\n\nIt can be acquire from shops, chests, etc. in the dungeon.";
                    break;
                case 2:
                    mTooltipTitle.text = "<Item>";
                    mTooltip.text = "Consumable, When use get effect for duration.\n\nCan buy it from a shop in the dungeon.";
                    break;
                case 3:
                    mTooltipTitle.text = "<Weapon>";
                    mTooltip.text = "<color=#FE2E2E>Short Range</color>: Wide attack, Enemy's bullet destruction\n<color=#FE642E>Middle Range</color>: Wide attack, Use ammo\n<color=#FFBF00>Long Range</color>: Long Range, Use ammo\n\nLong-range and middle-range weapons are reloaded when run out of ammo.\nbut can't attack while reloading.\nCan change weapons in the [kitchen].";
                    break;
                case 4:
                    mTooltipTitle.text = "<Skill>";
                    mTooltip.text = "When use get effect for duration.\n\n[The wizard] will allow you to replace your skills.";
                    break;
                case 5:
                    mTooltipTitle.text = "<Monster>";
                    mTooltip.text = "Monsters get stronger on the lower floors, and there's one boss monster on each floor that's more powerful than regular monsters.\nMonsters drop gold when they die.";
                    break;
                case 6:
                    mTooltipTitle.text = "<Dungeon Shop>";
                    mTooltip.text = "Shops on every floor in the dungeon.\nCan buy items and artifacts by gold.";
                    break;
                case 7:
                    mTooltipTitle.text = "<SlotMachine>";
                    mTooltip.text = "Can find it in the dungeon. Use the gold to acquire either random weapons or artifacts.";
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogID = 0;
            LeftButton.gameObject.SetActive(false);
            ShowTooltip();
            mWindow.gameObject.SetActive(true);
        }
    }
}
