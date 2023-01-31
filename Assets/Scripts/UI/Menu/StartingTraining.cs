using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
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

public class StartingTraining : MonoBehaviour
{
    [SerializeField] private RectTransform[] permanentsElements;
    [SerializeField] private List<Advice> advices = new List<Advice>();
    [SerializeField] private RectTransform trainingTransform;

    // *  //запоминать родителя и элемент для возврата обратно

    private int currentAdvice;

    public static Action NextAdviceAction;

    public void StartTraining()
    {
        foreach (RectTransform rectTransform in permanentsElements) 
        {
            MoveElement(rectTransform);
        }

        currentAdvice = 0;
        NextAdviceAction += OnNextAdvice;
        OnNextAdvice();
    }
    private void OnNextAdvice()
    {
        if (currentAdvice >= 0 && currentAdvice < advices.Count)
        {
            advices[currentAdvice].adviceObj.SetActive(true);
            advices[currentAdvice].Subscribe();
        }

        if (currentAdvice > 0 && currentAdvice <= advices.Count)
        {
            advices[currentAdvice - 1].adviceObj.SetActive(false);
            advices[currentAdvice - 1].Unsubscribe();
        }
        currentAdvice++;
    }

    private void MoveElement(RectTransform element) 
    {
        element.transform.parent = trainingTransform;
    }

    private void RevertElement(RectTransform element) 
    {

    }

    private void EndElement() 
    {
        foreach (RectTransform rectTransform in permanentsElements)
        {
            RevertElement(rectTransform);
        }
    }
}
