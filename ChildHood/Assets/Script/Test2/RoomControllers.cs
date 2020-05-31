using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    Queue<RoomInfo> LoadRoomQueue = new Queue<RoomInfo>();

    public List<Room> LoadedRooms = new List<Room>();

    bool isLoadingRoom = false;

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
        //if (DoesRoomExist(x, y))
        //{
        //    return;
        //}
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        LoadRoomQueue.Enqueue(newRoomData);
    }

    private void Start()
    {
        //bool Exit;//for j를 탈출
        //int rand = Random.Range(3,7);
        //Vector2[] MapCheck = new Vector2[rand];
        //Vector2[] NowMap = new Vector2[rand];
        LoadRoom("Start", 0, 0);

        LoadRoom("Empty1", 1, 0);
        LoadRoom("Empty1", -1, 0);
        LoadRoom("Empty2", 0, 1);
        LoadRoom("Empty2", 0, -1);

        //질문- 몬스터의 '추적 범위 콜라이더'와 플레이어의 공격 범위 콜라이더가 충돌할 때 몬스터가 데미지를 입지 않게 하려면 어떻게 하는지.
        //질문- 모든 방이 연결되게 랜덤 배치가 가능한 방법, 방이 3개 이상일 때 3번째 방부터 오브젝트들에 스크립트가 빠져있는 것에 대해


        //for(int i=0; i < rand; i++)
        //{
        //    Exit = false;
        //    int randX = Random.Range(-3, 4);
        //    int randY = Random.Range(-3, 4);
        //    NowMap[i] = new Vector2(randX, randY);
        //    MapCheck[i] = NowMap[i];
        //    if (Exit == true)
        //    {
        //        continue;
        //    }
        //    //현재 생성한 랜드 x y값을 배열에 저장, 만약 중복 위치가 할당되면 continue
        //    for (int j=0; j<i;j++)
        //    {
        //        if (NowMap[i]==MapCheck[j])
        //        {
        //            Exit = true;
        //        }
        //        else
        //        {
        //            LoadRoom("Empty",randX,randY);
        //        }

        //        if (Exit==true)
        //        {
        //            break;
        //        }
        //    }
        //}
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
            return;
        }

        CurrentLoadRoomData = LoadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRouine(CurrentLoadRoomData));
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
        room.transform.position = new Vector2(CurrentLoadRoomData.X * room.Width, CurrentLoadRoomData.Y * room.Height);

        room.X = CurrentLoadRoomData.X;
        room.Y = CurrentLoadRoomData.Y;
        room.name = CurrentWorldName + "_" + CurrentLoadRoomData.name + " " + room.X + ", " + room.Y;
        room.transform.parent = transform;

        isLoadingRoom = false;

        LoadedRooms.Add(room);
    }

    //public bool DoesRoomExist(int x, int y)
    //{
    //    return LoadedRooms.Find(item => item.X == x && item.Y == y) != null;
    //}
}
