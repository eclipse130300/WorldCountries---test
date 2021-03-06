﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public CityData cityData;

    //public Text cityNameText;
    public Text cityNameText;
    public Text areaText;
    public Text GRPText;
    public Text populationText;

    public Animator cityInfo_Anim;
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
        cityInfo_Anim.SetBool("InfoIsON", true);

        areaText.text = "Площадь: " + cityData.area.ToString() + " киллометров^2";
        GRPText.text = "ВРП: " + cityData.GRP.ToString() + " млрд USD";
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
        cityInfo_Anim.SetBool("InfoIsON", false);
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
        cityInfo_Anim.SetBool("InfoIsON", false);
        isChecked = true;
        spriteRenderer.sprite = checkMarkSprite;
    }

    private void AddCityToList()
    {
        if (!listManager.pickedCities.Contains(cityData))
        {
            listManager.pickedCities.Add(cityData);
        }
    }

    public void ClearMark()  // вместо галочки снова GPS
    {
        isChecked = false;
        spriteRenderer.sprite = gpsSprite;
    }

    private void DeleteCityFromList()
    {
        if (listManager.pickedCities.Contains(cityData))
        {
            listManager.pickedCities.Remove(cityData);
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
