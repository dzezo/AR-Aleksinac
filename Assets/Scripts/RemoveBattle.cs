using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Skripta za back dugme kod panela za bitku
public class RemoveBattle : MonoBehaviour
{
    // Panel sa bitkom
    private GameObject battlePanel;

    // Panel sa bitkama
    private GameObject battlefieldPanel;

    private Button btn;

    // Inicijalizacija
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(DestroyCitizen);
        battlePanel = GameObject.FindWithTag("Battle");
        battlefieldPanel = GameObject.FindWithTag("Battlefield");
        battlefieldPanel.SetActive(false);
    }

    // Uklanja bitku sa scene i vraca se na bitke
    private void DestroyCitizen()
    {
        battlefieldPanel.SetActive(true);
        Destroy(battlePanel);
    }
}
