using UnityEngine;

public class ColliderSemilla : MonoBehaviour
{
    [Header("Configuración")]
    public Vector3 respawnPosition = new Vector3(0, 0, 0.5f); // Posición relativa a la cámara
    public float respawnDelay = 3f; // Tiempo antes de reaparecer

    private Rigidbody rb;
    private bool hasCollided = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Guarda la posición inicial al spawnear (opcional, si no es estática)
        if (respawnPosition == Vector3.zero)
        {
            respawnPosition = transform.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return; // Evita múltiples colisiones

        if (collision.gameObject.CompareTag("Tierra"))
        {
            Debug.Log("¡Semilla plantada en la tierra!");
            Destroy(collision.gameObject); // Opcional: destruye la tierra
        }

        hasCollided = true;
        Invoke("RecycleSeed", 3f); // Recicla la semilla
    }

    void RecycleSeed()
    {
        // Desactiva físicas y resetea velocidad
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Vuelve a la posición inicial (opcional: con suavizado)
        transform.position = Camera.main.transform.TransformPoint(respawnPosition);
        transform.rotation = Camera.main.transform.rotation;

        // Espera antes de reactivar (para evitar bugs)
        Invoke("EnableSeed", 0.1f);
    }

    void EnableSeed()
    {
        rb.useGravity = false;
        hasCollided = false;
    }
}