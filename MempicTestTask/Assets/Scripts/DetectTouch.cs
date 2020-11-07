using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class DetectTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private UnityAction OnPointerDown;
    private UnityAction OnPointerUp;
    public void SubscribeOnPointerDown(UnityAction action) 
    {
        OnPointerDown += action;
    }
    public void UnsubscribeOnPointerDown(UnityAction action) 
    {
        OnPointerDown -= action;
    }
    public void SubscribeOnPointerUp(UnityAction action) 
    {
        OnPointerUp += action;
    }
    public void UnsubscribeOnPointerUp(UnityAction action)
    {
        OnPointerUp -= action;
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown?.Invoke();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        OnPointerUp?.Invoke();
    }
}
