using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAligner : MonoBehaviour
{
    private Quaternion baseRotation;

    private void Awake() => baseRotation = transform.rotation;
    private void Update()
    {
        transform.rotation *= baseRotation;
        transform.LookAt(Camera.main.transform);
    }
}
