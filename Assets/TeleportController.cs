using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeleportController : MonoBehaviourPun
{
    // Start is called before the first frame update
    public static gameManager manager;
    void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        manager = gameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Vector3 tp = manager.GetComponent<gameManager>().tpinfo();
        if (tp[0] == 1)
        {
            transform.position = new Vector3(tp[1], transform.position[1], tp[2]);
        }
    }
}
