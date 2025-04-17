using UnityEngine;

public class ColliderSemilla : MonoBehaviour
{
    [Header("Configuraci�n")]
    public Vector3 respawnPosition = new Vector3(0, 0, 0.5f); // Posici�n relativa a la c�mara
    public float respawnDelay = 3f; // Tiempo antes de reaparecer

    private Rigidbody rb;
    private bool hasCollided = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Guarda la posici�n inicial al spawnear (opcional, si no es est�tica)
        if (respawnPosition == Vector3.zero)
        {
            respawnPosition = transform.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return; // Evita m�ltiples colisiones

        if (collision.gameObject.CompareTag("Tierra"))
        {
            Debug.Log("�Semilla plantada en la tierra!");
            Destroy(collision.gameObject); // Opcional: destruye la tierra
        }

        hasCollided = true;
        Invoke("RecycleSeed", 3f); // Recicla la semilla
    }

    void RecycleSeed()
    {
        // Desactiva f�sicas y resetea velocidad
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Vuelve a la posici�n inicial (opcional: con suavizado)
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