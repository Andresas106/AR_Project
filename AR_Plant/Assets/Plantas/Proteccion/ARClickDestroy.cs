using UnityEngine;
using UnityEngine.InputSystem;


public class ARClickDestroy : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;

    void Update()
    {
        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // Verifica si el objeto tiene la etiqueta "Virus"
            if (hit.collider != null && hit.collider.CompareTag("Bicho"))
            {
                // Destruye el objeto
                Destroy(hit.collider.gameObject);
            }
        }
    }
}

/*
 
 var isPointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(-1);
                if (!isPointerOverUI && m_ARInteractor.TryGetCurrentARRaycastHit(out var arRaycastHit))
                {
                    if (!(arRaycastHit.trackable is ARPlane arPlane))
                        return;

                    if (m_RequireHorizontalUpSurface && arPlane.alignment != PlaneAlignment.HorizontalUp)
                        return;

                    m_ObjectSpawner.TrySpawnObject(arRaycastHit.pose.position, arPlane.normal);
                }
 
 */
