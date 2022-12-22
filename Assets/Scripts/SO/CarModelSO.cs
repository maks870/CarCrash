using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarModelCollectible", menuName = "ScriptableObject/Collectible/CarModelCollectible")]
public class CarModelSO : CollectibleSO
{
    [SerializeField] private float acceleration;
    [SerializeField] private float handleability;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Mesh mesh;

    public float Acceleration => acceleration;
    public float Handleability => handleability;
    public override string Name { get => name; }
    public Sprite Sprite => sprite;
    public Mesh Mesh => mesh;
}