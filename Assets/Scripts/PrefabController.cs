using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skripta se postavlja na prefab-ove koji se instanciraju na glavnom meniju
// Sluzi da poveze meni menadzer sa prefabom kako bi prefab mogao da koristi funkcionalnosti menadzera
public class PrefabController : MonoBehaviour
{
    private MainMenuManager menuManager;

    // Inicijalizacija
    void Start()
    {
        GameObject managerObj = GameObject.FindWithTag("MenuManager");
        menuManager = managerObj.GetComponent<MainMenuManager>();
    }

    public void SwitchBack()
    {
        menuManager.SwitchBack();
    }

    public void SwitchPanel(GameObject newActivePanel)
    {
        menuManager.SwitchPanel(newActivePanel);
    }

    public void ChangeLang()
    {
        menuManager.ChangeLang();
    }

}
