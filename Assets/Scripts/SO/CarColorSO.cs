using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarColorCollectible", menuName = "ScriptableObject/Collectible/CarColorCollectible")]
public class CarColorSO : CollectibleSO
{
    [SerializeField] private Texture2D texture;
    [SerializeField] private int cost;

    public override string Name { get => texture.name; }
    public int Cost => cost;
    public Texture2D Texture => texture;
}
