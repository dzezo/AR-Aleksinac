using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapScrolling : MonoBehaviour
{
    public GameObject[] images;
    public int imageOffset;
    public float imageSnapSpeed;
    private Vector2[] imagePos;
    private int selectedImageID;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private bool isScrolling;

    // Inicijalizacija
    private void Start()
    {
        contentRect = GetComponent<RectTransform>();
        imagePos = new Vector2[images.Length];
        // Slike koje se nalaze u nizu images se rasporedjuju po horizontali i pamte se te njihove nove pozicije
        for (int i = 0; i < images.Length; i++)
        {
            if (i != 0)
            {
                images[i].transform.localPosition = new Vector2(images[i - 1].transform.localPosition.x + images[i].GetComponent<RectTransform>().sizeDelta.x + imageOffset, 
                    images[i].transform.localPosition.y);
            }
            imagePos[i] = -images[i].transform.localPosition;
        }
    }
    
    private void FixedUpdate()
    {
        // Trazi se ID slike koja je najbliza centru
        float nearestPos = float.MaxValue;
        for (int i = 0; i < images.Length; i++)
        {
            // Kada se scrolla content njegova pozicija x ide u plus ili minus, dok je pozicija slike staticna
            // Razlika pomeraja contenta i pozicije slike daje trenutnu udaljenost slike od centra
            // Obzirom da ova vrednost moze da bude manja od nule (za slike koje se nalaze levo) koristi se Mathf.Abs kako bi
            // nula bila najmanja moguca distanca, ovako se nalazi indeks slike najblize centru
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - imagePos[i].x);
            if(distance < nearestPos)
            {
                nearestPos = distance;
                selectedImageID = i;
            }
        }
        // Ukoliko se ne scrolla radi se snap(interpolacija) na najblizu sliku
        if (!isScrolling)
        {
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
