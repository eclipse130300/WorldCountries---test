using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public CityData cityData;

    //public Text cityNameText;
    public Text cityNameText;
    public Text areaText;
    public Text GDPText;
    public Text populationText;

    public GameObject cityInfoPanel;
    [SerializeField]static float dragCounter;

    public Sprite gpsSprite;
    public Sprite checkMarkSprite;
    public ListManager listManager;
    SpriteRenderer spriteRenderer;
    GameObject sightToApear;

    float dragTimer;
    bool isChecked;
    private void Awake()
    {
        dragCounter = 1.5f; // sets the amount of secounds you have to click and hover over the city to add it to a list
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gpsSprite;
        SetDragTimer();
    }
    private void Update()
    {

    }
    private void OnMouseOver()
    {
        ShowCityName();
        if (Input.GetMouseButtonDown(0))
        {
            DisplayInfo();
        }
    }
    void DisplayInfo()
    {
        cityInfoPanel.SetActive(true);

        areaText.text = "Площадь: " + cityData.area.ToString() + " киллометров^2";
        GDPText.text = "ВВП: " + cityData.GDP.ToString() + " млрд USD";
        populationText.text = "Население: " + cityData.population.ToString();

        Vector3 offset = new Vector3(-0.15f, 0, -0.15f);
        sightToApear = Instantiate(cityData.sight, transform.position + offset, Quaternion.identity);
    }
    private void OnMouseDrag()
    {
        dragTimer -= Time.deltaTime;
        if (dragTimer <= 0)
        {
            SetDragTimer();
            Handheld.Vibrate();
            switch (isChecked)
            {
                case true:
                    ClearMark();
                    break;
                case false:
                    MarkOut();
                    break;
            }       
        }
    }
    private void OnMouseExit()
    {
        cityInfoPanel.SetActive(false);
        cityNameText.gameObject.SetActive(false);
        SetDragTimer();
        Destroy(sightToApear);
    }
    private void ShowCityName()
    {
        cityNameText.text = cityData.cityName;
        cityNameText.gameObject.SetActive(true);
    }
    void MarkOut()
    {
        isChecked = true;
        print("Picked");
        spriteRenderer.sprite = checkMarkSprite;
        if (!listManager.GetCityList().Contains(this))
        {
            listManager.AddToList(this);
        }
    }
    void ClearMark()
    {
        isChecked = false;
        print("Removed");
        spriteRenderer.sprite = gpsSprite;
        if (listManager.GetCityList().Contains(this))
        {
            listManager.RemoveFromList(this);
        }
    }
    void SetDragTimer() 
    {
        dragTimer = dragCounter;
    }
    void ShowSight()
    {

    }
}
