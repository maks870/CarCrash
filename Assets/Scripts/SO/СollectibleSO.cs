using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class �ollectibleSO : ScriptableObject
{
    [SerializeField] private Sprite sprite;

    public Sprite Sprite => sprite;
}
