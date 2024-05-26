using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mapsection;


    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Respawn")){
            Instantiate(mapsection, new Vector3(0,0,-108), Quaternion.identity);
        }
    }
}
