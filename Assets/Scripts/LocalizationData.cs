// Sluzi za smestanje podataka iz JSON fajla za lokalizovani tekst
// Format JSON fajla za lokalizovani tekst: {"items": [{"key": "key_value", "value": "string_value"}]}

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}