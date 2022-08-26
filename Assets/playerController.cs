using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class playerController : MonoBehaviourPunCallbacks
{
    #region Private Fields
    float xRotation = 0f;
    bool map_opened = false;
    public static gameManager manager;
    #endregion

    #region public Fields
    public static GameObject LocalPlayerInstance;
    public CharacterController characterController;
    public Camera playerCamera;
    public float speed = 5f;
    public float mouseSens = 100f;
    #endregion

    void Start()
    {
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            return;
        }
        Debug.Log("Mai ninja hattori aa gya hun");
        Camera.main.gameObject.SetActive(false);
        manager = gameManager.Instance;
        name = "localPlayer";
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 tp = manager.GetComponent<gameManager>().tpinfo();
        if (tp[0] == 1)
        {
            transform.position = new Vector3(tp[1], transform.position[1], tp[2]);
        }
    }

    public void getClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Construction")
                {
                    Debug.Log("It's working!");
                }
                else
                {
                    Debug.Log("nopz");
                }
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }
}
    