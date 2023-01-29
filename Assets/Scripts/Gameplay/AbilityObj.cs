using UnityEngine;

[System.Serializable]
public class AbilityObj : MonoBehaviour
{
    [SerializeField] private AbilitySO abilitySO;
    [Range(0, 100)] [SerializeField] private int dropChance;

    public AbilitySO AbilitySO => abilitySO;
    public int DropChance => dropChance;
}
