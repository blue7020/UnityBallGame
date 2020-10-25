using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;


// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPController : MonoBehaviour, IStoreListener
{
    public static IAPController Instance;
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.

    ////상품 ID가 어떻게 사용하는지에 대한 Sample
    //public static string kProductIDConsumable = "consumable"; //한번 살 수 있게
    //public static string kProductIDNonConsumable = "nonconsumable"; //여러번 살 수 있게
    //내가 헷갈려서 이 곳에서는 논컨슈머블과 컨슈머블 변수 이름을 반대로 기재함;;
    //public static string kProductIDSubscription = "subscription";//정기 구독형

    //// Apple App Store-specific product identifier for the subscription product.
    //private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    //// Google Play Store-specific product identifier subscription product.
    //private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";
    ////

    //private string Consumable_Ruby100 = "Ruby_100";
    //private string NonConsumable_StarterPack = "StarterPack";

    private string Consumable_NOADS = "NoAds_1";
    private string GooglePlay_NOADS = "noads1";

    private string Consumable_Chara_Hero = "Chara08Hero";
    private string GooglePlay_Chara_Hero = "chara_08hero";

    private string Consumable_Chara_Castella = "Chara09Castella";
    private string GooglePlay_Chara_Castella = "chara_09castella";

    private string Consumable_Chara_ShrimpNinja = "Chara10ShrimpNinja";
    private string GooglePlay_Chara_ShrimpNinja = "chara_10shrimpninja";

    private string Consumable_Chara_DemonToast = "Chara11DemonToast";
    private string GooglePlay_Chara_DemonToast = "chara_11demontoast";

    private string NonConsumable_Donate = "Donate_Statue";
    private string GooglePlay_Donate = "donate_statue";

    private string NonConsumable_Syrup_01 = "Syrup_01";
    private string GooglePlay_Syrup_01 = "syrup_01";

    private string NonConsumable_Syrup_02 = "Syrup_02";
    private string GooglePlay_Syrup_02 = "syrup_02";

    private string NonConsumable_Syrup_03 = "Syrup_03";
    private string GooglePlay_Syrup_03 = "syrup_03";


    //private string GooglePlay_Ruby100 = "ruby100";//구글 플레이 콘솔에서 인앱결제 상품을 등록할때 ID(대문자가 안된다)
    //private string GooglePlay_StarterPack = "starterpack";

    private string ios_Ruby100 = "01100"; //만약 숫자만 될 때 사용하는 방법 01은 아이템 ID 100은 개수
    private string ios_StarterPack = "s00";//한 문자만 사용하는 방법 s은 아이템 이니셜


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

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        //여긴 건들지 말기, 이건 꼭 돌아가야함
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
        //
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Consumable_NOADS, ProductType.NonConsumable, new IDs()
        {
            { GooglePlay_NOADS,GooglePlay.Name}//해당 스토어에 이 상품을 연동시킨 것이다. 만약 한 플랫폼에만 팔고 싶다면 둘중 하나만 쓰면 된다.
            //,{ ios_Ruby100,AppleAppStore.Name}
        });

        builder.AddProduct(Consumable_Chara_Hero, ProductType.NonConsumable, new IDs()
        {
            { GooglePlay_Chara_Hero,GooglePlay.Name}
        });

        builder.AddProduct(Consumable_Chara_Castella, ProductType.NonConsumable, new IDs()
        {
            { GooglePlay_Chara_Castella,GooglePlay.Name}
        });

        builder.AddProduct(Consumable_Chara_ShrimpNinja, ProductType.NonConsumable, new IDs()
        {
            { GooglePlay_Chara_ShrimpNinja,GooglePlay.Name}
        });

        builder.AddProduct(Consumable_Chara_DemonToast, ProductType.NonConsumable, new IDs()
        {
            { GooglePlay_Chara_DemonToast,GooglePlay.Name}
        });

        builder.AddProduct(NonConsumable_Donate, ProductType.Consumable, new IDs()
        {
            { GooglePlay_Donate,GooglePlay.Name},
        });

        builder.AddProduct(NonConsumable_Syrup_01, ProductType.Consumable, new IDs()
        {
            { GooglePlay_Syrup_01,GooglePlay.Name},
        });

        builder.AddProduct(NonConsumable_Syrup_02, ProductType.Consumable, new IDs()
        {
            { GooglePlay_Syrup_02,GooglePlay.Name},
        });

        builder.AddProduct(NonConsumable_Syrup_03, ProductType.Consumable, new IDs()
        {
            { GooglePlay_Syrup_03,GooglePlay.Name},
        });


        //builder.AddProduct(Consumable_Ruby100, ProductType.Consumable, new IDs()
        //{
        //    { GooglePlay_Ruby100,GooglePlay.Name},//해당 스토어에 이 상품을 연동시킨 것이다. 만약 한 플랫폼에만 팔고 싶다면 둘중 하나만 쓰면 된다.
        //    { ios_Ruby100,AppleAppStore.Name}
        //});
        //builder.AddProduct(NonConsumable_StarterPack, ProductType.NonConsumable, new IDs()
        //{
        //    { GooglePlay_StarterPack,GooglePlay.Name},
        //    { ios_StarterPack,AppleAppStore.Name}
        //});

        //참고용
        //// Add a product to sell / restore by way of its identifier, associating the general identifier
        //// with its store-specific identifiers.
        //builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
        //// Continue adding the non-consumable product.
        //builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
        //// And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        //// if the Product ID was configured differently between Apple and Google stores. Also note that
        //// one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        //// must only be referenced here. 
        //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){//인앱결제 아이디
        //        { kProductNameAppleSubscription, AppleAppStore.Name },
        //        { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //    });

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    //결제를 위한 함수 - 아이템별로 함수를 각각 만들어줘야한다.
    public void BuyNOAds()
    {
        BuyProductID(Consumable_NOADS);
    }

    public void BuyCharaHero()
    {
        BuyProductID(Consumable_Chara_Hero);
    }

    public void BuyCharaCastella()
    {
        BuyProductID(Consumable_Chara_Castella);
    }

    public void BuyCharaShrimpNinja()
    {
        BuyProductID(Consumable_Chara_ShrimpNinja);
    }

    public void BuyCharaDemonToast()
    {
        BuyProductID(Consumable_Chara_DemonToast);
    }

    public void BuyDonateStatue()//다회성 결제 가능 상품
    {
        BuyProductID(NonConsumable_Donate);
    }

    public void BuySyrup01()//다회성 결제 가능 상품
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(NonConsumable_Syrup_01);
    }

    public void BuySyrup02()//다회성 결제 가능 상품
    {
        BuyProductID(NonConsumable_Syrup_02);
    }

    public void BuySyrup03()//다회성 결제 가능 상품
    {
        BuyProductID(NonConsumable_Syrup_03);
    }


    //public void BuyRuby()
    //{
    //    BuyProductID(Consumable_Ruby100);
    //}

    //public void BuyStarterPack()
    //{
    //    BuyProductID(NonConsumable_StarterPack);
    //}
    //public void BuyConsumable()
    //{
    //    // Buy the consumable product using its general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    BuyProductID(kProductIDConsumable);
    //}


    //public void BuyNonConsumable()//다회성 결제 가능 상품
    //{
    //    // Buy the non-consumable product using its general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    BuyProductID(kProductIDNonConsumable);
    //}


    //public void BuySubscription()//구독형
    //{
    //    // Buy the subscription product using its the general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    // Notice how we use the general product identifier in spite of this ID being mapped to
    //    // custom store-specific identifiers above.
    //    BuyProductID(kProductIDSubscription);
    //}


    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        bool validPurchase = true;
