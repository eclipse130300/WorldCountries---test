using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New City", menuName = "Create City")]
public class CityData : ScriptableObject
{
    public string cityName;
    public int GRP;
    public int population;
    public int area;
    public GameObject sight;
}
