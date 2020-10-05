using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public int num;

    public int X;
    public int Y;
    public bool IsFound;
    public int EnemyCount;
}

public class RoomControllers : MonoBehaviour
{

    public static RoomControllers Instance;

    RoomInfo CurrentLoadRoomData;
    public int RoomLength;

    public Queue<RoomInfo> LoadRoomQueue;

    public List<Room> LoadedRooms;

    public Room[] rooms;
    public bool RoomSetting;

    bool spawnedEndRoom = false;
    bool spawnedShopRoom = false;
    bool spawnedSpacialRoom = false;
    bool spawnedChestRoom = false;
    public bool AllRoomGen = false;

    bool isLoadingRoom = false;
    bool updatedRooms = false;
    bool firstSetting = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            rooms = GameSetting.Instance.NowStageRoom;
            LoadedRooms = new List<Room>();
            LoadRoomQueue = new Queue<RoomInfo>();
            RoomLength = 0;
            RoomSetting = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (RoomSetting == true)
        {
            UpdateRoomQueue();
        }

    }

    public void Seeker()
    {
        if (Player.Instance.MapSeeker==true)
        {
            for (int i = 1; i < LoadedRooms.Count; i++)
            {
                LoadedRooms[i].MapIcon.gameObject.SetActive(true);
            }
        }
    }

    public void LoadRoom(int id, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.num = id;
        newRoomData.X = x;
        newRoomData.Y = y;
        newRoomData.IsFound = false;
        newRoomData.EnemyCount = 0;

        RoomLength++;
        LoadRoomQueue.Enqueue(newRoomData);
    }

    private void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (LoadRoomQueue.Count == 0)
        {
            if (firstSetting == false)
            {
                StartCoroutine(SpawnEndRoom());
            }
            if (firstSetting == true&&AllRoomGen==true)
            {
                RoomSetting = false;
                foreach (Room room in LoadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                Seeker();
            }
            return;
        }
        CurrentLoadRoomData = LoadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRouine(CurrentLoadRoomData));
    }

    private IEnumerator SpawnEndRoom()
    {
        WaitForSeconds point = new WaitForSeconds(1f);
        firstSetting = true;
        yield return point;
        if (LoadRoomQueue.Count == 0)
        {
            List<int> EnemyRoom = new List<int>();
            for (int i = 0; i < LoadedRooms.Count-1; i++)
            {
                EnemyRoom.Add(i);

            }
            for (int i = 0; i < 4; i++)
            {
                int Count = Random.Range(0,EnemyRoom.Count);
                switch (i)
                {
                    case 0:
                        //ShopRoom
                        int rand0 = Random.Range(1, Count);
                        Room ShopRoom = LoadedRooms[rand0];
                        Room tempRoom1 = new Room(ShopRoom.X, ShopRoom.Y);
                        Destroy(ShopRoom.gameObject);
                        var roomToRemove1 = LoadedRooms.Single(r => r.X == tempRoom1.X && r.Y == tempRoom1.Y);
                        LoadedRooms.Remove(roomToRemove1);
                        LoadRoom(1, tempRoom1.X, tempRoom1.Y);
                        spawnedShopRoom = true;
                        RoomLength--;
                        break;
                    case 1:
                        float rand = Random.Range(0, 1f);
                        if (rand<0.5f)
                        {
                            //StatueRoom
                            int rand1 = Random.Range(1, Count);
                            Room StatueRoom = LoadedRooms[rand1];
                            Room tempRoom3 = new Room(StatueRoom.X, StatueRoom.Y);
                            Destroy(StatueRoom.gameObject);
                            var roomToRemove3 = LoadedRooms.Single(r => r.X == tempRoom3.X && r.Y == tempRoom3.Y);
                            LoadedRooms.Remove(roomToRemove3);
                            LoadRoom(2, tempRoom3.X, tempRoom3.Y);
                        }
                        else if(rand>=0.5f)
                        {
                            //SlotRoom
                            int rand1 = Random.Range(1, Count);
                            Room SlotRoom = LoadedRooms[rand1];
                            Room tempRoom3 = new Room(SlotRoom.X, SlotRoom.Y);
                            Destroy(SlotRoom.gameObject);
                            var roomToRemove3 = LoadedRooms.Single(r => r.X == tempRoom3.X && r.Y == tempRoom3.Y);
                            LoadedRooms.Remove(roomToRemove3);
                            LoadRoom(6, tempRoom3.X, tempRoom3.Y);
                        }
                        spawnedSpacialRoom = true;
                        RoomLength--;
                        break;
                    case 2:
                        //ChestRoom
                        int rand2 = Random.Range(1, Count);
                        Room ChestRoom = LoadedRooms[rand2];
                        Room tempRoom4 = new Room(ChestRoom.X, ChestRoom.Y);
                        Destroy(ChestRoom.gameObject);
                        var roomToRemove4 = LoadedRooms.Single(r => r.X == tempRoom4.X && r.Y == tempRoom4.Y);
                        LoadedRooms.Remove(roomToRemove4);
                        LoadRoom(3, tempRoom4.X, tempRoom4.Y);
                        spawnedChestRoom = true;
                        RoomLength--;
                        break;
                    case 3:
                        //Endroom
                        Room BossRoom = LoadedRooms[LoadedRooms.Count-1];
                        Room tempRoom2 = new Room(BossRoom.X, BossRoom.Y);
                        Destroy(BossRoom.gameObject);
                        var roomToRemove2 = LoadedRooms.Single(r => r.X == tempRoom2.X && r.Y == tempRoom2.Y);
                        LoadedRooms.Remove(roomToRemove2);
                        LoadRoom(5, tempRoom2.X, tempRoom2.Y);
                        spawnedEndRoom = true;
                        RoomLength--;
                        break;

                    default:
                        Debug.LogError("Wrong Index");
                        break;
                }
                EnemyRoom.Remove(Count);
                Count--;
            }
            AllRoomGen = true;
        }
    }


    private IEnumerator LoadRoomRouine(RoomInfo info)
    {
        int roomnum = info.num;
        Instantiate(rooms[roomnum]);
        yield return null;
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(CurrentLoadRoomData.X, CurrentLoadRoomData.Y))
        {
            room.transform.position = new Vector2(CurrentLoadRoomData.X * room.Width, CurrentLoadRoomData.Y * room.Height);

            room.X = CurrentLoadRoomData.X;
            room.Y = CurrentLoadRoomData.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;


            LoadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }

    }

    public bool DoesRoomExist(int x, int y)
    {
        return LoadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return LoadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public int GetRandomRoomName()
    {
        int[] possibleRooms = new int[]
        {
            4
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        Room CurrentRoom =room;
        Player.Instance.CurrentRoom = CurrentRoom;
        if (GameController.Instance.StageLevel < GameSetting.STAGELEVEL_COUNT)
        {
            if (CurrentRoom.IsFound == false)
            {
                if (CurrentRoom.eType != eRoomType.Normal || CurrentRoom.eType != eRoomType.StageEnd)
                {
                    StartCoroutine(MonsterSpawn(CurrentRoom));
                }
                CurrentRoom.RoomBlackOut();
                CurrentRoom.IsFound = true;
            }
        }
        
    }

    public IEnumerator MonsterSpawn(Room currentRoom)
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        yield return delay;
        if (currentRoom.eType == eRoomType.Monster)
        {
            currentRoom.mGrid.GenerateGrid();
        }
        
    }
}
