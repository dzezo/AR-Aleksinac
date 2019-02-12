using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSelectionManager : MonoBehaviour
{
    // dugmici koji se nalaze na mapi
    public GameObject[] battles;

    // dugmici koji se nalaze ispod mape
    public GameObject[] infoButtons;

    // dugme ispod mape koje se prikazuje kada nista nije selektovano
    public GameObject defaultInfoButton;

    // koliko ima bitaka na mapi
    private int battleCount;

    // animatori koji se nalaze na dugmicima koji su na mapi
    private Animator[] animators;

    // Inicijalizacija
    void Start()
    {
        battleCount = battles.Length;
        animators = new Animator[battleCount];
        for (int i = 0; i < battleCount; i++)
        {
            animators[i] = battles[i].GetComponent<Animator>();
        }
    }

    // Izvrsava se kada je panel (u ratu) ponovo vidljiv, trenutno samo prikazuje podrazumevanu poruku
    // Nije potrebno menjati b_select u animatoru na false jer se to automatski radi svaki put kad se deaktivira panel
    void OnEnable()
    {
        defaultInfoButton.SetActive(true);

        for (int i = 0; i < battleCount; i++)
        {
            infoButtons[i].SetActive(false);
        }
    }

    // Funkcija koja deselektuje sve dugmice, koristi se kada se klikne bilo gde drugde na panelu
    // Panel (u ratu) ima eventTrigger na sebi koji poziva ovu funkicju ukoliko se desio klik
    public void DeselectAll()
    {
        defaultInfoButton.SetActive(true);

        for (int i = 0; i < battleCount; i++)
        {
            animators[i].SetBool("select", false);
            infoButtons[i].SetActive(false);
        }
    }

    // Funkcija koja selektuje odabranu bitku i deselektuje sve ostale bitke ukoliko je neka bila selektovana
    // Ovu funkciju zove OnClick metoda dugmeta i kao argument prosledjuje sebe(dugme) radi identifikacije
    public void SelectBattle(GameObject battle)
    {
        defaultInfoButton.SetActive(false);

        for (int i = 0; i < battleCount; i++)
        {
            if (battles[i].Equals(battle))
            {
                animators[i].SetBool("select", true);
                infoButtons[i].SetActive(true);
            }
            else
            {
                animators[i].SetBool("select", false);
                infoButtons[i].SetActive(false);
            }
        }
    }
}
