using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class Sentence
{
    public Button nextStage;
    public GameObject sentenceObj;

    public void Subscribe(UnityAction action)
    {
        nextStage.onClick.AddListener(action);
    }

    public void Unsubscribe(UnityAction action)
    {
        nextStage.onClick.RemoveListener(action);
    }
}

public class Dialogue : MonoBehaviour
{
    const float textOutputDelay = 0.05f;

    [SerializeField] private GameObject dialogueUI;
    private Sentence[] sentences;
    private int currentSentence;

    public Action NextSentenceAction;
    public Action EndDialogueAction;

    public Sentence[] Sentences => sentences;

    private void Awake()
    {
        sentences = new Sentence[dialogueUI.transform.childCount];
        for (int i = 0; i < sentences.Length; i++)
        {
            sentences[i] = new Sentence();
            GameObject sentenceObj = dialogueUI.transform.GetChild(i).gameObject;
            sentenceObj.gameObject.SetActive(true);

            sentences[i].sentenceObj = sentenceObj;
            sentences[i].nextStage = sentenceObj.GetComponentInChildren<Button>();
            sentenceObj.gameObject.SetActive(false);
        }
    }

    public void OnNextSentence()
    {
        CloseSentence();
        Debug.Log("Текущее предложение номер " + currentSentence);
        Debug.Log(sentences.Length);
        if (currentSentence >= 0 && currentSentence < sentences.Length)
        {
            Sentence sentence = sentences[currentSentence];
            sentence.sentenceObj.gameObject.SetActive(true);
            sentence.Subscribe(NextSentenceAction.Invoke);
            StartCoroutine(SentenceOutput(sentence));
        }

        currentSentence++;

        if (currentSentence > sentences.Length)
        {
            EndDialogue();
        }
    }

    private void CloseSentence()
    {
        if (currentSentence > 0 && currentSentence <= sentences.Length)
        {
            int sentence = currentSentence - 1;
            sentences[sentence].sentenceObj.gameObject.SetActive(false);
            sentences[sentence].Unsubscribe(NextSentenceAction.Invoke);
        }
    }

    private void EndDialogue()
    {
        EndDialogueAction?.Invoke();
    }

    public void StartDialogue()
    {
        currentSentence = 0;
        NextSentenceAction += OnNextSentence;
        OnNextSentence();
    }

    IEnumerator SentenceOutput(Sentence sentence)
    {
        Text text = sentence.sentenceObj.GetComponentsInChildren<Text>()[1];
        string sentenceText = text.text;
        text.text = "";
        sentence.nextStage.gameObject.SetActive(false);

        foreach (char ch in sentenceText)
        {
            text.text += ch;
            yield return new WaitForSeconds(textOutputDelay);
        }

        sentence.nextStage.gameObject.SetActive(true);
    }
}
