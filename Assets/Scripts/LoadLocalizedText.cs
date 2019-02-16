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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadText);
    }

    private void LoadText()
    {
        LocalizationManager.instance.LoadLocalizedText(fileName);
    }
}
