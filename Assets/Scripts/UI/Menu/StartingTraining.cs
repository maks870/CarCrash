using System;
using System.Collections.Generic;
using UnityEngine;

public class StartingTraining : MonoBehaviour
{

    [SerializeField] private List<Advice> advices = new List<Advice>();
    private int currentAdvice;

    public static Action NextAdviceAction;

    public void StartTraining()
    {
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
}
