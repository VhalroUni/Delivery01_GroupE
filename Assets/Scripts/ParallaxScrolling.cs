using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private float parallaxEffectMultiplier; 

    private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.position -= new Vector3(deltaMovement.x, deltaMovement.y) * parallaxEffectMultiplier;
        transform.position = new Vector3(transform.position.x, transform.position.y, 10); 

        lastCameraPosition = cameraTransform.position;
    }
}
