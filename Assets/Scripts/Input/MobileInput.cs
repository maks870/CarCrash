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

        for (int i = 0; i < abilities.Count; i++)
        {
            GameObject buttonAbilityObj = Instantiate(buttonAbilityPref, container);
            Button buttonAbility = buttonAbilityObj.GetComponent<Button>();
            buttonAbility.image.sprite = abilities[i].Icon;
            int abilityIndex = i;
            buttonAbility.onClick.AddListener(() => PressButtonAbilityEvent.Invoke(abilityIndex));
            buttonAbility.onClick.AddListener(() => Destroy(buttonAbilityObj));
        }

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
