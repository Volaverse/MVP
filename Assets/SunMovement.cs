using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 10;
    private float xRotation = 0f;
    void Start()
    {
        System.DateTime dateTime = System.DateTime.Now;
        Debug.Log(dateTime.Hour);

        //transform.position = new Vector3(distance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //float x = transform.position.x;
        //float y = transform.position.y;

        //float theta = Mathf.Atan2(y, x);
        //float del_theta = Time.deltaTime * speed / distance;
        //float new_theta = theta + del_theta;
        ////Debug.Log(theta + "+" + del_theta + "=" + new_theta + "| x = " + x + ", y = " + y);
        //if (new_theta >= 2 * Mathf.PI) new_theta -= 2 * Mathf.PI;

        //float new_x = distance * Mathf.Cos(new_theta);
        //float new_y = distance * Mathf.Sin(new_theta);
        //transform.position = new Vector3(new_x, new_y, 0);

        //transform.rotation = Quaternion.Euler(new_x, new_y, 0);

        xRotation -= Time.deltaTime * speed;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
