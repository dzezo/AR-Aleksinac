using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Skripta ide na back dugme na prefabu
public class RemovePrefab : MonoBehaviour
{
    // Tag panela sa kojeg je instanciran prefab
    public string creatorTag;

    // Tag prefaba
    public string prefabTag;

    private GameObject creatorPanel;
    private GameObject prefabPanel;
    private Button btn;

    // Inicijalizacija
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(DestroyPrefab);
        prefabPanel = GameObject.FindWithTag(prefabTag);
        creatorPanel = GameObject.FindWithTag(creatorTag);
        creatorPanel.SetActive(false);
    }

    // Uklanja prefab sa scene
    private void DestroyPrefab()
    {
        creatorPanel.SetActive(true);
        Destroy(prefabPanel);
    }
}