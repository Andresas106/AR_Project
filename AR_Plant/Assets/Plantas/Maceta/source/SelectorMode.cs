using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SelectorMode : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Toggles de Modo")]
    public Toggle toggleRiego;
    public Toggle toggleLuz;
    public Toggle toggleTemp;

    [Header("Grupo de Toggles")]
    public ToggleGroup toggleGroup;

    [Header("Barras UI")]
    public Slider Agua;
    public Slider Luz;
    public Slider Temp;

    public enum ModoAccion {Riego, Luz, Temperatura }
    public ModoAccion modoActivo;

    void Start()
    {
        // Asegura que los toggles estén en el grupo
        toggleRiego.group = toggleGroup;
        toggleLuz.group = toggleGroup;
        toggleTemp.group = toggleGroup;

        // Forzar modo riego por defecto
        toggleRiego.isOn = true;
        toggleLuz.isOn = false;
        toggleTemp.isOn = false;

        modoActivo = ModoAccion.Riego;

        // Listeners para cambios de selección
        toggleRiego.onValueChanged.AddListener((isOn) => {
            if (isOn) modoActivo = ModoAccion.Riego;
        });

        toggleLuz.onValueChanged.AddListener((isOn) => {
            if (isOn) modoActivo = ModoAccion.Luz;
        });

        toggleTemp.onValueChanged.AddListener((isOn) => {
            if (isOn) modoActivo = ModoAccion.Temperatura;
        });
    }

    void Update()
    {
        // Aquí puedes hacer cosas dependiendo del modo activo
        // Por ejemplo:
        switch (modoActivo)
        {
            case ModoAccion.Riego:
                Debug.Log("Regamos");
                
                
                break;
            case ModoAccion.Luz:
                // Ejecuta lógica de luz
                Debug.Log("Luces");
                break;
            case ModoAccion.Temperatura:
                // Ejecuta lógica de temperatura
                Debug.Log("Temperatura");
                break;
        }
    }
}
