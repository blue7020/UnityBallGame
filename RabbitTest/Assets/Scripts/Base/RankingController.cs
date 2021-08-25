using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayNANOO;

public class RankingController : MonoBehaviour
{

    public static RankingController Instance;

    Plugin plugin;
    public string UserID;

    void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        plugin = Plugin.GetInstance();
    }

    public void ShowAllRank()
    {
        plugin.RankingRange("beyondthesky-RANK-2222BFF2-DF28038B", 0, 99, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> item in (ArrayList)values["items"])
                {
                    Debug.Log(item["ranking"]);
                    Debug.Log(item["uuid"]);
                    Debug.Log(item["nickname"]);
                    Debug.Log(item["score"]);
                    Debug.Log(item["data"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void ShowPlayerRank()
    {
        plugin.RankingPersonal("beyondthesky-RANK-2222BFF2-DF28038B", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["ranking"]);
                Debug.Log(dictionary["player_data"]);
                Debug.Log(dictionary["total_player"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RecodeRank()
    {
        // 랭킹 기록
        GetPlugin().RankingRecord("beyondthesky-RANK-2222BFF2-DF28038B", SaveDataController.Instance.mUser.HighScore, "BestScore", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("랭킹 기록 완료");
            }
            else
            {
                Debug.Log("랭킹 기록 실패");
            }
        });
        string BestScore = SaveDataController.Instance.mUser.HighScore.ToString();
        GetPlugin().StorageSave("BestScore", BestScore, false, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
                Debug.Log("서버에 저장 완료");
            else
                Debug.Log("서버에 저장 실패");

        });
    }

    public Plugin GetPlugin()
    {
        // playnanoo plugin
        Plugin plugin = Plugin.GetInstance();
        if (UserID == "")
        {
            Debug.Assert(false);
        }
        plugin.SetUUID(UserID);
        plugin.SetNickname(UserID);
        plugin.SetLanguage("Configure.PN_LANG_KO");
        //        plugin.SetLanguage("Configure.PN_LANG_EN");
        return plugin;
    }

    public void UpdateRecode()
    {
        // 랭킹
        if (MainController.Instance.mRankingText != null)
        {
            GetPlugin().RankingPersonal("beyondthesky-RANK-2222BFF2-DF28038B", (state, message, rawData, dictionary) => {
                if (state.Equals(Configure.PN_API_STATE_SUCCESS))
                {
                    Debug.Log("랭킹 읽기 완료");
                    string ranking = dictionary["ranking"].ToString();
                    if (ranking == "-1")
                    {
                        if (SaveDataController.Instance.mUser.Language == 1)
                        {
                            MainController.Instance.mRankingText.text = "순위: 기록 없음";
                        }
                        else
                        {
                            MainController.Instance.mRankingText.text = "Ranking: Unknown";
                        }
                    }
                    else
                    {
                        if (SaveDataController.Instance.mUser.Language == 1)
                        {
                            MainController.Instance.mRankingText.text = "순위: " + dictionary["ranking"].ToString() + "명 중 " + dictionary["total_player"]+"위";
                        }
                        else
                        {
                            MainController.Instance.mRankingText.text = "Ranking: " + dictionary["ranking"].ToString() + " of " + dictionary["total_player"];
                        }
                    }

                }
                else
                {
                    Debug.Log("랭킹 읽기 실패");
                    //PlayNANOOErrorControl.ShowErrorMessage(message);
                    if (SaveDataController.Instance.mUser.Language == 1)
                    {
                        MainController.Instance.mRankingText.text = "순위: 기록 없음";
                    }
                    else
                    {
                        MainController.Instance.mRankingText.text = "Ranking: Unknown";
                    }
                }
            });
        }

        // 최고기록
        if (MainController.Instance.mBestRankingText != null)
        {
            if (SaveDataController.Instance.mUser.Language == 1)
            {
                MainController.Instance.mBestRankingText.text = "최고 점수: " + SaveDataController.Instance.mUser.HighScore.ToString();
            }
            else
            {
                MainController.Instance.mBestRankingText.text = "High Score: " + SaveDataController.Instance.mUser.HighScore.ToString();
            }
        }
    }
}
