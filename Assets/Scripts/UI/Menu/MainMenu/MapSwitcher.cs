using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MapSwitcher : MonoBehaviour
{

    [SerializeField] private GameObject button;

    private bool isFirstLoad = true;
    private List<MapSO> mapsSO = new List<MapSO>();
    private List<MapSO> openedMaps = new List<MapSO>();
    private List<MapSO> closedMaps = new List<MapSO>();
    private List<ButtonMapUI> buttons = new List<ButtonMapUI>();

    public void LoadSOSubscribe()
    {
        SOLoader.instance.EndLoadingEvent += () =>
        {
            mapsSO.AddRange(SOLoader.instance.GetSOList<MapSO>());
        };

        //SOLoader.instance.OnLoadingEvent += (scriptableObj) =>
        //{
        //    if (scriptableObj.GetType() == typeof(MapSO))
        //        mapsSO.Add((MapSO)scriptableObj);
        //};
        //SOLoader.LoadAllSO<MapSO>((result) => FillListBySO(result));
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
            buttons[i].ClosedImage.gameObject.SetActive(false);

            int highestPlace = YandexGame.savesData.playerWrapper.GetHighestPlace(openMaps[i].Name);
            int fastestTimeMiliSec = 0;
            int fastestTimeSec = 0;
            int fastestTime = YandexGame.savesData.playerWrapper.GetFastestTime(openMaps[i].Name, out fastestTimeMiliSec);
            float fastestTimeMin = Math.DivRem(fastestTime, 60, out fastestTimeSec);

            buttons[i].BestPlace.text = highestPlace.ToString();

            if (fastestTime != 0)
            {
                string minNull = fastestTimeMin > 10 ? "" : "0";
                string secNull = fastestTimeSec > 10 ? "" : "0";
                string miliSecNull = fastestTimeMiliSec > 10 ? "" : "0";

                buttons[i].FastestTime.text = $"{minNull}{fastestTimeMin}:{secNull}{fastestTimeSec}.{miliSecNull}{fastestTimeMiliSec}";
                buttons[i].BestTimeObj.gameObject.SetActive(true);
            }
            else
            {
                buttons[i].BestTimeObj.gameObject.SetActive(false);
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
            buttons[j].ClosedImage.gameObject.SetActive(true);

            buttons[j].BestTimeObj.gameObject.SetActive(false);
            buttons[j].BestPlace.text = "";

            buttons[j].CupImage.sprite = buttons[j].CupSprites[0];
            buttons[j].CupImage.gameObject.SetActive(true);

            buttons[j].MapSO = closedMaps[i];
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
        SceneTransition.SwitchScene(map.name);
    }
}
