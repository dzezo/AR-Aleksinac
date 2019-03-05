using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skripta se postavlja na panel za izbor jezika
public class LoadLocalizedText : MonoBehaviour
{
    // Canvas glavnog menija
    private GameObject menuCanvas;

    // Elementi panela koji se prikazuju pri prvom koriscenju
    [Tooltip("Elementi koji se prikazuju pri prvom koriscenju")]
    public GameObject[] defaultElements;

    // Elementi koji se prikazuju kada se na panel vrati iz menija
    [Tooltip("Elementi koji se prikazuju kada se dodje preko menija")]
    public GameObject[] elements;

    private void Start()
    {
        menuCanvas = GameObject.FindWithTag("MasterCanvas");

        // Ukoliko stack nije prazan (jezik se bira iz menija) prikazi odgovarajuce elemente
        if (ApplicationManager.instance.GetPanelsCount() != 0)
        {
            // Broj elemenata u oba niza mora biti jednak
            for (int i = 0; i < defaultElements.Length; i++)
            {
                defaultElements[i].SetActive(false);
                elements[i].SetActive(true);
            }
        }
    }

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
        SwitchPanel();
        // Ukloni panel za jezike sa scene
        Destroy(gameObject);
    }

    private void SwitchPanel()
    {
        // Ukoliko je stack prazan (jezik se bira prvi put), onda se postavlja podrazumevani panel glavnog menija
        if (ApplicationManager.instance.GetPanelsCount() == 0)
        {
            ApplicationManager.instance.SetLastPanel(ApplicationManager.instance.menuDefaultPanel);
            Instantiate(ApplicationManager.instance.menuDefaultPanel, menuCanvas.transform, false);
        }
        // U suprotnom se postavlja poslednji aktivni panel
        else
        {
            // Panel za jezike se uklanja sa steka panela koje je korisnik obisao
            ApplicationManager.instance.RemoveLastPanel();
            // Na scenu se postavlja panel koji je korisnik obisao pre trenutnog
            GameObject lastActivePanel = ApplicationManager.instance.GetLastPanel();
            Instantiate(lastActivePanel, menuCanvas.transform, false);
        }
    }

}
