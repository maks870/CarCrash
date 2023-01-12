using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct MapAward
{
    public int gems;
    public int coins;
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