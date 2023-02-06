using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : StartingTraining
{
    public Button nextStage;
    public Action EndDialogueAction;

    private void Start()
    {
        foreach (TrainingStage stage in trainigStages)
        {
            stage.nextStage = nextStage;
        }
    }

    protected override void EndDialogue()
    {
        EndDialogueAction.Invoke();
    }

}
