using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Canvas glavnog menija
    public GameObject menuCanvas;

    // Postavlja odgovarajuci panel tokom inicijalizacije scene
    void Start()
    {
        // Ukoliko je stack prazan (prvo ucitavanje menija), onda se postavlja panel za izbor jezika 
        if (ApplicationManager.instance.GetPanelsCount() == 0)
        {
            Instantiate(ApplicationManager.instance.languagePanel, menuCanvas.transform, false);
        }
        // U suprotnom se postavlja poslednji aktivni panel
        else
        {
            GameObject lastActivePanel = ApplicationManager.instance.GetLastPanel();
            Instantiate(lastActivePanel, menuCanvas.transform, false);
        }
    }
    
}
