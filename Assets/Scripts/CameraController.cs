using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private Camera _camera;
    public float movementSpeed;
    Vector3 firstTouchPos;
    Vector3 moveDirection;
    Rigidbody rb;
    [SerializeField] float slowDownSpeed;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
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
                print(rbForce);
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                rb.velocity = Vector3.zero;
            }
            }
    }

        //Vector2 mousePos = Input.mousePosition;

        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = _camera.ScreenPointToRay(mousePos);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        mapClickPoint = new Vector3(hit.point.x, 0, hit.point.z);
        //    }
        //}
        //if(Input.GetMouseButton(0))
        //{
        //    Ray ray2 = _camera.ScreenPointToRay(mousePos);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray2, out hit))
        //    {
        //        Vector3 mapHoverPoint = new Vector3(hit.point.x, 0, hit.point.z);
        //        moveDirection = (mapClickPoint - mapHoverPoint).normalized;
        //        if (Vector3.Distance(mapClickPoint, mapHoverPoint) >= 0.1f)
        //        {
        //            transform.position += moveDirection * movementSpeed * Time.deltaTime;
        //        }
        //    }
        //}
    }
