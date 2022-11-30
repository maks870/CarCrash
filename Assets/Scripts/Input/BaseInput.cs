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

    protected void ClearButton() 
    {
        foreach (Transform child in container) 
        {
            Destroy(child.gameObject);
        }
    }
    public abstract void SetAbilities(List<AbilitySO> abilities);

}
