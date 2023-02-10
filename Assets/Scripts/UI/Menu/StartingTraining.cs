using System;
using UnityEngine;
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
    [SerializeField] private RectTransform trainingTransform;
    [SerializeField] protected TrainingStage[] trainigStages;
    private Transform oldTransfrom;

    private int currentAdvice;

    public static Action NextStageAction;
    public Action EndDialogueAction;
    public void OnNextAdvice()
    {
        CloseAdvice();

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
                trainigStages[currentAdvice].trainingUI.gameObject.SetActive(true);
                trainigStages[currentAdvice].Subscribe();
            }
        }

        currentAdvice++;

        if (currentAdvice > trainigStages.Length)
            EndDialogue();
    }

    private void CloseAdvice()
    {
        if (currentAdvice > 0 && currentAdvice <= trainigStages.Length)
        {
            int advice = currentAdvice - 1;

            if (trainigStages[advice].trainingWaiter != null)
                trainigStages[advice].trainingWaiter.GetComponent<ITrainingWaiter>().UnsubscribeWaitAction(OnNextAdvice);

            if (trainigStages[advice].nonTrainingUI != null)
                trainigStages[advice].nonTrainingUI.SetParent(oldTransfrom);

            if (trainigStages[advice].trainingUI != null)
            {
                trainigStages[advice].trainingUI.gameObject.SetActive(false);
                trainigStages[advice].Unsubscribe();
            }
        }
    }

    private void EndDialogue()
    {
        EndDialogueAction?.Invoke();
    }

    public void StartTraining()
    {
        currentAdvice = 0;
        NextStageAction += OnNextAdvice;
        OnNextAdvice();
    }
}
