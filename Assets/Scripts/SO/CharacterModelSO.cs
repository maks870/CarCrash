using UnityEngine;

[CreateAssetMenu(fileName = "CharacterModelCollectible", menuName = "ScriptableObject/Model/CharacterModelCollectible")]
public class CharacterModelSO : CollectibleSO
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject prefab;

    public override string Name { get => name; }
    public Sprite Sprite => sprite;
    public GameObject Prefab => prefab;

}
