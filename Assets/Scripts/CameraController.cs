using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    Vector3 firstTouchPos;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstTouchPos = new Vector3(touch.position.x, 0, touch.position.y);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector3 movedTouchPos = new Vector3(touch.position.x, 0, touch.position.y);
                Vector3 swipeVector = movedTouchPos - firstTouchPos;
                Vector3 rbForce = swipeVector * movementSpeed * Time.deltaTime;
                rb.AddForce(rbForce, ForceMode.Force);
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                rb.velocity = Vector3.zero;
            }
            }
    }
}
