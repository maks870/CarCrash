using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TrainingStage
{
    public RectTransform trainingUI;
    public RectTransform nonTrainingUI;
    public Button nextStage;


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
    [SerializeField] private RectTransform[] permanentsElements;
    [SerializeField] private TrainingStage[] trainigStages;
    [SerializeField] private RectTransform trainingTransform;
    private Transform oldTransfrom;
    private List<Transform> oldTransforms = new List<Transform>();

    private int currentAdvice;

    public static Action NextStageAction;

    public void StartTraining()
    {
        foreach (RectTransform rectTransform in permanentsElements)
        {
            oldTransforms.Add(rectTransform.parent);
            rectTransform.parent = trainingTransform;
        }

        currentAdvice = 0;
        NextStageAction += OnNextAdvice;
        OnNextAdvice();
    }

    private void OnNextAdvice()
    {
        if (currentAdvice > 0 && currentAdvice <= trainigStages.Length)
        {
            if (trainigStages[currentAdvice - 1].nonTrainingUI != null)
                trainigStages[currentAdvice - 1].nonTrainingUI.parent = oldTransfrom;

            trainigStages[currentAdvice - 1].trainingUI.gameObject.SetActive(false);
            trainigStages[currentAdvice - 1].Unsubscribe();
        }

        if (currentAdvice >= 0 && currentAdvice < trainigStages.Length)
        {
            if (trainigStages[currentAdvice].nonTrainingUI != null)
            {
                oldTransfrom = trainigStages[currentAdvice].nonTrainingUI.parent;
                trainigStages[currentAdvice].nonTrainingUI.parent = trainingTransform;
            }

            trainigStages[currentAdvice].trainingUI.gameObject.SetActive(true);
            trainigStages[currentAdvice].Subscribe();
        }

        currentAdvice++;

        if (currentAdvice >= trainigStages.Length)
            EndTraining();
    }

    private void EndTraining()
    {
        for (int i = 0; i < permanentsElements.Length; i++)
        {
            permanentsElements[i].parent = oldTransforms[i]; ;
        }
    }
}
