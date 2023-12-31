﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LvlMenu : MonoBehaviour
{
    [SerializeField] private GameObject RaceUI, Countdown, FinishCamera, LapsSelected;
    //also, it is used if we hit restart in the pause menu
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private MissionManager missionManager;
    [SerializeField] SoundController soundController;
    [SerializeField] private bool isFreeplayMode = false;

    private bool endRace = false;
    public bool EndRace { get => endRace; set => endRace = value; }

    private void Start()
    {
        Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !EndRace)
        {
            Pause();
        }
    }
    public void Restart()
    {
        SceneTransition.ReloadScene();
    }

    public void Play()
    {
        RaceUI.SetActive(true);

        if (!isFreeplayMode)
        {
            LapsSelected.SetActive(true);
            FinishCamera.SetActive(false);
            Countdown?.SetActive(true);
        }

        SetPause(false);
    }

    public void Exit()
    {
        ExitScene("General");
    }

    public void ExitInCity()
    {
        ExitScene("SimpleTown");
    }

    private void ExitScene(string scene) 
    {
        SetPause(false);
        soundController.EnableSoundEffect(false);
        SceneTransition.SwitchScene(scene);
    }

    public void Pause()
    {
        SetPause(true);
    }

    private void SetPause(bool paused)
    {
        StartCoroutine(PrePause(paused));
    }

    private IEnumerator PrePause(bool paused) 
    {
        soundController.EnableSoundEffect(!paused);

        yield return new WaitForNextFrameUnit();

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        PauseMenu.SetActive(paused);
    }
}
