using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float speed, moveScrubbing;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private CameraMovement cameraMovement;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //Get Inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Configure object movement
        movement.Normalize();
        rb.velocity = Vector2.Lerp(rb.velocity, (cameraMovement.cameraScript.orthographicSize / speed) * movement , moveScrubbing);
        transform.position = new(Mathf.Clamp(transform.position.x, -40, 40), Mathf.Clamp(transform.position.y, -20, 20), -10);
    }
}
