using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MapSwitcher : MonoBehaviour
{

    [SerializeField] private GameObject button;

    private bool isFirstLoad = true;
    private List<MapSO> mapsSO = new List<MapSO>();
    private List<MapSO> openedMaps = new List<MapSO>();
    private List<MapSO> closedMaps = new List<MapSO>();
    private List<ButtonMapUI> buttons = new List<ButtonMapUI>();

    private void Awake()
    {
        List<MapSO> maps = SOLoader.LoadSOByType<MapSO>();
        FillListBySO(maps);
    }

    private void CreateButtons()
    {
        buttons.Add(button.GetComponent<ButtonMapUI>());
        for (int i = 0; i < mapsSO.Count - 1; i++)
        {
            ButtonMapUI newButton = Instantiate(button, transform).GetComponent<ButtonMapUI>();
            buttons.Add(newButton);
        }

        isFirstLoad = false;
    }

    private void UpdateUI(List<MapSO> openMaps, List<MapSO> closedMaps)
    {
        for (int i = 0; i < openMaps.Count; i++)
        {
            buttons[i].Image.sprite = openMaps[i].Sprite;
            buttons[i].NumberLvl.text = openMaps[i].Name;
            buttons[i].ClosedImage.gameObject.SetActive(false);

            int highestPlace = YandexGame.savesData.playerWrapper.GetHighestPlace(openMaps[i].Name);
            int fastestTime = YandexGame.savesData.playerWrapper.GetFastestTime(openMaps[i].Name);
            int fastestTimeSec = 0;
            float fastestTimeMin = Math.DivRem(fastestTime, 60, out fastestTimeSec);

            buttons[i].BestPlace.text = highestPlace.ToString();

            if (fastestTime != 0)
            {
                buttons[i].FastestTime.text = fastestTimeMin + ":" + fastestTimeSec;
                buttons[i].FastestTime.gameObject.SetActive(true);
            }
            else
            {
                buttons[i].FastestTime.gameObject.SetActive(false);
            }

            if (highestPlace != 0)
                highestPlace = highestPlace > 3 ? 0 : highestPlace;
            else
                buttons[i].BestPlace.text = "";

            buttons[i].CupImage.sprite = buttons[i].CupSprites[highestPlace];
            buttons[i].CupImage.gameObject.SetActive(true);
            buttons[i].MapSO = openMaps[i];

            buttons[i].Button.onClick.RemoveAllListeners();
            MapSO mapSO = buttons[i].MapSO;
            buttons[i].Button.onClick.AddListener(() => StartLvl(mapSO));
        }

        for (int i = 0; i < closedMaps.Count; i++)
        {
            int j = i + openMaps.Count;
            buttons[j].Image.sprite = closedMaps[i].Sprite;
            buttons[j].NumberLvl.text = closedMaps[i].Name;
            buttons[j].ClosedImage.gameObject.SetActive(true);


            buttons[i].FastestTime.text = "";
            buttons[i].FastestTime.gameObject.SetActive(false);
            buttons[i].BestPlace.text = "";

            buttons[i].CupImage.sprite = buttons[i].CupSprites[0];
            buttons[i].CupImage.gameObject.SetActive(true);

            buttons[i].MapSO = closedMaps[i];
            buttons[j].Button.onClick.RemoveAllListeners();
        }
    }

    private void LoadMapSO()
    {
        openedMaps.Clear();
        closedMaps.Clear();
        List<MapInfo> mapInfos = YandexGame.savesData.playerWrapper.maps;
        closedMaps.AddRange(mapsSO);

        foreach (MapInfo itemName in mapInfos)
        {
            MapSO mapSO = closedMaps.Find(item => item.Name == itemName.mapName);

            if (mapSO != null)
            {
                openedMaps.Add(mapSO);
                closedMaps.Remove(mapSO);
            }
        }

        UpdateUI(openedMaps, closedMaps);
    }

    public void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadMapSO();
    }

    public void SelectCurrentButton()
    {
    }

    public void FillListBySO(List<MapSO> maps)
    {
        mapsSO.AddRange(maps);
    }

    public void StartLvl(MapSO map)
    {
        YandexGame.savesData.playerWrapper.lastMap = map.Name;
        YandexGame.SaveProgress();
        SceneTransition.SwitchScene(map.Scene.name);
    }
}
