using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarCollectible", menuName = "ScriptableObject/Model/CarCollectible")]
public class CarColorSO : ÑollectibleSO
{
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private int cost;

    public int Cost => cost;
    public List<Material> Materials => materials;
}
