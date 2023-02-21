using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileInputObj;
    [SerializeField] private GameObject desktopInputObj;

    private BaseInput currentInput;
    public BaseInput CurrentInput => currentInput;

    private void Awake()
    {
        Cursor.visible = false;

        if (YG.YandexGame.EnvironmentData.isDesktop)
            currentInput = Instantiate(desktopInputObj).GetComponent<BaseInput>();
        else
            currentInput = Instantiate(mobileInputObj).GetComponent<BaseInput>();

    }

    public void Enable(bool active) 
    {
        currentInput.gameObject.SetActive(active);
    }
}
