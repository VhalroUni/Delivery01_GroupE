using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; //Referencia a la camara
    [SerializeField] private float parallaxEffectMultiplier; //Numero efecto parallax

    private Vector3 lastCameraPosition;

    private void Start()
    {
        //Posicion inicial camara
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        //Desplazamiento de la camara
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        //Efecto en ambas direcciones
        transform.position -= new Vector3(deltaMovement.x, deltaMovement.y) * parallaxEffectMultiplier;
        transform.position = new Vector3(transform.position.x, transform.position.y, 10); //!without this line of code the background sometimes moves in front of objects. I don't know why it causes the backround's z to be 20.

        //Posicion anterior de la cámara
        lastCameraPosition = cameraTransform.position;
    }
}
