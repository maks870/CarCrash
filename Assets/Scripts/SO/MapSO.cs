using UnityEngine;

[System.Serializable]
public class MapAward
{
    public int coins = 0;
    public int gems = 0;

    public MapAward AddAward(MapAward award)
    {
        coins += award.coins;
        gems += award.gems;
        return this;
    }
}

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/Map")]
public class MapSO : ScriptableObject
{
    [SerializeField] private int maxPoints;
    [SerializeField] private CarModelSO car;
    [SerializeField] private MapSO nextMap;
    [SerializeField] private Sprite sprite;
    [SerializeField] private MapAward[] awards;

    public string Name => name;
    public int MaxPoints => maxPoints;
    public CarModelSO Car => car;
    public MapSO NextMap => nextMap;
    public Sprite Sprite => sprite;
    public MapAward[] Awards => awards;
}