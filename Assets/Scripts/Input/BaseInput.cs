using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    [SerializeField] protected Transform container;

    protected float horizontalAxis;
    protected float verticalAxis;
    protected float handbrake;
    public abstract event PressButtonAbility PressButtonAbilityEvent;
    public float HorizontalAxis => horizontalAxis;
    public float VerticalAxis => verticalAxis;
    public float HandBrake => handbrake;

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
