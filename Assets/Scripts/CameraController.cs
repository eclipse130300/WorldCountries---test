using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    public ListManager listManager;
    Vector3 firstTouchPos;
    Vector3 movedTouchPos;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (!listManager.inListMenu) //check if listPanel isn't activated
        {
            if (Input.touchCount > 0)
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        firstTouchPos = new Vector3(touch.position.x, 0, touch.position.y);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        movedTouchPos = new Vector3(touch.position.x, 0, touch.position.y);
                        Vector3 swipeVector = movedTouchPos - firstTouchPos;
                        Vector3 rbForce = swipeVector * movementSpeed * Time.deltaTime;
                        rb.AddForce(rbForce, ForceMode.Force);
                    }
                    //else if (touch.phase == TouchPhase.Moved)
                    //{
                    //    transform.position = Camera.main.ScreenToWorldPoint(movedTouchPos);
                    //}
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        rb.velocity = Vector3.zero;
                    }
                }
        }
        }
    }

