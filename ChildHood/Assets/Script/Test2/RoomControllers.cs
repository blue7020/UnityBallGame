using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;

    public int X;
    public int Y;
    public bool IsFound;
    public int EnemyCount;
}

public class RoomControllers : MonoBehaviour
{
    
    public static RoomControllers Instance;

    string CurrentWorldName = "Basement";

    RoomInfo CurrentLoadRoomData;

    Room CurrentRoom;
    public int RoomLength = 0;

    public Queue<RoomInfo> LoadRoomQueue = new Queue<RoomInfo>();

    public List<Room> LoadedRooms = new List<Room>();


    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;
    private List<string> roomNameList;

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
    }

    public void LoadRoom(string name, int x,int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;
        newRoomData.IsFound = false;
        newRoomData.EnemyCount = 0;

        RoomLength++;
        LoadRoomQueue.Enqueue(newRoomData);
    }

    private void Update()
    {
        UpdateRoomQueue();
        
    }

    private void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (LoadRoomQueue.Count ==0)
        {

            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnEndRoom());
            }
            else if (spawnedBossRoom &&!updatedRooms)
            {
                foreach (Room room in LoadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        CurrentLoadRoomData = LoadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRouine(CurrentLoadRoomData));
    }

    private IEnumerator SpawnEndRoom()
    {
        WaitForSeconds point = new WaitForSeconds(0.5f);
        spawnedBossRoom = true;
        yield return point;
        if (LoadRoomQueue.Count==0)
        {
            Room bossRoom = LoadedRooms[LoadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = LoadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            LoadedRooms.Remove(roomToRemove);
            LoadRoom("End",tempRoom.X,tempRoom.Y);
            //나머지 특수한 방 생성
            bool Shop = false;
            for (int i=0; i<LoadRoomQueue.Count-1;i++)
            {
                Debug.Log(i);
                if (LoadedRooms[i].name == "BasementEmpty" && Shop == false)
                {
                    Room ShopRoom = LoadedRooms[i];
                    Room tempRoom2 = new Room(bossRoom.X, bossRoom.Y);
                    Destroy(ShopRoom.gameObject);
                    var roomToRemove2 = LoadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
                    LoadedRooms.Remove(roomToRemove2);
                    LoadRoom("Shop", tempRoom.X, tempRoom.Y);
                    Shop = true;
                }
                
            }
        }
    }


    private IEnumerator LoadRoomRouine(RoomInfo info)
    {
        string roomName = CurrentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (loadRoom.isDone==false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(CurrentLoadRoomData.X,CurrentLoadRoomData.Y))
        {
            room.transform.position = new Vector2(CurrentLoadRoomData.X * room.Width, CurrentLoadRoomData.Y * room.Height);

            room.X = CurrentLoadRoomData.X;
            room.Y = CurrentLoadRoomData.Y;
            room.name = CurrentWorldName + "_" + CurrentLoadRoomData.name + " " + room.X + ", " + room.Y;
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

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
        {
            "Statue",
            "Enemy",
            //"Chest",
            //"Shop",
            "Empty"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlaerEnterRoom(Room room)
    {
        //플레이어가 해당 방에 들어왔을 때.
        CurrentRoom = room;
        Player.Instance.CurrentRoom = CurrentRoom;
        Player.Instance.NowEnemyCount = CurrentRoom.EnemyCount;
        if (CurrentRoom.IsFound == false)
        {
            CurrentRoom.IsFound = true;
        }
    }
}
