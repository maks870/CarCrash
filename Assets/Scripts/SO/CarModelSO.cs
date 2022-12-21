using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarModelCollectible", menuName = "ScriptableObject/Collectible/CarModelCollectible")]
public class CarModelSO : CollectibleSO
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private MeshFilter meshFilter;

    public override string Name { get => sprite.name; }
    public Sprite Sprite => sprite;
    public MeshFilter MeshFilter => meshFilter;
}