using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    // Iskljucuje aplikaciju, izlaz koji se nalazi na MainMenu panelu
    public void QuitApp()
    {
        Application.Quit();
    }

    // Funkcija koja ukljucuje AR kameru
    public void StartCam()
    {
        SceneManager.LoadScene("AugmentedReality");
    }

    // Iskljucuje AR kameru, vraca se na menuScreen scenu
    public void QuitCam()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    // Funkcija koja postavlja scenu za menjanje jezika
    public void ChangeLang()
    {
        LocalizationManager.instance.ResetIsReady();
        SceneManager.LoadScene("LanguageScreen");
    }
}
