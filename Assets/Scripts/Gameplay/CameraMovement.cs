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
    private Rigidbody2D rb;

    private float cameraZoom;

    public Camera cameraScript;

    // Update is called once per frame
    void Update()
    {
        //Get Inputs
        cameraZoom = Input.GetAxis("Mouse ScrollWheel");

        //Configure camera zoom
        cameraZoom *= zoomSpeed;
        cameraScript.orthographicSize = Mathf.Clamp(Mathf.Lerp(
            cameraScript.orthographicSize,
            cameraScript.orthographicSize - cameraZoom, zoomScrubbing),
            zoomMin, zoomMax);
        

        //Move Camera to Object
        Vector3 cameraMovement = Vector3.Lerp(
            transform.position,
            new(moveObject.transform.position.x, moveObject.transform.position.y, -10),
            moveScrubbing);

        transform.position = cameraMovement;
    }
}