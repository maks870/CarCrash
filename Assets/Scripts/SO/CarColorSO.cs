using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarCollectible", menuName = "ScriptableObject/Model/CarCollectible")]
public class CarColorSO : ÑollectibleSO
{
    [SerializeField] private Texture2D texture;
    [SerializeField] private int cost;

    public int Cost => cost;
    public Texture2D Texture => texture;
}
