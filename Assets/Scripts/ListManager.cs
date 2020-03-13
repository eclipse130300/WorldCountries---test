using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public GameObject listInfoPanel;
    public GameObject listPanel;

    public Text listInfoText;
    public List<City> pickedCities = new List<City>();
    string resultString;

    public bool inListMenu;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pickedCities.Count >= 1)
        {
            listInfoPanel.SetActive(true);
            UpdateListInfo();
        }
        else
        {
            listInfoPanel.SetActive(false);
        }

    }

   public void AddToList(City item)
    {
        pickedCities.Add(item);
    }

    public void RemoveFromList(City item)
    {
        pickedCities.Remove(item);
    }
    public List<City> GetCityList()
    {
        return pickedCities;
    }
    public void ShowList()
    {
        inListMenu = true;
        listPanel.SetActive(true);
    }
    public void ClearList()
    {
        foreach (City city in pickedCities)
        {
            city.ClearMark();
        }
        pickedCities.Clear();
    }
     void UpdateListInfo()
    {
        if (pickedCities.Count == 1)
        {
            listInfoText.text = "ВЫБРАНА " + pickedCities.Count + " СТРАНА";
        }
        else
        {
            listInfoText.text = "ВЫБРАНО " + pickedCities.Count + NormalInfoString();
        }
    }
     private string NormalInfoString()
    {
       if(pickedCities.Count >= 2 || pickedCities.Count <= 4)
        {
            resultString = " СТРАНЫ";
        }
       if (pickedCities.Count >= 5)
        {
            resultString = " СТРАН";
        }
        return resultString;
    }
    public void BackToMap()
    {
        listPanel.SetActive(false);
        inListMenu = false;
    }
}
