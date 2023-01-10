[System.Serializable]
public struct MapInfo
{
    public string mapName;
    public int highestPlace;
    public float fastestTime;
    public bool isPassed;

    public MapInfo(string mapName)
    {
        this.mapName = mapName;
        highestPlace = 0;
        fastestTime = 0;
        isPassed = false;
    }
}
