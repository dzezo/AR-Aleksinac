using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Skripta se nalazi na dugmetu za ucitavanje jezika (LanguageScreen scena)
// Definise OnClick metodu za dugme na koje je nakacena
public class LoadLocalizedText : MonoBehaviour
{
    // Ime JSON fajla iz kojeg cita
    public string fileName;

    // Definise koja se metoda izvrsava kada se klikne dugme na koje je nakacena skripta
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadText);
    }

    // Bira metodu za ucitavanje lokalizovanog teksta prema platformi na kojoj se aplikacija izvrsava
    private void LoadText()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            LocalizationManager.instance.LoadLocalizedText(fileName);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            LocalizationManager.instance.StartCoroutine("LoadLocalizedTextOnAndroid", fileName);
        }
    }

}
