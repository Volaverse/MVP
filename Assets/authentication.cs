using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Runtime.InteropServices;
using System.Collections;
public class authentication : MonoBehaviourPunCallbacks
{
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 5;

    private const float API_CHECK_MAXTIME = 2f; //seconds
    private float apiCheckCountdown = API_CHECK_MAXTIME;

    public static gameManager manager;

    public InputField publicKeyInput;
    public InputField privateKeyInput;

    public Text error_message;
    public Text loading;

    public bool wallet = false;
    bool checkingForPublicKey = false;

    [DllImport("__Internal")]
    private static extern int ConnectToMetamask();

    [DllImport("__Internal")]
    private static extern string GetAccount();

    void Start()
    {
        publicKeyInput.text = "0x51d8E1146617106286B5b2482A12EfF96362e0cA";
        privateKeyInput.text = "68b56921fb65822e4192a10b6b29c6379c2293d24c08f2b785c609e05e7e1a39";
    }
    public void onLogin(bool _wallet)
    {
        wallet = _wallet;
        manager = gameManager.Instance;
        if (!wallet)
        {
            manager.setPlayerInfo(wallet, publicKeyInput.text, privateKeyInput.text);
            Connect();
        }
        else
        {
            manager.setPlayerInfo(wallet, "", "");
            loading.text = "Please check browser Metamask extension for connection request";
            int metamaskError = ConnectToMetamask();
            if (metamaskError != 0)
            {
                if (metamaskError == 1 || metamaskError == 2)
                {
                    loading.text = "Could not find Metamask!";
                }
                else if (metamaskError == 3)
                {
                    loading.text = "Please switch to Rinkedby!";
                }
                else
                {
                    loading.text = "Unknown connection error!";
                }
            }
            else
            {
                if (!checkingForPublicKey)
                {
                    checkingForPublicKey = true;
                    //StartCoroutine(CheckForPublicKey());
                }

            }

        }

    }

    void Update()
    {
        apiCheckCountdown -= Time.deltaTime;
        if (apiCheckCountdown <= 0)
        {
            if (checkingForPublicKey)
            {
                string pk = GetAccount();
                Debug.Log("PK: " + pk);
                if (pk != "" && pk != null)
                {
                    checkingForPublicKey = false;
                    loading.text = "Reaching volaverse servers";
                    Connect();
                }
            }
            apiCheckCountdown = API_CHECK_MAXTIME;
        }
    }

    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    void Awake()
    {
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom("Room12345");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        loading.text = "Could not reach volaverse servers. Please check internet connection";
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
    //    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    //}

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed() was called by PUN with error code: " + returnCode + " and message: " + message);
        PhotonNetwork.CreateRoom("Room12345", new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Create Room Failed");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in room: " + PhotonNetwork.CurrentRoom);
        //if (PhotonNetwork.IsMasterClient)
        manager.setRoomNo(PhotonNetwork.CurrentRoom.ToString());
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnLeftRoom()
    {
        loading.text = "You left the room";
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("isConnected was true");
            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinRoom("Room12345");
        }
        else
        {
            Debug.Log("isConnected was false");
            //AppSettings chinaSettings = new AppSettings();
            //chinaSettings.UseNameServer = true;
            //chinaSettings.ServerAddress = "ns.photonengine.cn";
            //chinaSettings.AppIdRealtime = "ChinaPUNAppId"; // TODO: replace with your own PUN AppId unlocked for China region
            //PhotonNetwork.ConnectUsingSettings(chinaSettings);
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("connected using settings");
        }
    }
}
