using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    [SerializeField] protected PlayerLoad playerLoad;
    [SerializeField] public GameObject objectUI;

    protected virtual void SavePlayer()
    {
    }

    public virtual void SOLoaderSubscribe()
    {

    }

    public virtual void SaveDefaultSO()
    {

    }

    public virtual void OpenMenu()
    {
        objectUI.SetActive(true);
    }

    public virtual void CloseMenu()
    {
        objectUI.SetActive(false);
    }

    public virtual void InitializeMenu()
    {

    }
}
