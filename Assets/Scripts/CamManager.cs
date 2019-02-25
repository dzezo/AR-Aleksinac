using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamManager : MonoBehaviour
{
    void Start()
    {
        // Onemogucavanje zatamljenja ekrana
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        // Input Esc predstavlja strelicu unazad na Androidu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitCam();
        }
    }

    // Iskljucuje AR kameru, vraca se na MenuScreen scenu
    public void QuitCam()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
