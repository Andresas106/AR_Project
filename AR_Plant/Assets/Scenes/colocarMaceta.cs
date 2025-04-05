using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class colocarMaceta : MonoBehaviour
{
    public GameObject macetaPrefab;
    private GameObject macetaInstanciada;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Salir si ya se colocó una maceta o no hay toque en pantalla
        if (macetaInstanciada != null || Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        // Raycast desde la posición del toque para detectar planos
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            ARPlane plane = hits[0].trackable as ARPlane;

            // Asegurarse de que sea un plano horizontal hacia arriba (suelo)
            if (plane != null && plane.alignment == PlaneAlignment.HorizontalUp)
            {
                Debug.Log("Suelo detectado. Colocando maceta...");
                macetaInstanciada = Instantiate(macetaPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                Debug.Log("Plano detectado, pero no es el suelo.");
            }
        }
        else
        {
            Debug.Log("No se detectó ningún plano.");
        }
    }
}
