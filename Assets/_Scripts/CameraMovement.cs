using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public Camera Camera;
    protected Plane Plane;

    public bool InBounds;
    public Vector3 OldPosition;

    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
        Input.multiTouchEnabled = true;
        InBounds = true;

    }

    private void Update()
    {
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        
        if (Input.touchCount >= 1 && InBounds==true)
        {
            OldPosition = Camera.transform.position;
            Delta1 = PositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                Camera.transform.Translate(Delta1, Space.World);
        }
        if (InBounds == false)
        {
            Camera.transform.position=OldPosition;
            InBounds = true;
        }

        if (Camera.transform.position.x > 5 || Camera.transform.position.x < -25 || Camera.transform.position.z > 10 || Camera.transform.position.z < -23)
        {
            InBounds = false;
        }
    }

    protected Vector3 PositionDelta(Touch touch)
    {
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    { 
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

#endif
}