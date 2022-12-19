using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarCollectible", menuName = "ScriptableObject/Model/CarCollectible")]
public class CarCollectibleSO : ÑollectibleSO
{
    [SerializeField] private List<Material> materials = new List<Material>();

    public List<Material> Materials => materials;
}
