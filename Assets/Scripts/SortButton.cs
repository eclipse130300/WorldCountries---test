using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortButton : MonoBehaviour
{
    public ListManager listManager;
    private bool isClicked;


    private void OnMouseDown()
    {
        isClicked = true;
        print("OKOKOK");
    }
}
