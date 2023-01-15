using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private CarModelSO car;
    [SerializeField] private MapSO nextMap;
    [SerializeField] private Sprite sprite;
    [SerializeField] private SceneAsset scene;
    [SerializeField] private MapAward[] awards;

    public string Name => name;
    public CarModelSO Car => car;
    public MapSO NextMap => nextMap;
    public Sprite Sprite => sprite;
    public SceneAsset Scene => scene;
    public MapAward[] Awards => awards;
}