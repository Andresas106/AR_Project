using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarCamera : MonoBehaviour
{
    private Transform camTransform;

    void Start()
    {
        // Asegura que usamos la ARCamera
        if (Camera.main != null)
        {
            camTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("No se ha encontrado Camera.main. Asegúrate de que tu ARCamera tenga el tag 'MainCamera'.");
        }
    }

    void Update()
    {
        if (camTransform == null && Camera.main != null)
        {
            camTransform = Camera.main.transform;
        }

        if (camTransform != null)
        {
            transform.LookAt(camTransform);

            // Opcional: bloquear el eje X y Z para que el virus no se incline
            Vector3 rot = transform.eulerAngles;
            rot.x = 0f;
            rot.z = 0f;
            transform.eulerAngles = rot;
        }
    }
}
