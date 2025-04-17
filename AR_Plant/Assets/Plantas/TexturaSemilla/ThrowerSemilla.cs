using UnityEngine;

public class SeedController : MonoBehaviour
{
    [Header("Configuraci�n")]
    public GameObject seedPrefab;          // Prefab de la semilla (aseg�rate de que tiene Rigidbody y Collider)
    public float throwForce = 10f;        // Fuerza de lanzamiento
    public float distanceFromCamera = 0.5f; // Distancia inicial desde la c�mara
    public float smoothMovement = 0.1f;   // Suavizado al arrastrar

    private GameObject currentSeed;       // Referencia a la semilla instanciada
    private Rigidbody seedRb;             // Rigidbody de la semilla
    private bool isHoldingSeed = false;   // �Est� el usuario agarrando la semilla?
    private Vector3 velocity = Vector3.zero; // Para SmoothDamp

    void Start()
    {
        SpawnSeed(); // Crear la semilla al inicio
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // --- Detecci�n inicial del touch ---
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Si el toque golpea la semilla, la agarramos
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == currentSeed)
                {
                    isHoldingSeed = true;
                    seedRb.isKinematic = true; // Desactivamos f�sica mientras se arrastra
                }
            }

            // --- Arrastre con el dedo ---
            if (isHoldingSeed && touch.phase == TouchPhase.Moved)
            {
                // Convertir posici�n 2D del touch a 3D (en un plano delante de la c�mara)
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(
                    new Vector3(touch.position.x, touch.position.y, distanceFromCamera)
                );

                // Mover la semilla con suavizado
                currentSeed.transform.position = Vector3.SmoothDamp(
                    currentSeed.transform.position,
                    targetPosition,
                    ref velocity,
                    smoothMovement
                );
            }

            // --- Soltar y lanzar ---
            if (isHoldingSeed && touch.phase == TouchPhase.Ended)
            {
                isHoldingSeed = false;
                ThrowSeed();
            }
        }
    }

    // Crear la semilla en el centro de la pantalla
    void SpawnSeed()
    {
        if (currentSeed == null)
        {
            currentSeed = Instantiate(seedPrefab);
            seedRb = currentSeed.GetComponent<Rigidbody>();
            ResetSeedPosition();
        }
    }

    // Posici�n inicial centrada
    void ResetSeedPosition()
    {
        currentSeed.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera;
        currentSeed.transform.rotation = Camera.main.transform.rotation;
        seedRb.isKinematic = true; // F�sica desactivada al inicio
    }

    // Lanzar la semilla
    void ThrowSeed()
    {
        seedRb.isKinematic = false; // Activamos f�sica
        seedRb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);

        // Reciclar semilla despu�s de 3 segundos (opcional)
        Invoke("ResetSeed", 3f);
    }
}