using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Skripta se stavlja na kontejner sa slikama
public class LoopScrolling : MonoBehaviour
{
    public ScrollRect scrollRect;

    [Header("Auto Rotation")]
    public bool autoRotation = true;
    public float autoRotationVelocity;
    private Vector2 velocityVector;
    public float autoRotationDelay; // in sec
    private float startRotationTime;
    private bool autoRotateRight = true;

    private RectTransform contentRect;
    private GameObject[] images;
    private float imageWidth;

    private int prev;
    private int curr;
    private int next;

    private float leftLimit;
    private float rightLimit;

    // Inicijalizacija
    void Start()
    {
        velocityVector = new Vector2(autoRotationVelocity, 0);

        contentRect = GetComponent<RectTransform>();
        images = new GameObject[gameObject.transform.childCount];

        // Azuriraj pokazivace
        curr = 0;
        prev = Mod(curr - 1, images.Length);
        next = Mod(curr + 1, images.Length);

        for (int i = 0; i < images.Length; i++)
        {
            // Dobavi slike iz kontejnera
            images[i] = gameObject.transform.GetChild(i).gameObject;
            // Ukljuci samo one na koje ukazuju pokazivaci
            if (i == prev || i == curr || i == next)
            {
                images[i].SetActive(true);
            }
            else
            {
                images[i].SetActive(false);
            }
        }

        // Sve slike imaju istu sirinu
        imageWidth = images[0].GetComponent<RectTransform>().rect.width;

        // Izracunaj lokaciju slika na koje ukazuju pokazivaci
        // Tokom inicijalizacije se ne racuna pozicija za pocetnu sliku jer je ona na lokaciji 0
        images[prev].transform.localPosition = new Vector2(images[curr].transform.localPosition.x - imageWidth, images[prev].transform.localPosition.y);
        images[next].transform.localPosition = new Vector2(images[curr].transform.localPosition.x + imageWidth, images[next].transform.localPosition.y);

        // Postavi limite
        leftLimit = images[prev].transform.localPosition.x + 150.0f;
        rightLimit = images[next].transform.localPosition.x - 150.0f;
    }

    void Update()
    {
        if (autoRotation && Time.realtimeSinceStartup > startRotationTime)
        {
            // postavlja se nova brzina rotaciju ukoliko je doslo do promene u inspektoru
            velocityVector.x = autoRotationVelocity;
            // Iskljucuje se trenje i daje se brzina skrolu (rotacija bez zaustavljanja)
            scrollRect.decelerationRate = 1.0f;
            scrollRect.velocity = (autoRotateRight) ? velocityVector : velocityVector * -1.0f;
        }
        
        // Ukoliko se predju limiti, postavi odgovarajucu sliku
        if(contentRect.anchoredPosition.x < leftLimit)
        {
            SpawnRight();
        }
        else if (contentRect.anchoredPosition.x > rightLimit)
        {
            SpawnLeft();
        }
    }

    private void SpawnRight()
    {
        // Iskljuci levi
        images[prev].SetActive(false);

        // Azuriraj indekse
        prev = curr;
        curr = next;
        next = Mod(next + 1, images.Length);

        // Ukljuci desni i izracunaj mu lokaciju
        images[next].SetActive(true);
        images[next].transform.localPosition = new Vector2(images[curr].transform.localPosition.x + imageWidth, images[next].transform.localPosition.y);
        
        // Azuriraj limite
        rightLimit = leftLimit;
        leftLimit = leftLimit - images[curr].GetComponent<RectTransform>().rect.width;
    }

    private void SpawnLeft()
    {
        // Iskljuci desni
        images[next].SetActive(false);

        // Azuriraj indekse
        next = curr;
        curr = prev;
        prev = Mod(prev - 1, images.Length);

        // Ukljuci levi i izracunaj mu lokaciju
        images[prev].SetActive(true);
        images[prev].transform.localPosition = new Vector2(images[curr].transform.localPosition.x - imageWidth, images[prev].transform.localPosition.y);
        
        // Azuriraj limite
        leftLimit = rightLimit;
        rightLimit = rightLimit + images[curr].GetComponent<RectTransform>().rect.width;
    }

    // Operator % vraca ostatak (-1 % 7 = -1), ova funkcija vraca mod (-1 mod 7 = 6)
    // Funkcija sluzi da bi se dobavio indeks u nizu
    private int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public void SetScrolling(bool scroll)
    {
        // Ova grana se izvrsava kada korisnik zapocne skrolanje
        if (scroll)
        {
            autoRotation = false;
            // vraca se trenje kako bi se rotacija usporila kada korisnik zavrsi skrolanje
            scrollRect.decelerationRate = 0.135f;
        }
        // Ova grana se izvrsava kada korisnik zavrsi skrolanje
        else
        {
            autoRotation = true;
            startRotationTime = Time.realtimeSinceStartup + autoRotationDelay;
            // postavlja se smer rotacije koji odgovara smeru u kome je korisnik skrolao
            autoRotateRight = (scrollRect.velocity.x > 0) ? true : false;
        }
    }
}
