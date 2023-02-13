using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CarModelCollectible", menuName = "ScriptableObject/Collectible/CarModelCollectible")]
public class CarModelSO : CollectibleSO
{
    [SerializeField] private float acceleration;
    [SerializeField] private float handleability;
    [SerializeField] private Sprite sprite;
    [SerializeField] private AssetReference meshAsset;

    public float Acceleration => acceleration;
    public float Handleability => handleability;
    public override string Name { get => name; }
    public Sprite Sprite => sprite;
    public AssetReference MeshAsset => meshAsset;
}