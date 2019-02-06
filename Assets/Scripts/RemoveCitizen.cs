using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Skripta ide na back dugme na profilu
public class RemoveCitizen : MonoBehaviour
{
    // Panel sa profilom
    private GameObject citizenProfile;

    // Panel na kome se nalazi lista znamenitih Aleksincana
    private GameObject citizenList;

    private Button btn;

    // Inicijalizacija
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(DestroyCitizen);
        citizenProfile = GameObject.FindWithTag("Citizen");
        citizenList = GameObject.FindWithTag("CitizenList");
        citizenList.SetActive(false);
    }

    // Uklanja Aleksincanina sa scene
    private void DestroyCitizen()
    {
        citizenList.SetActive(true);
        Destroy(citizenProfile);
    }
}
