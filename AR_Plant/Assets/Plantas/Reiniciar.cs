using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void RestartCurrentScene()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Alternativa usando el índice (más eficiente):
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}