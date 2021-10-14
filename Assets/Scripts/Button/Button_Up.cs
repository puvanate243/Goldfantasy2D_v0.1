using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Up : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GolfController golfController;
    bool press = false;

    private void FixedUpdate()
    {
        if (press)
        {
            golfController.Button_Up();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        press = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        press = false;
    }
}
