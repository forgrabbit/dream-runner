using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    private GameObject mainCam;
    private float distance; 
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        distance = mainCam.transform.position.x - transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(mainCam.transform.position.x - distance, transform.position.y, transform.position.z);       
    }
}
