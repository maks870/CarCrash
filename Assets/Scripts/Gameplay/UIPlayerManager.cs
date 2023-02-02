using UnityEngine;
using UnityEngine.UI;

public class UIPlayerManager : MonoBehaviour
{
    [SerializeField] private Image WarningUI;
    [SerializeField] private TargetPointer missleTarget;
    [SerializeField] private TargetPointer mineWarning;

    public TargetPointer MisslePointer => missleTarget;
    public TargetPointer MinePointer => mineWarning;

    public void WarningActivate()
    {
        WarningUI.gameObject.SetActive(true);
    }

    public void WarningDeactivate()
    {
        WarningUI.gameObject.SetActive(false);
    }
}
