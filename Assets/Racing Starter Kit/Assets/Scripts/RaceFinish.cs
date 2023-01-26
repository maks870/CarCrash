using UnityEngine;
using UnityEngine.UI;
using YG;

public class RaceFinish : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject FinishCam;
    [SerializeField] private GameObject ViewModes;
    [SerializeField] private GameObject PosDisplay, PauseButton, Panel1, Panel2;
    [SerializeField] private GameObject FinishPanelWin, FinishPanelLose;
    [SerializeField] private GameObject finishPanels;
    [SerializeField] private GameObject recordPanels;
    [SerializeField] private Text currentTime;
    [SerializeField] private GameObject newRecordObj;
    [SerializeField] private Image cup;
    [SerializeField] private Sprite[] spritesCup;
    [SerializeField] private Text posText;
    private bool finish = false;

    private void WriteRecords()
    {
        int currentMapIndex = YandexGame.savesData.playerWrapper.GetMapInfoIndex(YandexGame.savesData.playerWrapper.lastMap);
        MapInfo currentMap = YandexGame.savesData.playerWrapper.maps[currentMapIndex];

        int currentTimeInSeconds = LapTimeManager.MinuteCount * 60 + LapTimeManager.SecondCount;
        int currentTimeInMiliSeconds = (int)(LapTimeManager.MilliCount * 10);

        if (currentMap.fastestTime == 0 || currentMap.fastestTime > currentTimeInSeconds)
        {
            currentMap.fastestTime = currentTimeInSeconds;
            YandexGame.savesData.playerWrapper.maps[currentMapIndex].fastestTimeMiliSec = currentTimeInMiliSeconds;
            currentMap.newRecordTime = true;
        }

        if (currentMap.highestPlace == 0 || currentMap.highestPlace > ChkManager.posMax)
        {
            currentMap.highestPlace = ChkManager.posMax;
            currentMap.newRecordPlace = true;
        }

    }

    private void ShowRecords()
    {
        int currentMapIndex = YandexGame.savesData.playerWrapper.GetMapInfoIndex(YandexGame.savesData.playerWrapper.lastMap);
        MapInfo currentMap = YandexGame.savesData.playerWrapper.maps[currentMapIndex];

        if (currentMap.newRecordTime == true)
            newRecordObj.SetActive(true);

        string minNull = LapTimeManager.MinuteCount < 10 ? "" : "0";
        string secNull = LapTimeManager.SecondCount < 10 ? "" : "0";
        string miliSecNull = LapTimeManager.MilliCount < 10 ? "" : "0";

        currentTime.text = $"{minNull}{LapTimeManager.MinuteCount}.{secNull}{LapTimeManager.SecondCount}:{miliSecNull}{(int)(LapTimeManager.MilliCount * 10)}";

        recordPanels.SetActive(true);
        newRecordObj.SetActive(currentMap.newRecordTime);
    }

    public void Finish()
    {
        if (finish)
            return;

        finish = true;

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
        inputManager.CurrentInput.gameObject.SetActive(false);

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
        }
        AudioListener.volume = 0f;//audio turns off
    }
}