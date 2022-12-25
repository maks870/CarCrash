using UnityEngine;

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
    [SerializeField] private GameObject prefab;
    [SerializeField] private CharacterType characterType;

    public override string Name { get => name; }
    public Sprite Sprite => sprite;
    public GameObject Prefab => prefab;
    public CharacterType CharacterType => characterType;
}
