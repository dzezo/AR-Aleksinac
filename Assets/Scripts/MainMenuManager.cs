using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Canvas glavnog menija
    public GameObject menuCanvas;

    // Panel sa kojim pocinje glavni meni
    public GameObject rootPanel;

    // Tagovi za sve prefabove
    public string[] prefabTags;


    // Postavlja poslednji aktivni panel
    void Start()
    {
        GameObject lastActivePanel = ApplicationManager.instance.GetLastPanel();
        // ukoliko je stack prazan onda se ukljucuje pocetni panel (root) i dodaje se na stack
        if (lastActivePanel == null)
        {
            ApplicationManager.instance.SetLastPanel(rootPanel);
            rootPanel.SetActive(true);
        }
        else
        {
            // U suprotnom se postavlja poslednji aktivni panel
            SetNewActivePanel(lastActivePanel);
        }   
    }

    // Slusanje inputa za back/exit dugme
    void Update()
    {
        // Input Esc predstavlja strelicu unazad na Androidu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchBack();
        }
    }

    // Menja trenutno aktivni panel za panel u argumentu
    public void SwitchPanel(GameObject newActivePanel)
    {
        // Iskljuci se stari panel
        GameObject activePanel = ApplicationManager.instance.GetLastPanel();
        activePanel.SetActive(false);
        // Postavi se novi panel i doda se kao poslednji panel koji je korisnik obisao
        ApplicationManager.instance.SetLastPanel(SetNewActivePanel(newActivePanel));
    }

    // Vraca jedan panel unazad
    public void SwitchBack()
    {
        // Ukoliko se korisnik vratio do pocetnog panel pokreni f-ju za iskljucivanje aplikacije
        if (ApplicationManager.instance.GetLastPanel().Equals(rootPanel))
        {
            QuitApp();
            return;
        }

        // Trenutno aktivni panel se brise ukoliko je prefab ili se iskljucuje ukoliko je clan menija
        GameObject activePanel = ApplicationManager.instance.RemoveLastPanel();
        if (IsPrefab(activePanel))
        {
            Destroy(activePanel);
        }
        else
        {
            activePanel.SetActive(false);
        }
        // Na scenu se postavlja panel koji je korisnik obisao pre trenutnog
        GameObject newActivePanel = ApplicationManager.instance.GetLastPanel();
        newActivePanel.SetActive(true);
    }

    // Iskljucuje aplikaciju
    private void QuitApp()
    {
        Debug.Log("Quiting application...");
        Application.Quit();
    }

    // Funkcija koja ukljucuje AR kameru
    public void StartCam()
    {
        SceneManager.LoadScene("CameraScreen");
    }

    // Funkcija koja postavlja scenu za menjanje jezika
    public void ChangeLang()
    {
        // Resetuje se spremnost recnika i ucitava se scena
        LocalizationManager.instance.ResetIsReady();
        SceneManager.LoadScene("LanguageScreen");
    }

    // Funkcija koja odredjuje da li je prosledjeni panel prefab ili stalni clan menija
    private bool IsPrefab(GameObject panel)
    {
        bool isPrefab = false;
        foreach (string tag in prefabTags)
        {
            if (string.Equals(panel.tag, tag))
            {
                isPrefab = true;
                break;
            }
        }
        return isPrefab;
    }

    private GameObject SetNewActivePanel(GameObject newActivePanel)
    {
        // Ukoliko je prefab mora da se instancira, u suprotnom samo se aktivira
        if (IsPrefab(newActivePanel))
        {
            GameObject panelClone = Instantiate(newActivePanel, menuCanvas.transform, false);
            return panelClone;
        }
        else
        {
            newActivePanel.SetActive(true);
            return newActivePanel;
        }
    }
}
