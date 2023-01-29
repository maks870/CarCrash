using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Advice
{
    public GameObject adviceObj;
    public Button button;


    public void Subscribe()
    {
        button.onClick.AddListener(StartingTraining.NextAdviceAction.Invoke);
    }

    public void Unsubscribe()
    {
        button.onClick.RemoveListener(StartingTraining.NextAdviceAction.Invoke);
    }
}
