using UnityEngine;
using UnityEngine.UI;

public class TargetPointer : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform pointerUI; // объект Image UI
    [SerializeField] private Sprite pointerIcon; // иконка когда цель в поле видимости
    [SerializeField] private float interfaceScale = 100;
    [SerializeField] private RectTransform canvas;
    private Transform target;

    public Transform Target { set => target = value; }

    private void Awake()
    {
        pointerUI.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (target != null)
        {
            pointerUI.gameObject.SetActive(true);
            MovePointer(target.position + new Vector3(0,0.5f,0));
        }
        else 
        {
            pointerUI.gameObject.SetActive(false);
        }       
    }

    private void MovePointer(Vector3 posVector)
    {
        Vector3 realPos = mainCamera.WorldToScreenPoint(posVector)/ canvas.localScale.x; // получениее экранных координат объекта

        if (IsBehind(posVector)) // если цель сзади
        {
            pointerUI.gameObject.SetActive(false);
        }
        pointerUI.anchoredPosition = realPos;
    }

    private bool IsBehind(Vector3 point) // true если point сзади камеры
    {
        Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = point - mainCamera.transform.position;
        if (Vector3.Dot(forward, toOther) < 0) return true;
        return false;
    }
}