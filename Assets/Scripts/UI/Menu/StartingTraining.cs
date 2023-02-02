using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class TrainingStage
{
    public RectTransform trainingUI;
    public RectTransform nonTrainingUI;
    public Button nextStage;
    public GameObject trainingWaiter;

    public void Subscribe()
    {
        nextStage.onClick.AddListener(StartingTraining.NextStageAction.Invoke);
    }

    public void Unsubscribe()
    {
        nextStage.onClick.RemoveListener(StartingTraining.NextStageAction.Invoke);
    }
}

public class StartingTraining : MonoBehaviour
{
    [SerializeField] private TrainingStage[] trainigStages;
    [SerializeField] private RectTransform trainingTransform;
    private Transform oldTransfrom;

    private int currentAdvice;

    public static Action NextStageAction;

    public void StartTraining()
    {
        currentAdvice = 0;
        NextStageAction += OnNextAdvice;
        OnNextAdvice();
    }

    private void OnNextAdvice()
    {

        if (currentAdvice > 0 && currentAdvice <= trainigStages.Length)
        {
            if (trainigStages[currentAdvice - 1].trainingWaiter != null)
                trainigStages[currentAdvice - 1].trainingWaiter.GetComponent<ITrainingWaiter>().UnsubscribeWaitAction(OnNextAdvice);

            if (trainigStages[currentAdvice - 1].nonTrainingUI != null)
                trainigStages[currentAdvice - 1].nonTrainingUI.SetParent(oldTransfrom);

            if (trainigStages[currentAdvice - 1].trainingUI != null)
            {
                trainigStages[currentAdvice - 1].trainingUI?.gameObject.SetActive(false);
                trainigStages[currentAdvice - 1].Unsubscribe();
            }
        }

        if (currentAdvice >= 0 && currentAdvice < trainigStages.Length)
        {
            if (trainigStages[currentAdvice].trainingWaiter != null)
                trainigStages[currentAdvice].trainingWaiter.GetComponent<ITrainingWaiter>().SubscribeWaitAction(OnNextAdvice);

            if (trainigStages[currentAdvice].nonTrainingUI != null)
            {
                oldTransfrom = trainigStages[currentAdvice].nonTrainingUI.parent;
                trainigStages[currentAdvice].nonTrainingUI.SetParent(trainingTransform);
            }

            if (trainigStages[currentAdvice].trainingUI != null)
            {
                trainigStages[currentAdvice].trainingUI?.gameObject.SetActive(true);
                trainigStages[currentAdvice].Subscribe();
            }
        }

        currentAdvice++;

        if (currentAdvice >= trainigStages.Length)
            EndTraining();
    }

    private void EndTraining()
    {
    }
}