#if UNITY_EDITOR
#elif UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
        // Prepare the validator with the secrets we prepared in the Editor
        // obfuscation window.
        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
            AppleTangle.Data(), Application.identifier);

        try
        {
            // On Google Play, result has a single product ID.
            // On Apple stores, receipts contain multiple products.
            var result = validator.Validate(args.purchasedProduct.receipt);
            // For informational purposes, we list the receipt(s)
            Debug.Log("Receipt is valid. Contents:");
            foreach (IPurchaseReceipt productReceipt in result)
            {
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);
            }
        }
        catch (IAPSecurityException)
        {
            Debug.Log("Invalid receipt, not unlocking content");
            validPurchase = false;
        }
#endif

        if (validPurchase)//영수증 처리 기본
        {
            if (String.Equals(args.purchasedProduct.definition.id, Consumable_NOADS, StringComparison.Ordinal))//광고제거
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                SaveDataController.Instance.mUser.NoAds = true;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Chara_Hero, StringComparison.Ordinal))//히어로 캐릭터
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                SaveDataController.Instance.mUser.CharacterHas[8]=true;
                SaveDataController.Instance.mUser.CharacterOpen[8] = true;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Chara_Castella, StringComparison.Ordinal))//카스테라 캐릭터
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                SaveDataController.Instance.mUser.CharacterHas[9] = true;
                SaveDataController.Instance.mUser.CharacterOpen[9] = true;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Chara_ShrimpNinja, StringComparison.Ordinal))//쉬림프 닌자 캐릭터
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                SaveDataController.Instance.mUser.CharacterHas[10] = true;
                SaveDataController.Instance.mUser.CharacterOpen[10] = true;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Chara_DemonToast, StringComparison.Ordinal))//데몬 토스트 캐릭터
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                SaveDataController.Instance.mUser.CharacterHas[11] = true;
                SaveDataController.Instance.mUser.CharacterOpen[11] = true;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, NonConsumable_Donate, StringComparison.Ordinal))//석상 기부
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                if (SaveDataController.Instance.mUser.DonateCount==0)
                {
                    SaveDataController.Instance.mUser.CharacterHas[7] = true;
                    SaveDataController.Instance.mUser.CharacterOpen[7] = true;
                    SaveDataController.Instance.mUser.WeaponHas[25] = true;
                    SaveDataController.Instance.mUser.WeaponOpen[25] = true;
                }
                else if (SaveDataController.Instance.mUser.DonateCount == 1)
                {
                    SaveDataController.Instance.mUser.CharacterHas[2] = true;
                    SaveDataController.Instance.mUser.CharacterOpen[2] = true;
                }
                else
                {
                    GameSetting.Instance.GetSyrup(5000);
                }
                MainLobbyUIController.Instance.ShowSyrupText();
                SaveDataController.Instance.mUser.DonateCount++;
                AngelStatue.Instance.ShowRewardWindow();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Chara_DemonToast, StringComparison.Ordinal))//시럽 3000개
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                GameSetting.Instance.GetSyrup(5000);
                MainLobbyUIController.Instance.ShowSyrupText();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Chara_DemonToast, StringComparison.Ordinal))//시럽 6500개
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                GameSetting.Instance.GetSyrup(11000);
                MainLobbyUIController.Instance.ShowSyrupText();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Chara_DemonToast, StringComparison.Ordinal))//시럽 12000개
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                GameSetting.Instance.GetSyrup(33000);
                MainLobbyUIController.Instance.ShowSyrupText();
            }
            //else if (String.Equals(args.purchasedProduct.definition.id, NonConsumable_StarterPack, StringComparison.Ordinal))
            //{
            //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //    // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //    //해당 아이템 지급
            //}
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else//결제 실패
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }
            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
        }
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}