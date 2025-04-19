using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class RestartScene : MonoBehaviour
{
    private ARSession _arSession;

    void Start()
    {
        // Cachear referencia al ARSession al inicio
        _arSession = FindObjectOfType<ARSession>();
    }

    public void RestartCurrentScene()
    {
        // 1. Detener el ARSession antes de reiniciar
        if (_arSession != null)
        {
            _arSession.Reset();
            Debug.Log("ARSession reseteado");
        }

        // 2. Reiniciar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        // 3. Opcional: Forzar recolección de basura
        System.GC.Collect();
    }
}