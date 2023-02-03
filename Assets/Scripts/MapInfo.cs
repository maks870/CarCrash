[System.Serializable]
public class MapInfo
{
    public string mapName;
    public int highestPlace;
    public int fastestTime;
    public int fastestTimeMiliSec;
    public int maxPoints;
    public bool isPassed;

    public bool newRecordPlace;
    public bool newRecordTime;

    public MapInfo(string mapName, int maxPoints)
    {
        this.mapName = mapName;
        this.maxPoints = maxPoints;
        highestPlace = 0;
        fastestTime = 0;
        fastestTimeMiliSec = 0;
        newRecordPlace = false;
        newRecordTime = false;
        isPassed = false;
    }
}
