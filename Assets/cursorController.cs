using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class cursorController : MonoBehaviour
{
    // Start is called before the first frame update
    bool locked = false;
    public Button startButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool Lpressed = Input.GetKeyDown(KeyCode.L);
        if (Lpressed)
        {
            Debug.Log("L pressed");
            if (!locked)
            {
                LockCursor();
            }
            else
            {
                unlockCursor();
            }
        }
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
        if (escapePressed)
        {
            locked = false;
            GameObject lp = GameObject.Find("localPlayer");
            ThirdPersonUserControl ss = lp.GetComponent<ThirdPersonUserControl>();
            ss.lockCursor(false);
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        locked = true;
        GameObject lp = GameObject.Find("localPlayer");
        ThirdPersonUserControl ss = lp.GetComponent<ThirdPersonUserControl>();
        ss.lockCursor(true);
    }

    public void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        locked = false;
        GameObject lp = GameObject.Find("localPlayer");
        ThirdPersonUserControl ss = lp.GetComponent<ThirdPersonUserControl>();
        ss.lockCursor(false);
    }
    public void destroyMyself()
    {
        Destroy(startButton.gameObject);
    }
}
