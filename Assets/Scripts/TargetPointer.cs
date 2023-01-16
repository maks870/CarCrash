using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetPointer : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform pointerUI; // объект Image UI
    [SerializeField] private Sprite pointerIcon; // иконка когда цель в поле видимости
    [SerializeField] private Sprite outOfScreenIcon; // иконка когда цель за приделами экрана	
    [SerializeField] private float interfaceScale = 100; // масштаб интерфейса
    private Transform target;

    private Vector3 startPointerSize;

    public Transform Target { set => target = value; }

    private void Awake()
    {
        startPointerSize = pointerUI.sizeDelta;
        pointerUI.gameObject.SetActive(true);
    }
    private void LateUpdate()
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
        Vector3 realPos = mainCamera.WorldToScreenPoint(posVector); // получениее экранных координат объекта
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);

        Vector3 outPos = realPos;
        float direction = 1;

        if (outOfScreenIcon != null)
            pointerUI.GetComponent<Image>().sprite = outOfScreenIcon;

        if (!IsBehind(posVector)) // если цель спереди
        {
            if (rect.Contains(realPos)) // и если цель в окне экрана
            {
                pointerUI.GetComponent<Image>().sprite = pointerIcon;
            }
        }
        else // если цель cзади
        {
            realPos = -realPos;
            outPos = new Vector3(realPos.x, 0, 0); // позиция иконки - снизу
            if (mainCamera.transform.position.y < posVector.y)
            {
                direction = -1;
                outPos.y = Screen.height; // позиция иконки - сверху				
            }
        }
        // ограничиваем позицию областью экрана
        float offset = pointerUI.sizeDelta.x / 2;
        outPos.x = Mathf.Clamp(outPos.x, offset, Screen.width - offset);
        outPos.y = Mathf.Clamp(outPos.y, offset, Screen.height - offset);

        Vector3 pos = realPos - outPos; // направление к цели из PointerUI 

        RotatePointer(direction * pos);

        pointerUI.sizeDelta = new Vector2(startPointerSize.x / 100 * interfaceScale, startPointerSize.y / 100 * interfaceScale);
        pointerUI.anchoredPosition = outPos;
    }

    private bool IsBehind(Vector3 point) // true если point сзади камеры
    {
        Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = point - mainCamera.transform.position;
        if (Vector3.Dot(forward, toOther) < 0) return true;
        return false;
    }
    private void RotatePointer(Vector2 direction) // поворачивает PointerUI в направление direction
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pointerUI.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


}