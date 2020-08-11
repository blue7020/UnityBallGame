using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;
    public int Height;

    public int X;
    public int Y;

    private bool updatedDoors = false;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public eRoomType eType;
    public GridController mGrid;
    public EnemyFinder mEnemyFinder;

    public List<Door> doors = new List<Door>();

    public bool IsFound;
    public bool Special;
    public int EnemyCount;
    public GameObject blackOut;


    private void Awake()
    {
        EnemyCount = 0;
        Special = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameController.Instance.Level < 5)
        {
            Door[] ds = GetComponentsInChildren<Door>();
            foreach (Door d in ds)
            {
                doors.Add(d);
                switch (d.doorType)
                {
                    case DoorType.top:
                        topDoor = d;
                        break;
                    case DoorType.bot:
                        bottomDoor = d;
                        break;
                    case DoorType.right:
                        rightDoor = d;
                        break;
                    case DoorType.left:
                        leftDoor = d;
                        break;
                    default:
                        Debug.LogError("Wrong DoorType");
                        break;
                }
            }

            RoomControllers.Instance.RegisterRoom(this);

        }
    }

    private void Update()
    {
        if (RoomControllers.Instance.AllRoomGen == true)
        {
            if (name.Contains("End") && !updatedDoors)
            {
                RemoveUnconnectedDoors();
                updatedDoors = true;
            }
            if (name.Contains("Shop") && !updatedDoors)
            {
                RemoveUnconnectedDoors();
                updatedDoors = true;
            }
            if (name.Contains("Statue") && !updatedDoors)
            {
                RemoveUnconnectedDoors();
                updatedDoors = true;
            }
            if (name.Contains("Chest") && !updatedDoors)
            {
                RemoveUnconnectedDoors();
                updatedDoors = true;
            }
        }
    }


    public void RoomBlackOut()
    {
        blackOut.gameObject.SetActive(false);
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case DoorType.top:
                    if (GetTop() != null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorType.bot:
                    if (GetBottom() != null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorType.right:
                    if (GetRight() != null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorType.left:
                    if (GetLeft() != null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                default:
                    Debug.LogError("Wrong DoorType");
                    break;
            }
        }
    }

    public Room GetRight()
    {
        if (RoomControllers.Instance.DoesRoomExist(X + 1, Y))
        {
            return RoomControllers.Instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetLeft()
    {

        if (RoomControllers.Instance.DoesRoomExist(X - 1, Y))
        {
            return RoomControllers.Instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomControllers.Instance.DoesRoomExist(X, Y + 1))
        {
            return RoomControllers.Instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if (RoomControllers.Instance.DoesRoomExist(X, Y - 1))
        {
            return RoomControllers.Instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& Special==false)
        {
            if (eType==eRoomType.Shop)
            {
                for (int i=0; i < CanvasFinder.Instance.mShopPriceText.Length;i++)
                {
                    CanvasFinder.Instance.mShopPriceText[i].gameObject.SetActive(true);
                }
                
            }
            if (eType == eRoomType.Statue)
            {
                for (int i = 0; i < CanvasFinder.Instance.mStatuePriceText.Length; i++)
                {
                    if (i!=1)
                    {
                        CanvasFinder.Instance.mStatuePriceText[i].gameObject.SetActive(true);
                    }
                }

            }
            if (eType == eRoomType.Boss)
            {
                PortalTrigger.Instance.BossSpawn();
                EnemyCount++;

            }
            if (eType == eRoomType.Slot)
            {
                CanvasFinder.Instance.mSlotPriceText.gameObject.SetActive(true);

            }
            Special = true;
            RoomControllers.Instance.OnPlayerEnterRoom(this);
        }
    }
}
