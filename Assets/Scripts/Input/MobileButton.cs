using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonDown;
    public UnityEvent buttonUp;
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonDown.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonUp.Invoke();
    }
}
