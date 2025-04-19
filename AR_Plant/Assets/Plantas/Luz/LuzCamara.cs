using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CircularRaycast : MonoBehaviour
{
    [Header("Configuración")]
    public float maxDistance = 10f;    // Distancia máxima del Raycast
    public LayerMask targetLayers;     // Capas a detectar

    public Slider Luz;

    public float velocidadLlenado = 5f;

    private Camera cam;

    private GameObject maceta;
    private GameObject particulasLuz;
    public ObjectSpawner os;

    void Start()
    {
       
        
        cam = Camera.main;
    }

    void Update()
    {
        // Lanza un Raycast circular desde el centro de la cámara
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, targetLayers);

        if (os.spawnOptionIndex == 0)
        {
            maceta = GameObject.Find("Maceta(Clone)");
        } else if(os.spawnOptionIndex == 1)
        {
            maceta = GameObject.Find("MacetaNivel1(Clone)");
        } else if(os.spawnOptionIndex == 2)
        {
            maceta = GameObject.Find("MacetaNivel2(Clone)");
        } else if(os.spawnOptionIndex == 3)
        {
            maceta = GameObject.Find("MacetaNivel3(Clone)");
        }

        
        if (maceta != null)
        {
            particulasLuz = maceta.transform.Find("ParticulasLuz")?.gameObject;
        }

        if (hasHit)
        {

            // Aquí puedes añadir acciones (ej: activar un evento, destruir el objeto, etc.)
            if (particulasLuz != null) {
                particulasLuz.SetActive(true);
            }
            
            LlenarBarra(Luz);
        }
        else
        {
            if (particulasLuz != null)
            {
                particulasLuz.SetActive(false);
            }
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
            if (particulasLuz != null)
            {
                particulasLuz.SetActive(false);
            }
        }
    }
}