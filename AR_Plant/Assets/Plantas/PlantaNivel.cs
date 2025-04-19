using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class LevelSystem : MonoBehaviour
{
    public ObjectSpawner ObjectSpawner;
    public GameObject MensajeEndGame;

    [Header("Sliders")]
    public Slider Agua;
    public Slider Luz;
    public Slider Temperatura;

    [Header("Configuración")]
    public float maxLevelValue = 100f;
    public int currentLevel = 1;

    void Update()
    {
        CheckLevelUp();
        EndGame();
    }

    void CheckLevelUp()
    {
        // Verifica si los 3 sliders están al máximo
        if (Agua.value >= maxLevelValue && Luz.value >= maxLevelValue && Temperatura.value >= maxLevelValue)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;

        // Reinicia los sliders (opcional)
        Agua.value = 0;
        Luz.value = 0;
        Temperatura.value = 0;
        ObjectSpawner.spawnOptionIndex = ObjectSpawner.spawnOptionIndex + 1;

        // Cambiar el prefab visual al instante
        Vector3 spawnPoint = ObjectSpawner.m_CurrentSpawnedObject.transform.position;
        Vector3 spawnNormal = Vector3.up; // Usa la normal que estés usando para alinear el objeto

        ObjectSpawner.ForceRespawnObject(spawnPoint, spawnNormal);

        // Aquí puedes añadir efectos visuales/sonidos
    }

    void EndGame()
    {
        if(currentLevel > 4)
        {
            MensajeEndGame.SetActive(true);
        }
    }
}