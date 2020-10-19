using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    Vector3 lastCameraPosition;
    [SerializeField] Vector2 parallaxEffectAmount = new Vector2(1f, 1f);

    private void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 changeInPosition = (cameraTransform.position - lastCameraPosition) * parallaxEffectAmount;
        transform.position = transform.position + changeInPosition;
        lastCameraPosition = cameraTransform.position;
    }
}
