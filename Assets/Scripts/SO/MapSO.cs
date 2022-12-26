using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/Map")]
public class MapSO : ScriptableObject
{
    [SerializeField] private string mapName;
    [SerializeField] private int[] gemAward;
    [SerializeField] private Sprite sprite;
    [SerializeField] private SceneAsset scene;

    public string Name => name;
    public int[] GemAward => gemAward;
    public Sprite Sprite => sprite;
    public SceneAsset Scene => scene;
}