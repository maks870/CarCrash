using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarModelCollectible", menuName = "ScriptableObject/Model/CarModelCollectible")]
public class CarModelSO : CollectibleSO
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject prefab;

    public override string Name { get => sprite.name; }
    public Sprite Sprite => sprite;
    public GameObject Prefab => prefab;
}