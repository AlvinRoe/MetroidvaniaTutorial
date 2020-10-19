using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Vector2 parallaxEffectAmount = new Vector2(1f, 1f);
    Transform cameraTransform;
    Vector3 lastCameraPosition;
    float widthOfImage;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        widthOfImage = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 changeInPosition = (cameraTransform.position - lastCameraPosition) * parallaxEffectAmount;
        transform.position = transform.position + changeInPosition;
        lastCameraPosition = cameraTransform.position;

        if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= widthOfImage)
        {
            float widthOffset = (cameraTransform.position.x - transform.position.x) % widthOfImage;
            transform.position = new Vector3(cameraTransform.position.x + widthOffset, transform.position.y);
        }
    }
}
