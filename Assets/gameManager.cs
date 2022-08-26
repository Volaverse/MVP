using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class gameManager : MonoBehaviourPunCallbacks
{
    private static Vector3 tp = new Vector3(0, 0, 0);
    private static string publicKey = "";
    private static string privateKey = "";
    private static bool wallet = false;
    private static string roomNo = "";

    [Tooltip("The prefab to use for representing the player")]
    public static gameManager Instance;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    void LoadWorld()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading World");
    }

    public void settpinfo(float a, float b)
    {
        tp[0] = 1f;
        tp[1] = a;
        tp[2] = b;
    }

    public Vector3 tpinfo()
    {
        Vector3 tp2 = tp;
        tp[0] = 0;
        if (tp2[0] == 1)
        {
            Debug.Log(tp2);
        }
        return tp2;
    }

    public void setPlayerInfo(bool _wallet, string _publicKey, string _privateKey)
    {
        wallet = _wallet;
        publicKey = _publicKey;
        privateKey = _privateKey;
        Debug.Log("Received public and private keys");
        Debug.Log("Received public: " + publicKey);
    }

    public bool isWallet()
    {
        return wallet; 
    }
    public string getPublicKey()
    {
        return publicKey;
    }

    public string getPrivateKey()
    {
        return privateKey;
    } 

    public string getRoomNo()
    {
        return roomNo;
    }

    public void setRoomNo(string _roomNo)
    {
        Debug.Log(_roomNo);
        roomNo = _roomNo;
    }
}
