using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCollectible", menuName = "ScriptableObject/Model/CharacterCollectible")]
public class CharacterCollectibleSO : ÑollectibleSO
{
    [SerializeField] private GameObject prefab;

    public GameObject Prefab => prefab;
}
