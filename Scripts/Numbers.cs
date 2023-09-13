using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbers : MonoBehaviour
{
     
    public GameObject cam;
     
    // Start is called before the first frame update
    void Start()
    {
         cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
 
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
        transform.rotation*=Quaternion.Euler(0, 180, 0);
    }
}
