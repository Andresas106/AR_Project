using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CircularRaycast : MonoBehaviour
{
    [Header("Configuración")]
    public float maxDistance = 10f;    // Distancia máxima del Raycast
    public LayerMask targetLayers;     // Capas a detectar

    public Slider Luz;

    public float velocidadLlenado = 5f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Lanza un Raycast circular desde el centro de la cámara
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, targetLayers);

        if (hasHit)
        {
            Debug.Log("Objeto detectado: " + hitInfo.collider.name);
            // Aquí puedes añadir acciones (ej: activar un evento, destruir el objeto, etc.)
            LlenarBarra(Luz);
        }
    }

    // Visualización en el Editor (opcional)
    void OnDrawGizmos()
    {
        if (cam != null)
        {
            Gizmos.color = Color.cyan;
            Vector3 endPoint = cam.transform.position + cam.transform.forward * maxDistance;
            Gizmos.DrawLine(cam.transform.position, endPoint);
        }
    }

    void LlenarBarra(Slider barra)
    {
        barra.value += velocidadLlenado * Time.deltaTime;
        if (barra.value >= barra.maxValue)
        {
            barra.value = 0;
            Debug.Log("¡Barra llena!");
        }
    }
}