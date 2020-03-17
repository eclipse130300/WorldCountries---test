using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public Animator pickedC_Anim;
    public GameObject listWindow;
    public GameObject listLineTemplate;

    public Text listInfoText;
    public List<CityData> pickedCities = new List<CityData>();
    string resultString;

    public bool inListMenu;
    private City[] allCities;
    private List<GameObject> lineList = new List<GameObject>();
    void Awake()
    {
        allCities = FindObjectsOfType<City>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedCities.Count >= 1)
        {
            pickedC_Anim.SetBool("pickedISON", true);
            UpdateMapInfo();
        }
        else
        {
            pickedC_Anim.SetBool("pickedISON", false);
        }
    }

    public void ShowListMenu()
    {
        inListMenu = true;
        listWindow.SetActive(true);
        DefaultSort();
        DisplayList();
    }
    public void ClearPickedCities()
    {
        foreach (City city in allCities)
        {
            city.ClearMark();
        }
        pickedCities.Clear();
    }
    void UpdateMapInfo()
    {
        if (pickedCities.Count == 1)
        {
            listInfoText.text = "ВЫБРАН " + pickedCities.Count + " ГОРОД";
        }
        else
        {
            listInfoText.text = "ВЫБРАНЫ " + pickedCities.Count + NormalInfoString();
        }
    }
    private string NormalInfoString()
    {
        if (pickedCities.Count >= 2 || pickedCities.Count <= 4)
        {
            resultString = " ГОРОДА";
        }
        if (pickedCities.Count >= 5)
        {
            resultString = " ГОРОДОВ";
        }
        return resultString;
    }
    public void BackToMap()
    {
        listWindow.SetActive(false);
        inListMenu = false;
        ClearListMenu();
    }

    public void ClearListMenu()
    {
        if (lineList.Count >= 1)
        {
            foreach (GameObject line in lineList)
            {
                Destroy(line);
            }
            lineList.Clear();
        }
    }
    public void UpdateListMenu()
    {
        ClearListMenu();
        DisplayList();
    }
    public void ReverseList()
    {
        ClearListMenu();
        pickedCities.Reverse();
        DisplayList();
    }
    public void DisplayList()
    {
        for (int i=1; i <= pickedCities.Count; i++)
        {
           GameObject line = Instantiate(listLineTemplate);
            line.SetActive(true);
            line.transform.SetParent(listLineTemplate.transform.parent, false);
            LineTemplateScript lineScript = line.GetComponent<LineTemplateScript>();

            lineScript.lineTextCityName.text = pickedCities[i - 1].cityName;
            lineScript.lineTextArea.text = pickedCities[i - 1].area.ToString();
            lineScript.lineTextPopulation.text = pickedCities[i - 1].population.ToString();
            lineScript.lineTextGRP.text = pickedCities[i - 1].GRP.ToString();

            lineList.Add(line);
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
        return cityD1.GRP.CompareTo(cityD2.GRP);
    }
}
