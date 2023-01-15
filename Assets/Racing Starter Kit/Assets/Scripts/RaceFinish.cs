﻿using UnityEngine;
using UnityEngine.UI;
using YG;

public class RaceFinish : MonoBehaviour
{
    [SerializeField] private AwardPresenter awardPresenter;
    [SerializeField] private GameObject FinishCam;
    [SerializeField] private GameObject ViewModes;
    [SerializeField] private GameObject PosDisplay, PauseButton, Panel1, Panel2;
    [SerializeField] private GameObject FinishPanelWin, FinishPanelLose;
    [SerializeField] private GameObject finishPanels;
    [SerializeField] private Image cup;
    [SerializeField] private Sprite[] spritesCup;
    [SerializeField] private Text posText;

    private void WriteRecords()
    {
        //записываем рекорды если есть
    }

    private void ShowRecords()
    {
        //записываем рекорды если есть
    }

    public void Finish()
    {
        WriteRecords();
        ShowRecords();

        YandexGame.savesData.playerWrapper.lastMapPlaces.Add(ChkManager.posMax);
        YandexGame.SaveProgress();

        FinishCam.SetActive(true);
        PauseButton.SetActive(false);
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        ViewModes.SetActive(false);
        finishPanels.SetActive(true);
        posText.text = ChkManager.posMax.ToString();

        if (ChkManager.posMax < 4)
        {
            cup.sprite = spritesCup[ChkManager.posMax - 1];
            FinishPanelWin.SetActive(true);//win panel turns on
            FinishPanelLose.SetActive(false);//lose panel turns off
        }
        //you lose (not 1st position)
        else
        {
            FinishPanelWin.SetActive(false);//win panel turns off
            FinishPanelLose.SetActive(true);//lose panel turns on
            AudioListener.volume = 0f;//audio turns off
            Time.timeScale = 0;//time stops
        }
    }
}