using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileInputObj;
    [SerializeField] private GameObject desktopInputObj;
    public bool mobile; //для теста
    private BaseInput currentInput;
    public BaseInput CurrentInput => currentInput;

    private void Awake()
    {
        if (mobile)
            currentInput = Instantiate(mobileInputObj).GetComponent<BaseInput>();
        else
            currentInput = Instantiate(desktopInputObj).GetComponent<BaseInput>();
    }
}
