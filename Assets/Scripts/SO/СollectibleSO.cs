using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality
{
    rare,
    epic,
    legendary

}
public class ÑollectibleSO : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Quality quality;

    public Quality Quality => quality;
    public Sprite Sprite => sprite;
}
