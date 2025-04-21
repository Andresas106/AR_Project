using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using System.Collections;

public class RestartScene : MonoBehaviour
{

    public void ReiniciarScene()
    {
        Application.Quit();
    }
}