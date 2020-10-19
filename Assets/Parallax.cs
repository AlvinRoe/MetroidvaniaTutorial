using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 changeInPosition = cameraTransform.position - lastCameraPosition;
        transform.position = transform.position + changeInPosition;
        lastCameraPosition = cameraTransform.position;
    }
}
