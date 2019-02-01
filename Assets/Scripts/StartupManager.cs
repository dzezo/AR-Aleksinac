using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ceka da aplikacija bude spremna pre nego sto je ucita
public class StartupManager : MonoBehaviour
{

    // Odlaze ucitavanje za sledeci frejm sve dokle god je recnik prazan
    private IEnumerator Start()
    {
        while (!LocalizationManager.instance.GetIsReady())
            yield return null;
        SceneManager.LoadScene("MenuScreen");
    }
}
