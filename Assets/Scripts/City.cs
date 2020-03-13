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
    SpriteRenderer spriteRenderer;
    GameObject sightToApear;

    public Sprite gpsSprite;
    public Sprite checkMarkSprite;
    public ListManager listManager;

    static float sightOffset = 0.15f; // offset of each sightPoint
    static float dragCounter = 1.5f;  // sets the amount of secounds you have to click and hover over the city to add it to a list

    float dragTimer;
    bool isChecked = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gpsSprite;

        SetDragTimer();
    }

    private void OnMouseOver()
    {
        ShowCityName();
        if (Input.touchCount > 0 && !isChecked)
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

        ShowSight();
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
                    DeleteCityFromList();
                    break;
                case false:
                    MarkOut();
                    AddCityToList();
                    break;
            }       
        }
    }
    private void OnMouseExit()
    {
        cityInfoPanel.SetActive(false);
        cityNameText.gameObject.SetActive(false);
        SetDragTimer();
        if (sightToApear != null)
        {
            Destroy(sightToApear);
        }
    }
    private void ShowCityName()
    {
        cityNameText.text = cityData.cityName;
        cityNameText.gameObject.SetActive(true);
    }
    void MarkOut() //галочка появилась
    {
        isChecked = true;
        cityInfoPanel.SetActive(false);
        spriteRenderer.sprite = checkMarkSprite;
    }

    private void AddCityToList()
    {
        if (!listManager.GetCityList().Contains(this))
        {
            listManager.AddToList(this);
        }
    }

    public void ClearMark()  // вместо галочки снова GPS
    {
        isChecked = false;
        spriteRenderer.sprite = gpsSprite;
    }

    private void DeleteCityFromList()
    {
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
        Vector3 offsetDirection = (transform.position - Camera.main.transform.position);
        offsetDirection.y = 0;
        Vector3 sightPoint = offsetDirection.normalized * sightOffset;
        if (sightToApear == null)
        {
            sightToApear = Instantiate(cityData.sight, transform.position + sightPoint, cityData.sight.transform.rotation);
        }
    }
}
