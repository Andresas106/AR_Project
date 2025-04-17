using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColliderSemilla : MonoBehaviour
{
    [Header("Configuración")]
    public Vector3 offset = new Vector3(0, 0, 0.5f);
    public float followSmoothTime = 0.1f;
    public float respawnDelay = 3f;

    private Rigidbody rb;
    private bool hasCollided = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Transform cameraTransform;
    private bool followCamera = true;
    private Vector3 currentVelocity = Vector3.zero;
    private TrailRenderer trail;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;

        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        cameraTransform = Camera.main.transform;

        if (grabInteractable != null)
        {
            grabInteractable.enabled = true;
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnDrop);
        }

        followCamera = true;
    }

    void Update()
    {
        if (followCamera)
        {
            Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * offset.z;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmoothTime);
            transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;

        if (collision.gameObject.CompareTag("Tierra"))
        {
            Debug.Log("¡Semilla plantada en la tierra!");
            Destroy(collision.gameObject);
        }

        hasCollided = true;
        followCamera = false;

        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }

        Invoke("RecycleSeed", respawnDelay);
    }

    void RecycleSeed()
    {
        if (trail != null)
            trail.enabled = false;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        followCamera = true; // vuelve a seguir a la cámara
        Invoke("EnableSeed", 0.1f);
    }

    void EnableSeed()
    {
        rb.useGravity = false;
        hasCollided = false;

        if (grabInteractable != null)
        {
            grabInteractable.enabled = true;
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        followCamera = false;
        rb.useGravity = true;

        if (trail != null)
        {
            trail.Clear(); // Limpia la estela anterior
            trail.enabled = true; // Actívala al lanzar
        }
    }

    void OnDrop(SelectExitEventArgs args)
    {
        // Por si quieres hacer algo cuando se suelta
    }
}
