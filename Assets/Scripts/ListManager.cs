using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    public List<City> pickedCities = new List<City>();
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
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
}
