using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class CheckEPressable : MonoBehaviour
{
    private const float API_CHECK_MAXTIME = 1f; //seconds
    private float apiCheckCountdown = API_CHECK_MAXTIME;
    public Text pressE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        apiCheckCountdown -= Time.deltaTime;
        if (apiCheckCountdown <= 0)
        {
            GameObject player = GameObject.Find("localPlayer");
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(player.transform.position + new Vector3(0, 1, 0), new Vector3(0, 1, 0), out hitInfo);
            if (hit)
            {
                Debug.Log("hit:"+hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.name == "Classic_Ceiling_01_snaps001")
                {
                    Debug.Log("setting press E active");
                    pressE.enabled = true;
                }
                else
                {
                    pressE.enabled = false;
                }
            }
            else
            {
                pressE.enabled = false;
            }
            apiCheckCountdown = API_CHECK_MAXTIME;
        }
    }
}
