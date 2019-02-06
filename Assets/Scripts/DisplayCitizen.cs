using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ova skripta ide na dugme koje vodi do profila
public class DisplayCitizen : MonoBehaviour
{
    // Znameniti Aleksincanin
    public GameObject citizenPrefab;

    // Panel na kome se instancira Aleksincanin
    private GameObject citizenParent;

    private Button btn;

    // Inicijalizacija
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(InstantiateCitizen);
        citizenParent = GameObject.FindWithTag("MasterCanvas");
    }

    // Postavlja znamenitog Aleksincanina na scenu kada se klikne dugme
    private void InstantiateCitizen()
    {
        Instantiate(citizenPrefab, citizenParent.transform, false);
    }
}
