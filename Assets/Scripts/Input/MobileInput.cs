using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : BaseInput
{
    [SerializeField] private GameObject buttonAbilityPref;

    public override event PressButtonAbility PressButtonAbilityEvent;

    public override void SetAbilities(List<AbilitySO> abilities)
    {
        ClearButton();

        GameObject buttonAbilityObj = Instantiate(buttonAbilityPref, container);
        Button buttonAbility = buttonAbilityObj.GetComponent<Button>();
        buttonAbility.image.sprite = abilities[0].Icon;
        buttonAbility.onClick.AddListener(() => PressButtonAbilityEvent.Invoke(0));
        buttonAbility.onClick.AddListener(() => Destroy(buttonAbilityObj));
    }

    public void SetVerticalAxis(int y) 
    {
        verticalAxis = y;
    }

    public void SetHorizontalAxis(int x) 
    {
        horizontalAxis = x;
    }
}
