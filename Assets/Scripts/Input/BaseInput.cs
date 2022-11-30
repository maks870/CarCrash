using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    [SerializeField] protected Transform container;

    protected float horizontalAxis;
    protected float verticalAxis;
    public abstract event PressButtonAbility PressButtonAbilityEvent;
    public abstract float HorizontalAxis { get; }
    public abstract float VerticalAxis { get; }

    public delegate void PressButtonAbility(int numberAbility);
    public abstract void SetAbilities(AbilitySO[] abilities);
}
