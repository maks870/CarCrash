[System.Serializable]
public class MapInfo
{
    public string mapName;
    public int highestPlace;
    public float fastestTime;
    public bool isPassed;

    public bool newRecordPlace;
    public bool newRecordTime;

    public MapInfo(string mapName)
    {
        this.mapName = mapName;
        highestPlace = 0;
        fastestTime = 0;
        newRecordPlace = false;
        newRecordTime = false;
        isPassed = false;
    }
}
