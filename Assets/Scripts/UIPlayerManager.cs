using UnityEngine;
using UnityEngine.UI;

public class UIPlayerManager : MonoBehaviour
{
    [SerializeField] private Image WarningUI;

    public void WarningActivate() 
    {
        WarningUI.gameObject.SetActive(true);
    }

    public void WarningDeactivate() 
    {
        WarningUI.gameObject.SetActive(false);
    }
}
