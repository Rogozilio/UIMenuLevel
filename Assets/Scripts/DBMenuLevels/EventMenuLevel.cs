using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventMenuLevel : MonoBehaviour, IPointerEnterHandler
{
    public bool isButtonNextinfoActive;
    public Button ButtonNextInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isButtonNextinfoActive)
        {
           ButtonNextInfo.gameObject.SetActive(true);
        }
    }
}