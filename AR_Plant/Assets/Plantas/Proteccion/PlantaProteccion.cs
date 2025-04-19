using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlantaProteccion : MonoBehaviour
{

    public GameObject virusPrefab;
    public int cantidad = 5; // Número de objetos alrededor
    public float radio = 0.5f; // Radio alrededor de la maceta


    private List<GameObject> protecciones = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerarAlrededor(GameObject maceta)
    {
        if(maceta != null)
        {
            Vector3 centro = maceta.transform.position;
            float distanciaMinima = 0.3f; // Distancia mínima entre virus
            for (int i = 0; i < cantidad; i++)
            {
                bool colocado = false;

                while (!colocado)
                {
                    Vector2 pos2D = Random.insideUnitCircle * radio;
                    float alturaY = Random.Range(0f, 0.5f);

                    Vector3 posicion = new Vector3(
                        centro.x + pos2D.x,
                        centro.y + alturaY,
                        centro.z + pos2D.y
                    );

                    // Verificar distancia con virus ya colocados
                    bool muyCerca = false;
                    foreach (var existente in protecciones)
                    {
                        if (Vector3.Distance(posicion, existente.transform.position) < distanciaMinima)
                        {
                            muyCerca = true;
                            break;
                        }
                    }

                    if (!muyCerca)
                    {
                        GameObject virus = Instantiate(virusPrefab, posicion, Quaternion.identity);
                        protecciones.Add(virus);
                        colocado = true;
                    }
                }
            }
        }
    }

    public void DestruirProtecciones()
    {
        foreach (var go in protecciones)
        {
            if (go != null)
                Destroy(go);
        }
        protecciones.Clear();
    }
}
