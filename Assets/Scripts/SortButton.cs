using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SortButton : MonoBehaviour, IPointerDownHandler
{
    public ListManager lm;
    public RectTransform arrowImage;
    float arrowRotationAngle = 180f;
    public enum ButtonKind { area, population, GRP }
    public ButtonKind button;
    int clickCount;

    public void OnPointerDown(PointerEventData eventData)
    {
        clickCount++;
        Vector3 rotationAngle = new Vector3(0, 0, arrowRotationAngle);
        arrowImage.Rotate(rotationAngle);
        if (clickCount != 0 && clickCount % 2 == 0) //use bool?
        {
            lm.ReverseList();
        }
        else
        {
            switch (button)
            {
                case ButtonKind.area:
                    lm.AreaSort();
                    break;
                case ButtonKind.population:
                    lm.PopulationSort();
                    break;
                case ButtonKind.GRP:
                    lm.GRPSort();
                    break;
            }
            lm.UpdateListMenu();
        }
    }
}
