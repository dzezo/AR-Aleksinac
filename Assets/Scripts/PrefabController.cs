using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Skripta se postavlja na prefab-ove koji se instanciraju na glavnom meniju
// i sadrzi sve funkcionalnosti koje UI element glavnog menija nudi
public class PrefabController : MonoBehaviour
{
    // Canvas glavnog menija
    private GameObject menuCanvas;

    // Inicijalizacija
    void Start()
    {
        menuCanvas = GameObject.FindWithTag("MasterCanvas");
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

    // Vraca jedan panel unazad
    public void SwitchBack()
    {
        // Ukoliko se korisnik vratio do pocetnog panela pokreni f-ju za iskljucivanje aplikacije
        if (ApplicationManager.instance.GetPanelsCount() == 1)
        {
            QuitApp();
            return;
        }
        // Trenutni panel se uklanja sa steka panela koje je korisnik obisao
        ApplicationManager.instance.RemoveLastPanel();
        // Na scenu se postavlja panel koji je korisnik obisao pre trenutnog
        GameObject newActivePanel = ApplicationManager.instance.GetLastPanel();
        Instantiate(newActivePanel, menuCanvas.transform, false);
        // Panel sa koga se vracamo se uklanja sa scene
        Destroy(gameObject);
    }

    // Menja trenutno aktivni panel za panel u argumentu
    public void SwitchPanel(GameObject newActivePanel)
    {
        // Postavi se novi panel i doda se kao poslednji panel koji je korisnik obisao
        Instantiate(newActivePanel, menuCanvas.transform, false);
        ApplicationManager.instance.SetLastPanel(newActivePanel);
        // Sa scene se uklanja panel koji je pozvao ovu f-ju
        Destroy(gameObject);
    }

    // Funkcija koja postavlja scenu za menjanje jezika
    public void ChangeLang()
    {
        // Resetuje se spremnost recnika i ucitava se scena
        LocalizationManager.instance.ResetIsReady();
        SceneManager.LoadScene("LanguageScreen");
    }

    // Funkcija koja ukljucuje AR kameru
    public void StartCam()
    {
        SceneManager.LoadScene("CameraScreen");
    }

    // Iskljucuje aplikaciju
    private void QuitApp()
    {
        Debug.Log("Quiting application...");
        Application.Quit();
    }

}
