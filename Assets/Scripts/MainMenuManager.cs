using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Canvas glavnog menija
    public GameObject menuCanvas;

    // Panel sa kojim pocinje glavni meni
    public GameObject menuDefaultPanel;

    // Postavlja poslednji aktivni panel
    void Start()
    {
        // ukoliko je stack prazan, onda se postavlja podrazumevani (pocetni) meni panel i dodaje se na stack
        if (ApplicationManager.instance.GetPanelsCount() == 0)
        {
            ApplicationManager.instance.SetLastPanel(menuDefaultPanel);
            Instantiate(menuDefaultPanel, menuCanvas.transform, false);
        }
        else
        {
            // U suprotnom se postavlja poslednji aktivni panel
            GameObject lastActivePanel = ApplicationManager.instance.GetLastPanel();
            Instantiate(lastActivePanel, menuCanvas.transform, false);
        }   
    }

}
