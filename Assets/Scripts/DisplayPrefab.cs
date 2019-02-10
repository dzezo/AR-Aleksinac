using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Skripta ide na dugme koje treba da instancira prefab nakon klika
public class DisplayPrefab : MonoBehaviour
{
    // Prefab koji se instancira se postavlja u inspektoru
    public GameObject prefab;

    // Panel na kome se instancira prefab
    private GameObject prefabParent;

    private Button btn;

    // Inicijalizacija
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(InstantiatePrefab);
        prefabParent = GameObject.FindWithTag("MasterCanvas");
    }

    // OnClick metoda za instanciranje
    private void InstantiatePrefab()
    {
        Instantiate(prefab, prefabParent.transform, false);
    }
}
