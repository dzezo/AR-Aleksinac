using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    // Singleton
    public static ApplicationManager instance;

    // Paneli koje je korisnik obisao (Istorija panela)
    private Stack<GameObject> panels = new Stack<GameObject>();

    // Funkcija koja osigurava da u sistemu postoji samo jedna instanca
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Vraca poslednji panel koji je korisnik obisao i uklanja ga
    public GameObject RemoveLastPanel()
    {
        return panels.Pop();
    }

    // Vraca poslednji panel koji je korisnik obisao bez uklanjanja
    public GameObject GetLastPanel()
    {
        if (panels.Count > 0)
        {
            return panels.Peek();
        }
        else
        {
            return null;
        }
    }

    // Dodaje poslednji panel koji je korisnik obisao
    public void SetLastPanel(GameObject panel)
    {
        panels.Push(panel);
    }

    public int ReturnCount()
    {
        return panels.Count;
    }
}
