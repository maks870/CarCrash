using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarModelCollectible", menuName = "ScriptableObject/Collectible/CarModelCollectible")]
public class CarModelSO : CollectibleSO
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Mesh mesh;

    public override string Name { get => sprite.name; }
    public Sprite Sprite => sprite;
    public Mesh Mesh => mesh;
}