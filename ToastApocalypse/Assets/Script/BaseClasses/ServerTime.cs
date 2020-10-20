using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;
using UnityEngine;

public class ServerTime : MonoBehaviour
{
    public static ServerTime Instance;

    //NTP 서버의 url
    public string mServerUrl;

    public int yyyy, mm, dd;
    public DateTime mExpireDateTime, mNowLocalDateTime;
    public static DateTime NOW_SERVER_DATETIME;
    private TimeSpan mDuration;

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

    // Start is called before the first frame update
    void Start()
    {
        mExpireDateTime = new DateTime(Mathf.Clamp(yyyy, 1900, 3000), Mathf.Clamp(mm, 1, 12), Mathf.Clamp(dd, 1, 31));
        mNowLocalDateTime = DateTime.Now;
        NOW_SERVER_DATETIME = GetNISTDate().Add(mDuration);
        Debug.Log(NOW_SERVER_DATETIME);
        if (Debug.isDebugBuild)
        {
            Debug.Log("만료지정일: " + mExpireDateTime);
            Debug.Log("현재 로컬 시각: " + mNowLocalDateTime);
            Debug.Log("현재 서버 시각: " + NOW_SERVER_DATETIME);
        }

        if (mNowLocalDateTime < mExpireDateTime)
        {
            if (NOW_SERVER_DATETIME < mExpireDateTime)
            {
                Debug.Log("실행");
            }
            else
            {
                Debug.Log("서버 체크 결과 만료됨");
            }
        }
        else
        {
            Debug.Log("로컬 체크 결과 만료됨");
        }
    }

    #region NTPTIME

    //NTP time을 NIST에서 가져오기
    //4초 이내에 한번 이상 요청하면, ip가 차단됨

    public static DateTime GetDummyDate()
    {
        return new DateTime(2020, 10, 20);
        //to check if we have an online date or not.
    }

    public static DateTime GetNISTDate()
    {
        System.Random random = new System.Random(DateTime.Now.Millisecond);
        DateTime date = GetDummyDate();
        string serverResponse = string.Empty;

        //NIST 서버 목록
        string[] servers = new string[] {

            "time-a.nist.gov"//,
    };

        //너무 많은 요청으로 인한 차단을 피하기 위해 한 서버씩 순환한다. 5번만 시도한다.

        for (int i = 0; i < 5; i++) {
            try
            {
                //StreamREader(무작위 서버)
                StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(servers[random.Next(0, servers.Length)], 13).GetStream());
                serverResponse = reader.ReadToEnd();
                reader.Close();

                //서버 리스폰스를 표시한다. (디버그 확인용)
                if (Debug.isDebugBuild)
                {
                    Debug.Log(serverResponse);
                }
                //시그니처가 있는지 확인한다.
                if (serverResponse.Length > 47 && serverResponse.Substring(38, 9).Equals("UTC(NIST)"))
                {
                    //날짜 파싱
                    int jd = int.Parse(serverResponse.Substring(1, 5));
                    int yr = int.Parse(serverResponse.Substring(7, 2));
                    int mo = int.Parse(serverResponse.Substring(10, 2));
                    int dy = int.Parse(serverResponse.Substring(13, 2));
                    int hr = int.Parse(serverResponse.Substring(16, 2));
                    int mm = int.Parse(serverResponse.Substring(19, 2));
                    int sc = int.Parse(serverResponse.Substring(22, 2));

                    if (jd > 51544)
                    {
                        yr += 2000;
                    }
                    else
                    {
                        yr += 1999;
                    }
                    date = new DateTime(yr, mo, dy, hr, mm, sc);

                    //Exit the loop
                    break;
                }
            }
            catch (Exception e)
            {
                //아무것도 하지 않고 다음 서버를 시도한다.
            }
        }
        return date;
    }
    #endregion
}
