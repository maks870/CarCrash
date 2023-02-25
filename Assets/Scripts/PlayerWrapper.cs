using System.Collections.Generic;

[System.Serializable]
public class PlayerWrapper
{
    public bool newMission = true;
    public bool careerIsEnded = false;
    public string lastMap = "";
    public string currentCharacterItem;
    public string currentCarColorItem;
    public string currentCarModelItem;
    public List<int> lastMapPlaces = new List<int>();
    public List<string> collectibles = new List<string>();
    public List<string> newCollectibles = new List<string>();
    public List<MapInfo> maps = new List<MapInfo>();

    public int GetMapInfoIndex(string mapName)
    {
        int index = 0;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
            {
                index = i;
                return index;
            }
        }

        return index;
    }

    public int GetFastestTime(string mapName, out int miliSec)
    {
        miliSec = maps[0].fastestTimeMiliSec;
        int fastestTime = maps[0].fastestTime;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
            {
                fastestTime = maps[i].fastestTime;
                miliSec = maps[i].fastestTimeMiliSec;
                return fastestTime;
            }
        }

        return fastestTime;
    }

    public int GetHighestPlace(string mapName)
    {
        int highestPlace = maps[0].highestPlace;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
            {
                highestPlace = maps[i].highestPlace;
                return highestPlace;
            }
        }

        return highestPlace;
    }


}
