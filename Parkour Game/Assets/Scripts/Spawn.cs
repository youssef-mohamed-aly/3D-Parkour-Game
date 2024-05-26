using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Awake()
    {
       Player.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
