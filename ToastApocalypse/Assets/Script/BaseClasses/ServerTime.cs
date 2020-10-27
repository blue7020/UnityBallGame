using System;
using System.Collections;
using System.Globalization;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using UnityEngine;

public class ServerTime : MonoBehaviour
{
    public static ServerTime Instance;

    //NTP 서버의 URL
    public string m_ServerUrl;
    //받아온 DateTime을 저장할 변수
    public DateTime mServerTime { get; set; } = DateTime.MinValue;
    //시간 업데이트를 알릴 Action 델리게이트
    public Action<DateTime> onTimeUpdated { get; set; }

    //쓰레드를 보존할 변수
    private Thread m_GetTimeTread;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        m_GetTimeTread = new Thread(() =>
          {
              //while (true)
              //{
              try
              {
                  //NTP 포트는 123번 이지만 13번 DAYTIME 포트 사용
                  TcpClient tcpClient = new TcpClient(m_ServerUrl, 13);
                  if (tcpClient.Connected)
                  {
                      StreamReader sr = new StreamReader(tcpClient.GetStream());
                      //형태 57486 16-04-08 08:53:18 50 0 0 737.0 UTC(NIST)
                      string readData = sr.ReadToEnd();
                      Debug.Log("TCP / 받아온 데이터: " + readData);

                      //형태 16-04-08 08:57:07
                      string _time = readData.Substring(readData.IndexOf(" ") + 1, 17);

                      //대한민국은 UTC 기준 +9시간
                      //ServerTime = Conver.ToDateTime(_time).AddHours(9);
                      mServerTime = DateTime.ParseExact(_time, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).AddHours(9);
                      Debug.Log("TCP / 서버 시간: " + mServerTime);
                      onTimeUpdated?.Invoke(mServerTime);
                  }
              }
              catch (Exception e)
              {
                  //만약 초기값과 다르다면 한번이라도 서버 시간을 받아왔다는 뜻이므로 초기값일 때만 로컬 시간을 대신함
                  if (mServerTime == DateTime.MinValue)
                  {
                      mServerTime = DateTime.Now;
                      Debug.LogWarning("TCP / 서버 시간을 가져오는 중 에러가 발생했습니다. 로컬 시간으로 대체합니다.: " + e);
                      //쓰레드 종료
                      Thread.CurrentThread.Join();
                  }
              }
              //시간 서버에서 4초 안에 2번 이상의 호출이 될 경우 차단될 수 있으니
              //10초간의 딜레이를 가진다
              //Thread.Sleep(10000);
              //}
        });
        m_GetTimeTread?.Start();
    }

    private void OnDisable()
    {
        //쓰레드 즉시 종료
        if (m_GetTimeTread !=null)
        {
            m_GetTimeTread?.Abort();
        }
    }
}
