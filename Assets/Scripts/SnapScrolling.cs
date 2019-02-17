using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Postavlja se na element sa slikama jer se njegova pozicija snap-uje
public class SnapScrolling : MonoBehaviour
{
    [Header("Images")]
    // container sa slikama (element koji se snap-uje)
    public GameObject imageContainer;
    // slike koje se nalaze u elementu koji se snap-uje
    private GameObject[] images;
    // razmak izmedju slika
    public int imageOffset;
    // brzina interpolacije
    public float imageSnapSpeed;
    // slike su na pocetku pozicionirane jedna preko druge, tokom inicijalizacije im se postavlja pozicija
    private Vector2[] imagePos;
    // indeks trenutno selektovane slike
    private int selectedImageID;
    // pozicija content elementa
    private RectTransform contentRect;
    private Vector2 contentVector;
    // sluzi za indikaciju interakcije sa galerijom
    private bool isScrolling;

    [Header("Indicators")]
    // container za indikatore stranica
    public GameObject pageIndicatorContainer;
    // indikatori koji se nalaze u conteineru (broj indikatora ne sme biti manji od broja slika)
    private Image[] pageIndicators;
    // sprite-ovi za indikatore
    public Sprite pageActive;
    public Sprite pageInactive;

    [Header("Captions")]
    // caption container
    public GameObject captionContainer;
    // tesktovi koji se nalaze u containeru (broj tesktova ne sme biti manji od broja slika)
    private GameObject[] captionTexts;

    // Inicijalizacija
    private void Start()
    {
        contentRect = GetComponent<RectTransform>();
        images = new GameObject[imageContainer.transform.childCount];
        imagePos = new Vector2[images.Length];
        pageIndicators = new Image[images.Length];
        captionTexts = new GameObject[images.Length];
        // Slike koje se nalaze u nizu images se rasporedjuju po horizontali i pamte se te njihove nove pozicije
        for (int i = 0; i < images.Length; i++)
        {
            // dobavljanje slika iz kontejnera
            images[i] = imageContainer.transform.GetChild(i).gameObject;
            // kalkulacija njihovih pozicija
            if (i != 0)
            {
                images[i].transform.localPosition = new Vector2(images[i - 1].transform.localPosition.x + images[i].GetComponent<RectTransform>().rect.width + imageOffset, 
                    images[i].transform.localPosition.y);
            }
            imagePos[i] = -images[i].transform.localPosition;
            // dobavljanje indikatora iz kontejnera
            pageIndicators[i] = pageIndicatorContainer.transform.GetChild(i).gameObject.GetComponent<Image>();
            // dobavljanje tekstova iz kontejnera
            captionTexts[i] = captionContainer.transform.GetChild(i).gameObject;
        }
    }
    
    private void FixedUpdate()
    {
        // Trazi se ID slike koja je najbliza centru, tako sto se izracunava udaljenost svake slike od centra
        // slika koja ima najmanju udaljenost postaje kandidat za snap kada se skrol otpusti
        float nearestPos = float.MaxValue;
        for (int i = 0; i < images.Length; i++)
        {
            // Kada se scrolla content njegova pozicija x ide u plus ili minus, dok je pozicija slike staticna
            // Razlika pomeraja contenta i pozicije slike daje trenutnu udaljenost slike od centra
            // Obzirom da ova vrednost moze da bude manja od nule koristi se Mathf.Abs kako bi nula bila najmanja moguca distanca
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - imagePos[i].x);
            if(distance < nearestPos)
            {
                nearestPos = distance;
                selectedImageID = i;
            }
            // deaktiviraju se svi indikatori, jer je kandidat za selekciju poznat tek nakon kraja petlje
            pageIndicators[i].sprite = pageInactive;
            // deaktivacija svih tekstova
            captionTexts[i].SetActive(false);
        }
        // aktiviranje indikatora selektovane slike
        if (pageIndicators[selectedImageID].sprite != pageActive)
        {
            pageIndicators[selectedImageID].sprite = pageActive;
        }
        // Ukoliko se ne scrolla radi se snap(interpolacija) na najblizu sliku
        if (!isScrolling)
        {
            // caption tekst se tek kada se izabere slika (prestane interakcija)
            if (!captionTexts[selectedImageID].activeSelf)
            {
                captionTexts[selectedImageID].SetActive(true);
            }
            // interpolacija (snap)
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, imagePos[selectedImageID].x, imageSnapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
    }

    // EventTrigger koji se nalazi na scrollu postavlja isScrolling u zavisnosti da li korisnik interaguje sa scrollom
    // Kada se prekine sa scrollanjem zapocinje se snap na najblizu sliku
    public void SetScrolling(bool scroll)
    {
        isScrolling = scroll;
    }
}
