using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Objekat na koji je nakacena skripta mora imati Text komponentu
public class LocalizedText : MonoBehaviour
{
    // Kljuc se postavlja u inspektoru
    public string key;

    // Start is called before the first frame update
    // Funkcija popunjava tekst polje objekta tekstom iz recnika koji je mapiran na zadati kljuc
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
