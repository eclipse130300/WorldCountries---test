using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public GameObject listInfoPanel;
    public GameObject listWindow;
    public GameObject listLineTemplate;

    public Text listInfoText;
    public List<CityData> pickedCities = new List<CityData>();
    string resultString;

    public bool inListMenu;
    private City[] allCities;

    void Awake()
    {
        allCities = FindObjectsOfType<City>();
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

   public void AddToList(CityData item)
    {
        pickedCities.Add(item);
    }

    public void RemoveFromList(CityData item)
    {
        pickedCities.Remove(item);
    }
    public List<CityData> GetCityList()
    {
        return pickedCities;
    }
    public void ShowListMenu()
    {
        inListMenu = true;
        listWindow.SetActive(true);
        DisplayList();
    }
    public void ClearList()
    {
        foreach (City city in allCities)
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
        listWindow.SetActive(false);
        inListMenu = false;
    }
    void DisplayList()
    {
        for (int i=1; i <= pickedCities.Count; i++)
        {
           GameObject line = Instantiate(listLineTemplate);
            line.SetActive(true);
            line.transform.SetParent(listLineTemplate.transform.parent, false);
        }
    }
    // SORTING METHODS AND INTERFACE IMPLEMENTATIONS
    public void DefaultSort()
    {
        SortByName sbn = new SortByName();
        pickedCities.Sort(sbn);
    }
    public void AreaSort()
    {
        SortByArea sba = new SortByArea();
        pickedCities.Sort(sba);
    }
    public void PopulationSort()
    {
        SortByPopulation sbp = new SortByPopulation();
        pickedCities.Sort(sbp);
    }
    public void GRPSort()
    {
        SortByGRP sbG = new SortByGRP();
        pickedCities.Sort(sbG);
    }

}
public class SortByName : IComparer<CityData>
{
    public int Compare(CityData cityD1, CityData cityD2)
    {
        return cityD1.cityName.CompareTo(cityD2.cityName);
    }
}
public class SortByArea : IComparer<CityData>
{
    public int Compare(CityData cityD1, CityData cityD2)
    {
        return cityD1.area.CompareTo(cityD2.area);
    }
}
public class SortByPopulation : IComparer<CityData>
{
    public int Compare(CityData cityD1, CityData cityD2)
    {
        return cityD1.population.CompareTo(cityD2.population);
    }
}
public class SortByGRP : IComparer<CityData>
{
    public int Compare(CityData cityD1, CityData cityD2)
    {
        return cityD1.GDP.CompareTo(cityD2.GDP);
    }
}
