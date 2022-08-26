using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab;
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 4f, -25f), Quaternion.identity, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
