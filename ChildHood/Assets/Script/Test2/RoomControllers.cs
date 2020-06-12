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
    

    //TODO 포탈 진입 시 스테이지 재시작, Level이 5라면 보스방 진입
    public void RestartRoom()
    {
        if (Player.Instance.Level > 4)
        { 
            Debug.Log("room보스방 진입");
            SceneManager.LoadScene(7);
            Player.Instance.transform.position = new Vector2(0, -10.5f);
        }
        else
        {
            SceneManager.LoadScene(6);
            Debug.Log("방 재시작, 현재 지하 " + Player.Instance.Level + "층");//4층까지 존재 
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
                StartCoroutine(SpawnBossRoom());
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

    private IEnumerator SpawnBossRoom()
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
            //"Empty",
            "Empty1",
            "Empty2"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlaerEnterRoom(Room room)
    {
        //플레이어가 해당 방에 들어왔을 때.
        CurrentRoom = room;

    }
}
