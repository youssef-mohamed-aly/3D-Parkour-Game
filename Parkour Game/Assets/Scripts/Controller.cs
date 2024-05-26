using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float minx = -60f;
    public float maxx = 60f;

    public float sensitivity;
    public Camera cam;
    float rotx = 0f;
    float roty = 0f;

    void Start(){
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update(){
        roty += Input.GetAxis("Mouse X") * sensitivity;
        rotx += Input.GetAxis("Mouse Y") * sensitivity;

        rotx = Mathf.Clamp(rotx, minx, maxx);

        transform.localEulerAngles = new Vector3(0,roty,0);
        cam.transform.localEulerAngles = new Vector3(-rotx,0,0);
    }
}
