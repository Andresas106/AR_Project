using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SelectorMode : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Toggles de Modo")]
    public Toggle toggleRiego;
    public Toggle toggleLuz;
    public Toggle toggleTemp;

    [Header("Grupo de Toggles")]
    public ToggleGroup toggleGroup;

    [Header("Interaccion")]
    public GameObject AguaInteraccion;
    public GameObject LuzInteraccion;
    public GameObject TempInteraccion;

    public enum ModoAccion {Riego, Luz, Temperatura, Nada }
    public ModoAccion modoActivo;

    [Header("Maceta")]
    private GameObject maceta;
    private GameObject particulasLuz;
    public ObjectSpawner os;
    public PlantaProteccion pp;

    private bool generadoProteccion = false;

    void Start()
    {
        // Asegura que los toggles est�n en el grupo
        toggleRiego.group = toggleGroup;
        toggleLuz.group = toggleGroup;
        toggleTemp.group = toggleGroup;

        // Forzar modo riego por defecto
        toggleRiego.isOn = false;
        toggleLuz.isOn = false;
        toggleTemp.isOn = false;

        modoActivo = ModoAccion.Nada;

        // Listeners para cambios de selecci�n
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

        if (os.spawnOptionIndex == 0)
        {
            maceta = GameObject.Find("Maceta(Clone)");
        }
        else if (os.spawnOptionIndex == 1)
        {
            maceta = GameObject.Find("MacetaNivel1(Clone)");
        }
        else if (os.spawnOptionIndex == 2)
        {
            maceta = GameObject.Find("MacetaNivel2(Clone)");
        }
        else if (os.spawnOptionIndex == 3)
        {
            maceta = GameObject.Find("MacetaNivel3(Clone)");
        }


        if (maceta != null)
        {
            particulasLuz = maceta.transform.Find("ParticulasLuz")?.gameObject;
        }
        else
        {
            toggleRiego.isOn = false;
            toggleLuz.isOn = false;
            toggleTemp.isOn = false;
            modoActivo = ModoAccion.Nada;
        }
        // Aqu� puedes hacer cosas dependiendo del modo activo
        // Por ejemplo:
        switch (modoActivo)
        {
            case ModoAccion.Riego:
                AguaInteraccion.SetActive(true);
                LuzInteraccion.SetActive(false);
                TempInteraccion.SetActive(false);

                if(particulasLuz != null)
                {
                    particulasLuz.SetActive(false);
                }

                pp.DestruirProtecciones();
                generadoProteccion = false;

                break;
            case ModoAccion.Luz:

                AguaInteraccion.SetActive(false);
                LuzInteraccion.SetActive(true);
                TempInteraccion.SetActive(false);

                pp.DestruirProtecciones();
                generadoProteccion = false;

                break;
            case ModoAccion.Temperatura:

                AguaInteraccion.SetActive(false);
                LuzInteraccion.SetActive(false);
                TempInteraccion.SetActive(true);



                if (particulasLuz != null)
                {
                    particulasLuz.SetActive(false);
                }

                if (!generadoProteccion && maceta != null)
                {
                    pp.GenerarAlrededor(maceta);
                    generadoProteccion = true;
                }

                break;
            default:

                DesactivarTodo();
                break;
        }
    }

    public void DesactivarTodo()
    {
        AguaInteraccion.SetActive(false);
        LuzInteraccion.SetActive(false);
        TempInteraccion.SetActive(false);

        pp.DestruirProtecciones();
        generadoProteccion = false;
    }
}
