using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class LevelSystem : MonoBehaviour
{
    public ObjectSpawner ObjectSpawner;

    [Header("Sliders")]
    public Slider Agua;
    public Slider Luz;
    public Slider Temperatura;

    [Header("Configuraci�n")]
    public float maxLevelValue = 100f;
    public int currentLevel = 1;

    void Update()
    {
        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        // Verifica si los 3 sliders est�n al m�ximo
        if (Agua.value >= maxLevelValue &&
            Luz.value >= maxLevelValue &&
            Temperatura.value >= maxLevelValue)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;
        Debug.Log("�Subiste al nivel " + currentLevel + "!");

        // Reinicia los sliders (opcional)
        Agua.value = 0;
        Luz.value = 0;
        Temperatura.value = 0;
        ObjectSpawner.Index = +1;

        // Aqu� puedes a�adir efectos visuales/sonidos
    }
}