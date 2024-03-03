using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float zoomSpeed, zoomMin, zoomMax, zoomScrubbing, moveScrubbing;
    [SerializeField]
    private GameObject moveObject;
    [SerializeField]
    private Camera cameraScript;
    [SerializeField]
    private Rigidbody2D rb;

    private float cameraZoom;

    // Update is called once per frame
    void Update()
    {
        //Optional Zoom
        /*
        //Get Inputs
        cameraZoom = Input.GetAxis("Mouse ScrollWheel");

        //Configure camera zoom
        cameraZoom *= zoomSpeed;
        cameraScript.fieldOfView = Mathf.Clamp(Mathf.Lerp(
            cameraScript.fieldOfView,
            cameraScript.fieldOfView - cameraZoom, zoomScrubbing),
            zoomMin, zoomMax);
        */

        //Move Camera to Object
        Vector3 cameraMovement = Vector3.Lerp(
            transform.position,
            new(moveObject.transform.position.x, moveObject.transform.position.y, -10),
            moveScrubbing);

        transform.position = cameraMovement;
    }

    public float GetZoomMin()
    {
        return zoomMin;
    }

    public float GetZoomMax()
    {
        return zoomMax;
    }
}