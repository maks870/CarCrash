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
    [SerializeField] private Image cup;
    [SerializeField] private Sprite[] spritesCup;
    [SerializeField] private Text posText;
    private bool finish = false;

    private void WriteRecords()
    {
        int currentMapIndex = YandexGame.savesData.playerWrapper.GetMapInfoIndex(YandexGame.savesData.playerWrapper.lastMap);
        MapInfo currentMap = YandexGame.savesData.playerWrapper.maps[currentMapIndex];

        float currentTimeInSeconds = LapTimeManager.MinuteCount * 60 + LapTimeManager.SecondCount;

        if (currentMap.fastestTime == 0 || currentMap.fastestTime > currentTimeInSeconds)
        {
            currentMap.fastestTime = currentTimeInSeconds;
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
        //записываем рекорды если есть
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