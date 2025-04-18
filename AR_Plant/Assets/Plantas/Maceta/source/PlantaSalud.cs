using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Añade este namespace

public class PlantaSalud : MonoBehaviour
{
    [Header("Barras UI")]
    public Slider Agua;
    public Slider Luz;
    public Slider Temp;

    [Header("Velocidad de llenado")]
    public float velocidadLlenado = 5f;

    private Keyboard keyboard; // Referencia al teclado

    void Start()
    {
        keyboard = Keyboard.current; // Inicializa el teclado
    }

    void Update()
    {
        // Usa Keyboard.current en lugar de Input.GetKey
        if (keyboard.aKey.isPressed)
        {
            LlenarBarra(Agua);
        }

        if (keyboard.bKey.isPressed)
        {
            LlenarBarra(Luz);
        }

        // Para el mouse, usa Mouse.current
        if (Mouse.current.delta.x.ReadValue() != 0)
        {
            LlenarBarra(Temp);
        }
    }

    void LlenarBarra(Slider barra)
    {
        barra.value += velocidadLlenado * Time.deltaTime;
        if (barra.value >= barra.maxValue)
        {
            barra.value = 0;
            Debug.Log("¡Barra llena!");
        }
    }
}