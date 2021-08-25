using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using PlayNANOO;
using PlayNANOO.CloudCode;
using PlayNANOO.SimpleJSON;

public class PlayNANOOExample : MonoBehaviour
{
    Plugin plugin;
    void Start()
    {
        plugin = Plugin.GetInstance();

        plugin.SetLanguage(Configure.PN_LANG_EN);

        // Guest SignIn
        plugin.AccountGuestSignIn((status, errorCode, jsonString, values) =>
        {
            AccessEvent();
        });
    }

    // Update is called once per frame
    void Update()
    {
        plugin.AccountCheckDuplicate(OnCheckAccountDuplicate);
    }

    void OnCheckAccountDuplicate(bool isDuplicate)
    {
        if (isDuplicate)
        {
            Debug.LogError("Duplicate connection has been detected.");
        }
    }

    /// <summary>
    /// Open Forum Banner
    /// </summary>
    public void OpenBanner()
    {
        plugin.OpenBanner();
    }

    /// <summary>
    /// Open Forum
    /// </summary>
    public void OpenForum()
    {
        plugin.OpenForum();
    }

    /// <summary>
    /// Open HelpDesk
    /// </summary>
    public void OpenHelpDesk()
    {
        plugin.SetHelpDeskOptional("OptionTest1", "ValueTest1");
        plugin.SetHelpDeskOptional("OptionTest2", "ValueTest2");
        plugin.OpenHelpDesk();
    }

