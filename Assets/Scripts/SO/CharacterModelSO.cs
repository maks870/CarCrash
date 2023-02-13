using UnityEngine;
using UnityEngine.AddressableAssets;

public enum CharacterType
{
    RainbowFriends,
    Minecraft,
    Hagi,
    SquidGame,
    Amogus
}

[CreateAssetMenu(fileName = "CharacterModelCollectible", menuName = "ScriptableObject/Model/CharacterModelCollectible")]
public class CharacterModelSO : CollectibleSO
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private AssetReference assetReference;
    [SerializeField] private CharacterType characterType;

    public override string Name { get => name; }
    public Sprite Sprite => sprite;
    public AssetReference AssetReference => assetReference;
    public CharacterType CharacterType => characterType;


}
