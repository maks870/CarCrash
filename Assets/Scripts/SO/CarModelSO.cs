using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CarModelCollectible", menuName = "ScriptableObject/Collectible/CarModelCollectible")]
public class CarModelSO : CollectibleSO
{
    [SerializeField] private float acceleration;
    [SerializeField] private float handleability;
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject prefab;

    public float Acceleration => acceleration;
    public float Handleability => handleability;
    public override string Name { get => name; }
    public Sprite Sprite => sprite;
    public GameObject Prefab => prefab;
}