    /// <summary>
    /// Data Query in Forum
    /// </summary>
    public void ForumThread()
    {
        plugin.ForumThread(Configure.PN_FORUM_THREAD, 10, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList list = (ArrayList)dictionary["list"];
                foreach (Dictionary<string, object> thread in list)
                {
                    Debug.Log(thread["seq"]);
                    Debug.Log(thread["title"]);
                    Debug.Log(thread["summary"]);
                    Debug.Log(thread["attach_file"]);
                    Debug.Log(thread["url"]);
                    Debug.Log(thread["post_date"]);

                    foreach (string attach in (ArrayList)thread["attach_file"])
                    {
                        Debug.Log(attach);
                    }
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    #region Accounts
    public void AccountLink()
    {
        plugin.AccountLink("TEST_UserUniqueID", Configure.PN_ACCOUNT_GOOGLE, (status, errorCode, jsonString, values) =>
        {
            Debug.Log(values["access_token"].ToString());
            Debug.Log(values["refresh_token"].ToString());
            Debug.Log(values["uuid"].ToString());
            Debug.Log(values["openID"].ToString());
            Debug.Log(values["nickname"].ToString());
            Debug.Log(values["linkedID"].ToString());
            Debug.Log(values["linkedType"].ToString());
            Debug.Log(values["country"].ToString());
        });
    }

    public void AccountGuest()
    {
        plugin.AccountGuestSignIn((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountEmailSignIn()
    {
        string email = "test@email.com";
        string password = "password";
        plugin.AccountEmailSignIn(email, password, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountEmailSignUp()
    {
        string email = "test@email.com";
        string password = "password";
        plugin.AccountEmailSignUp(email, password, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountNicknameExists()
    {
        string nickname = "TEST_Nickname";
        plugin.AccountNicknameExists(nickname, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["status"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountNicknameGet()
    {
        plugin.AccountNicknameGet((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["nickname"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountNicknamePut()
    {
        string nickname = "TEST_Nickname";
        plugin.AccountNickanmePut(nickname, true, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["nickname"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenInfo()
    {
        plugin.AccountTokenInfo((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenStatus()
    {
        plugin.AccountTokenStatus((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["status"].ToString());
            }
            else
            {
                Debug.Log("Fail");
                Debug.Log(jsonString.ToString());
            }
        });
    }

    public void AccountTokenRefresh()
    {
        plugin.AccountTokenRefresh((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenSingIn()
    {
        plugin.AccountTokenSignIn((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenSingOut()
    {
        plugin.AccountTokenSignOut((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(jsonString);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenClear()
    {
        plugin.AccountDeviceClear((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(jsonString);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountSocialChange()
    {
        string accountToken = "SERVICE_TOKEN";
        plugin.AccountSocialChange(accountToken, Configure.PN_ACCOUNT_APPLE_ID, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountSocialChangePrevious()
    {
        string accountUserUniqueID = "UserUniqueID";
        string accountToken = "SERVICE_TOKEN";
        plugin.AccountSocialChangePrevious(accountUserUniqueID, accountToken, Configure.PN_ACCOUNT_APPLE_ID, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Server Time
    public void ServerTime()
    {
        plugin.ServerTime((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["timezone"]);
                Debug.Log(dictionary["timestamp"]);
                Debug.Log(dictionary["ISO_8601_date"]);
                Debug.Log(dictionary["date"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Access Event
    public void AccessEvent()
    {
        plugin.AccessEvent((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                if (dictionary.ContainsKey("open_id"))
                {
                    Debug.Log(dictionary["open_id"]);
                }

                if (dictionary.ContainsKey("server_timestamp"))
                {
                    Debug.Log(dictionary["server_timestamp"]);
                }

                if (dictionary.ContainsKey("postbox_subscription"))
                {
                    foreach (Dictionary<string, object> subscription in (ArrayList)dictionary["postbox_subscription"])
                    {
                        Debug.Log(subscription["product"]);
                        Debug.Log(subscription["ttl"]);
                    }
                }

                if (dictionary.ContainsKey("invite_rewards"))
                {
                    foreach (Dictionary<string, object> invite in (ArrayList)dictionary["invite_rewards"])
                    {
                        Debug.Log(invite["item_code"]);
                        Debug.Log(invite["item_count"]);
                    }
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Coupon
    public void Coupon()
    {
        plugin.Coupon("COUPON_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["code"]);
                Debug.Log(dictionary["item_code"]);
                Debug.Log(dictionary["item_count"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Inbox(Postbox)
    public void Postbox()
    {
        plugin.PostboxItem((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList items = (ArrayList)dictionary["item"];
                foreach (Dictionary<string, object> item in items)
                {
                    Debug.Log(item["uid"]);
                    Debug.Log(item["message"]);
                    Debug.Log(item["item_code"]);
                    Debug.Log(item["item_count"]);
                    Debug.Log(item["expire_sec"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxItemSend()
    {
        plugin.PostboxItemSend("ITEM_CODE", 1, 7, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxItemUse()
    {
        plugin.PostboxItemUse("ITEM_UID", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["item_code"]);
                Debug.Log(dictionary["item_count"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxMultiItemUse()
    {
        ArrayList items = new ArrayList();
        items.Add("ITEM_UID_1");
        items.Add("ITEM_UID_2");

        plugin.PostboxMultiItemUse(items, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList useItems = (ArrayList)dictionary["item"];
                foreach (Dictionary<string, object> item in useItems)
                {
                    Debug.Log(item["uid"]);
                    Debug.Log(item["item_code"]);
                    Debug.Log(item["item_count"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxClear()
    {
        plugin.PostboxClear((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxSubscriptionRegister()
    {
        plugin.PostboxSubscriptionRegister("PRODUCT_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxSubscriptionCancel()
    {
        plugin.PostboxSubscriptionCancel("PRODUCT_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Cloud Data(Storage)
    public void StorageSave()
    {
        plugin.StorageSave("TEST_KEY_001", "TEST_VALUE_001", false, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void StorageLoad()
    {
        plugin.StorageLoad("TEST_KEY_001", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Leaderboard(Ranking)
    public void Ranking()
    {
        plugin.Ranking("RANKING_CODE", 50, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList list = (ArrayList)dictionary["list"];
                foreach (Dictionary<string, object> rank in list)
                {
                    Debug.Log(rank["score"]);
                    Debug.Log(rank["data"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingRange()
    {
        plugin.RankingRange("RANKING_CODE", 1, 10, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach(Dictionary<string, object> item in (ArrayList)values["items"])
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

    public void RankingPreviousRange()
    {
        plugin.RankingPreviousRange("RANKING_CODE", 1, 1, 10, (status, errorMessage, jsonString, values) => {
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

    public void RankingRecord()
    {
        plugin.RankingRecord("exithero-RANK-6AB7FA94-64E39A5B", 100, "TEST_PLAYER_RANKING_VALUE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingPersonal()
    {
        plugin.RankingPersonal("RANKING_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["ranking"]);
                Debug.Log(dictionary["data"]);
                Debug.Log(dictionary["total_player"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingSeason()
    {
        plugin.RankingSeasonInfo("RANKING_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["season"]);
                Debug.Log(dictionary["expire_sec"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region In App Purchase
    public void IapReceiptionAndroid()
    {
        plugin.ReceiptVerificationAOS("PRODUCT_ID", "RECEIPT", "SIGNATURE", "CURRENCY", 100, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["package"]);
                Debug.Log(dictionary["product_id"]);
                Debug.Log(dictionary["order_id"]);
                Debug.Log("Issue Item");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void IapReceiptioniOS()
    {
        plugin.ReceiptVerificationIOS("PRODUCT_ID", "RECEIPT", "CURRENCY", 100, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["package"]);
                Debug.Log(dictionary["product_id"]);
                Debug.Log(dictionary["order_id"]);
                Debug.Log("Issue Item");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void IapReceiptionOneStoreKR()
    {
        plugin.ReceiptVerificationOneStoreKR("PRODUCT_ID", "PURCHASE_ID", "RECEIPT", "CURRENCY", 100, true, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["package"]);
                Debug.Log(dictionary["product_id"]);
                Debug.Log(dictionary["order_id"]);
                Debug.Log("Issue Item");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Invite
    public void Invite(string inviteCode)
    {
        plugin.Invite(inviteCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                string url = dictionary["url"].ToString();

                plugin.OpenShare("Please Enter Invite Message", url);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Cache Data
    public void CacheExists()
    {
        string cacheKey = "TEST001";
        plugin.CacheExists(cacheKey, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheGet()
    {
        string cacheKey = "TEST001";
        plugin.CacheGet(cacheKey, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheMultiGet()
    {
        ArrayList cacheKeys = new ArrayList();
        cacheKeys.Add("TEST001");
        cacheKeys.Add("TEST002");
        cacheKeys.Add("TEST003");

        plugin.CacheMultiGet(cacheKeys, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["values"])
                {
                    Debug.Log(value["key"]);
                    Debug.Log(value["value"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheSet()
    {
        string cacheKey = "TEST001";
        string cacheValue = "TESTValue";
        int cacheTTL = 3600;
        plugin.CacheSet(cacheKey, cacheValue, cacheTTL, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheIncrby()
    {
        string cacheKey = "TEST002";
        int cacheValue = 100;
        int cacheTTL = 3600;
        plugin.CacheIncrby(cacheKey, cacheValue, cacheTTL, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheDecrby()
    {
        string cacheKey = "TEST003";
        int cacheValue = 100;
        int cacheTTL = 3600;
        plugin.CacheDecrby(cacheKey, cacheValue, cacheTTL, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheDelete()
    {
        string cacheKey = "TEST003";
        plugin.CacheDel(cacheKey, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Currency
    public void CurrencyAll()
    {
        plugin.CurrencyAll((status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach(Dictionary<string, object> item in (ArrayList)values["items"])
                {
                    Debug.Log(item["currency"]);
                    Debug.Log(item["amount"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencySet()
    {
        plugin.CurrencySet("CURRENCY_CODE", 1000, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencyGet()
    {
        plugin.CurrencyGet("CURRENCY_CODE", (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencyCharge()
    {
        plugin.CurrencyCharge("CURRENCY_CODE", 1000, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencySubtract()
    {
        plugin.CurrencySubtract("CURRENCY_CODE", 1000, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region CloudeCode
    public void CloudCodeExecution()
    {
        var parameters = new CloudCodeExecution()
        {
            TableCode = "CLOUDCODE_TALBE_CODE",
            FunctionName = "CLOUDCODE_FUNCTION_NAME",
            FunctionArguments = new { InputValue1 = "InputValue1", InputValue2 = "InputValue2", InputValue3 = "InputValue3" }
        };

        plugin.CloudCodeExecution(parameters, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                JSONNode jSONNode = JSONNode.Parse(dictionary["value"].ToString());
                Debug.Log(jSONNode["message"].Value);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Friends
    public void FriendInfo()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        plugin.FriendInfo(friendCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["relationship_count"].ToString());
                Debug.Log(dictionary["ready_count"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendAll()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        plugin.Friend(friendCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["friends"])
                {
                    Debug.Log(value["relationship_code"]);
                    Debug.Log(value["uuid"]);
                    Debug.Log(value["nickname"]);
                    Debug.Log(value["access_timezone"]);
                    Debug.Log(value["access_diff"]);
                    Debug.Log(value["access_timestamp"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendReadyAll()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        plugin.FriendReady(friendCode, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["friends"])
                {
                    Debug.Log(value["relationship_code"]);
                    Debug.Log(value["uuid"]);
                    Debug.Log(value["nickname"]);
                    Debug.Log(value["access_timezone"]);
                    Debug.Log(value["access_diff"]);
                    Debug.Log(value["access_timestamp"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendRequest()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        string friendOpenID = "PLAYER_OPEN_ID";
        plugin.FriendRequest(friendCode, friendOpenID, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendAccept()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        string friendRelationshipCode = "FRIEND_RELATIONSHIP_CODE";
        plugin.FriendAccept(friendCode, friendRelationshipCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendDelete()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        string friendRelationshipCode = "FRIEND_RELATIONSHIP_CODE";
        plugin.FriendDelete(friendCode, friendRelationshipCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendRandomSearch()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        int limit = 10;
        plugin.FriendRandomSearch(friendCode, limit, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["friends"])
                {
                    Debug.Log(value["uuid"]);
                    Debug.Log(value["open_id"]);
                    Debug.Log(value["nickname"]);
                    Debug.Log(value["signin_time"]);
                    Debug.Log(value["signin_date"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region BlockReason
    public void BlockReason()
    {
        plugin.BlockReason((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)values["Items"])
                {
                    Debug.Log(value["Reason"]);
                    Debug.Log(value["Permanent"]);
                    Debug.Log(value["ExpireDate"]);
                    Debug.Log(value["TimeUntilExpire"]);
                    foreach(string service in (string[])value["Services"])
                    {
                        Debug.Log(service);
                    }
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Event logs
    public void LogWrite()
    {
        var messages = new PlayNANOO.Monitor.LogMessages();
        messages.Add(Configure.PN_LOG_DEBUG, "Message1");
        messages.Add(Configure.PN_LOG_INFO, "Message2");
        messages.Add(Configure.PN_LOG_ERROR, "Message3");

        plugin.LogWrite(new PlayNANOO.Monitor.LogWrite()
        {
            EventCode = "TEST_LOG_20210607001",
            EventMessages = messages
        });
    }
    #endregion

    public void OnApplicationFocus(bool focus)
    {
        if (plugin != null && focus)
        {
            AccessEvent();
        }
        Debug.Log("Focus");
    }
}
