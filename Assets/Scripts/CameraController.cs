using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public MeshRenderer map;
    public ListManager listManager;
    [SerializeField] float movementSpeed;
    Vector3 firstTouchPos;
    Vector3 movedTouchPos;
    Rigidbody rb;

    float xClamped;
    float zClamped;
    float minYpos = 0.350f;
    [SerializeField] float maxYpos;
    float heightRatio;

    float startTSMagnitude;
    public float zoomFactor;
    enum MoveState {horizontal, zooming}
    MoveState state;

    float delayTime = 0.3f; //to detect zooming appropriately
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        xClamped = map.bounds.size.x / 2;
        zClamped = map.bounds.size.z / 2;
        state = MoveState.horizontal;
    }

    void LateUpdate()
    {
        if (!listManager.inListMenu) 
        {
                if (Input.touchCount == 1 && state == MoveState.horizontal)
                {
                heightRatio = transform.position.y/maxYpos;
                Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        firstTouchPos = new Vector3(touch.position.x, 0, touch.position.y);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        movedTouchPos = new Vector3(touch.position.x, 0, touch.position.y);
                        Vector3 swipeVector = movedTouchPos - firstTouchPos;
                        Vector3 horizontalForce = swipeVector * movementSpeed * heightRatio * Time.deltaTime;
                        rb.AddForce(horizontalForce, ForceMode.Force);
                    }
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        rb.velocity = Vector3.zero;
                    }
                }
                else if(Input.touchCount == 2)
                {
                state = MoveState.zooming;
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);
                Vector2 middlePoint = (touch1.position - touch2.position) / 2;
                float magnBetweenTouches = (touch1.position - touch2.position).magnitude;
                if(touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
                {
                    startTSMagnitude = magnBetweenTouches;
                }
                if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
                {
                    float finishedMagnitude = magnBetweenTouches;
                    Ray ray = Camera.main.ScreenPointToRay(middlePoint);
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit))
                    {
                    Vector3 zoomForce = (transform.position - hit.point).normalized * zoomFactor * Time.deltaTime;
                        if (finishedMagnitude > startTSMagnitude)
                        {
                            // move closer
                            rb.AddForce(-zoomForce, ForceMode.Force);
                        }
                        else
                        {
                            // move further
                            rb.AddForce(zoomForce, ForceMode.Force);                       
                        }
                        StartCoroutine(BlockRandomInput());
                    }
                }

                }
                //Clamp Box over the map
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -xClamped, xClamped),
                Mathf.Clamp(transform.position.y, minYpos, maxYpos),
                Mathf.Clamp(transform.position.z, -zClamped, zClamped));
        }
    }
    IEnumerator BlockRandomInput()
    {
        yield return new WaitForSeconds(delayTime);
        state = MoveState.horizontal;
    }
}

