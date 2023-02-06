using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sentence
{
    public Button nextStage;
    public GameObject sentenceObj;

    public void Subscribe()
    {
        nextStage.onClick.AddListener(StartingTraining.NextStageAction.Invoke);
    }

    public void Unsubscribe()
    {
        nextStage.onClick.RemoveListener(StartingTraining.NextStageAction.Invoke);
    }
}

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    private Sentence[] sentences;
    private int currentSentence;

    public static Action NextStageAction;
    public Action EndDialogueAction;

    private void Awake()
    {
        sentences = new Sentence[dialogueUI.transform.childCount];
        for (int i = 0; i < sentences.Length; i++)
        {
            Transform sentence = dialogueUI.transform.GetChild(i);

            sentence.gameObject.SetActive(true);
            sentences[i].sentenceObj = sentence.gameObject;
            sentences[i].nextStage = sentence.GetComponentInChildren<Button>();
            sentence.gameObject.SetActive(false);
        }
    }

    public void OnNextAdvice()
    {
        CloseAdvice();

        if (currentSentence >= 0 && currentSentence < sentences.Length)
        {
            sentences[currentSentence].sentenceObj.gameObject.SetActive(true);
            sentences[currentSentence].Subscribe();
        }

        currentSentence++;

        if (currentSentence >= sentences.Length)
            EndDialogue();
    }

    private void CloseAdvice()
    {
        if (currentSentence > 0 && currentSentence <= sentences.Length)
        {
            int advice = currentSentence - 1;
            sentences[advice].sentenceObj.gameObject.SetActive(false);
            sentences[advice].Unsubscribe();
        }
    }

    private void EndDialogue()
    {
        EndDialogueAction.Invoke();
    }

    public void StartTraining()
    {
        currentSentence = 0;
        NextStageAction += OnNextAdvice;
        OnNextAdvice();
    }
}
