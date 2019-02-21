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

    // x-komponenta lokacije sadrzaja
    private float lastContentPos;
    private float currentContentPos;
    // slika se menja kada sadrzaj predje ovu distancu
    private float minSwitchDistance = 100f;
    // odredjuje da li je dozvoljena promena slike
    private bool switchAllowed = true;
    // indeks trenutno selektovane slike
    private int selectedImageID;
    // indeks prethodno selektovane slike
    private int prevSelectedImageID;

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
        // deaktiviraju se indikatori i tekstovi dok se ne odredi selektovana slika
        for (int i = 0; i < images.Length; i++)
        {
            pageIndicators[i].sprite = pageInactive;
            captionTexts[i].SetActive(false);
        }

        // Pamti se zadnja pozicija sadrzaja pre pocetka skrolanja
        if (!isScrolling)
        {
            lastContentPos = contentRect.anchoredPosition.x;
        }
        currentContentPos = contentRect.anchoredPosition.x;
        
        // Slika se menja ukoliko je razlika u poziciji sadrzaja veca od minimalne vrednosti potrebne za promenu slike
        if(Mathf.Abs(lastContentPos - currentContentPos) > minSwitchDistance && switchAllowed)
        {
            // Indeks slike se menja u zavisnosti od smera promene pozicije sadrzaja
            if (lastContentPos > currentContentPos && selectedImageID < images.Length - 1)
            {
                prevSelectedImageID = selectedImageID++;
                switchAllowed = false;
            }
            else if (lastContentPos < currentContentPos && selectedImageID > 0)
            {
                prevSelectedImageID = selectedImageID--;
                switchAllowed = false;
            }
        }
        // Ukoliko se u toku skrolanja korisnik predomisli u vezi promene i vrati se nazad na staru sliku,
        // onda je potrebno vratiti indeks na staru vrednost
        else if (isScrolling && Mathf.Abs(lastContentPos - currentContentPos) < minSwitchDistance && !switchAllowed)
        {
            selectedImageID = prevSelectedImageID;
            switchAllowed = true;
        }

        // aktiviranje indikatora selektovane slike
        if (pageIndicators[selectedImageID].sprite != pageActive)
        {
            pageIndicators[selectedImageID].sprite = pageActive;
        }

        // Ukoliko se ne scrolla, radi se snap(interpolacija) na najblizu sliku
        if (!isScrolling)
        {
            // caption tekst se pojavljuje tek kada se izabere slika (prestane interakcija)
            if (!captionTexts[selectedImageID].activeSelf)
            {
                captionTexts[selectedImageID].SetActive(true);
            }
            // interpolacija (snap)
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, imagePos[selectedImageID].x, imageSnapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
            // dozvoli sledecu promenu slike
            switchAllowed = true;
        }
    }

    // EventTrigger koji se nalazi na scrollu postavlja isScrolling u zavisnosti da li korisnik interaguje sa scrollom
    // Kada se prekine sa scrollanjem zapocinje se snap na najblizu sliku
    public void SetScrolling(bool scroll)
    {
        isScrolling = scroll;
    }
}
