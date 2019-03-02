using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skripta se postavlja na panel za izbor jezika, 
// a na dugmetu se postavlja OnClick actionListener koji poziva LoadText f-ju
public class LoadLocalizedText : MonoBehaviour
{
    // Bira metodu za ucitavanje lokalizovanog teksta prema platformi na kojoj se aplikacija izvrsava
    public void LoadText(string fileName)
    {
        // Ucitaj tekst
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            LocalizationManager.instance.LoadLocalizedText(fileName);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            LocalizationManager.instance.StartCoroutine("LoadLocalizedTextOnAndroid", fileName);
        }
        // Ucitaj meni
        StartCoroutine("LoadMenu");
    }

    // Ucitava odgovarajuci panel glavnog menija i uklanja panel za selekciju jezika sa scene
    private IEnumerator LoadMenu()
    {
        // Sacekaj da se recnik popuni
        while (!LocalizationManager.instance.GetIsReady())
            yield return null;
        // Postavi odgovarajuci panel
        // Ukoliko je stack prazan, onda se postavlja podrazumevani (pocetni) meni panel i dodaje se na stack
        if (ApplicationManager.instance.GetPanelsCount() == 0)
        {
            ApplicationManager.instance.SetLastPanel(ApplicationManager.instance.menuDefaultPanel);
            Instantiate(ApplicationManager.instance.menuDefaultPanel, ApplicationManager.instance.menuCanvas.transform, false);
        }
        // U suprotnom se postavlja poslednji aktivni panel
        else
        {
            // Panel za jezike se uklanja sa steka panela koje je korisnik obisao
            ApplicationManager.instance.RemoveLastPanel();
            // Na scenu se postavlja panel koji je korisnik obisao pre trenutnog
            GameObject lastActivePanel = ApplicationManager.instance.GetLastPanel();
            Instantiate(lastActivePanel, ApplicationManager.instance.menuCanvas.transform, false);
        }
        // Ukloni panel za jezike sa scene
        Destroy(gameObject);
    }

}
