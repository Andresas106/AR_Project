using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AguaInteraccion : MonoBehaviour
{
    private Transform cameraTransform;
    public Vector3 offset = new Vector3(0, -0.3f, 0.5f); // Ajusta el valor Y para bajar el objeto
    private Vector3 currentVelocity = Vector3.zero;
    public float followSmoothTime = 0.1f;
    private bool followCamera = true;
    public Slider Agua;

    [Header("Configuración")]
    public float valueIncrement = 0.1f; // Cuánto aumenta el valor por partícula
    public float currentValue = 0f; // Valor actual (puede ser vida, progreso, etc.)
    public LayerMask plantaLayer; // Asigna la capa "Planta" en el Inspector

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (followCamera)
        {
            // Posición objetivo: cámara + offset (incluye el ajuste vertical en Y)
            Vector3 targetPosition = cameraTransform.position
                                  + cameraTransform.forward * offset.z
                                  + cameraTransform.up * offset.y; // Importante: usa cameraTransform.up para Y

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref currentVelocity,
                followSmoothTime
            );

            // Rotación para mirar hacia adelante (igual que antes)
            transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        // Verifica si el objeto colisionado está en la capa "Planta"
        if (((1 << other.layer) & plantaLayer) != 0)
        {

            // Actualiza el Slider
            if (Agua != null)
            {
                Agua.value += valueIncrement;
            }

        }
    }

}