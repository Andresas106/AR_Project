using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using System.Collections;

public class RestartScene : MonoBehaviour
{

    public void ReiniciarScene()
    {
        StartCoroutine(RestartCurrentScene());
    }

    private IEnumerator RestartCurrentScene()
    {
        // 1. Detener el ARSession antes de reiniciar
        // Detener y desinicializar el sistema XR
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();

        // Esperar un frame para asegurar que el sistema XR se ha detenido completamente
        yield return null;

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}