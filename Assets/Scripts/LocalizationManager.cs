using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    // Singleton
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;

    private string missingTextString = "Text Not Found";

    private bool isReady = false;

    // Funkcija koja osigurava da u sistemu postoji samo jedna instanca LocalizedManager objekata
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Funkcija koja ucitava lokalizovani tekst iz JSON fajla, vrsi deserializaciju u LocalizationData objekat i popunjava recnik
    // Ukoliko se sve uspesno zavrsi signalizira se da je applikacija spremna za upotrebu
    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            isReady = true;
            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries.");
        }
        else
        {
            Debug.Log("Cannot find file!");
        }
    }

    // Funkcija za citanje recnika koja vraca tekst mapiran na prosledjeni kljuc
    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
            result = localizedText[key];
        return result;
    }

    // Funkcija kojom se ispituje da li je aplikacija spremna za upotrebu
    public bool GetIsReady()
    {
        return isReady;
    }
}
