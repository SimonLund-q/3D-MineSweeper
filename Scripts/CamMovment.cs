using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] public float distanceToTarget = 10f;

    private Vector3 previousPosition;

    // Update is called once per frame
    

    
    void Update()
    {
        if (distanceToTarget <= 0){
            distanceToTarget = 0;
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                //wheel goes up
                distanceToTarget = distanceToTarget + 1;
            }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                distanceToTarget = distanceToTarget - 1;
                //wheel goes down
            }
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
