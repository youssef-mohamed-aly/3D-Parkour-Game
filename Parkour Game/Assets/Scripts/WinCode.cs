using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WinCode : MonoBehaviour
{

    public GameObject leveldonetext;
    

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            leveldonetext.SetActive(true);
            Time.timeScale = 0;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}
